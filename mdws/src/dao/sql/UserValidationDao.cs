#region CopyrightHeader
//
//  Copyright by Contributors
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0.txt
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace gov.va.medora.mdws.bse
{
    public interface IDao
    {
        Visitor getVisitor(string securityToken, string encryptionKey);
    }

    public class UserValidationDao : IDao
    {
        string _connectionString;
        string _tableName = "dbo.Session";
        bool _encrypt = true;

        public UserValidationDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Encrypt
        {
            get { return _encrypt; }
            set { _encrypt = value; }
        }

        internal SqlConnection openCxn()
        {
            SqlConnection cxn = new SqlConnection(_connectionString);
            cxn.Open();
            return cxn;
        }

        public Visitor getVisitor(string securityToken, string encryptionKey)
        {
            if (String.Equals(securityToken, ""))
            {
                return getAdministrativeVisitor();
            }
            return getRecord(securityToken, encryptionKey);
        }

        internal Visitor getRecord(string securityToken, string encryptionKey)
        {
            SqlConnection myCxn = openCxn();
            SqlCommand myCmd = myCxn.CreateCommand();
            myCmd.Connection = myCxn;
            SqlDataReader rdr = null;

            try
            {
                Encrypt = false;
                myCmd.CommandText = buildGetRecordStatement(securityToken, encryptionKey);
                rdr = myCmd.ExecuteReader();
                if (!rdr.HasRows)
                {
                    return null;
                }
                rdr.Read();
                Encrypt = true;
                return toVisitor(rdr, encryptionKey);
            }
            finally
            {
                rdr.Close();
                myCxn.Close();
            }
        }

        internal string buildGetRecordStatement(string securityToken, string encryptionKey)
        {
            if (_encrypt)
            {
                return "SELECT * FROM " + _tableName + " WHERE SessionId ='" +
                    escapeString(SSTCryptographer.Encrypt(securityToken, encryptionKey)) + "';";
            }
            else
            {
                return "SELECT * FROM " + _tableName + " WHERE SessionId ='" +
                    escapeString(securityToken) + "';";
            }
        }

        internal Visitor toVisitor(SqlDataReader rdr, string encryptionKey)
        {
            Visitor result = new Visitor();
            if (_encrypt)
            {
                result.SSN = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("SSN")), encryptionKey);
                result.Name = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("Name")), encryptionKey);
                result.UID = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("DUZ")), encryptionKey);
                result.SiteID = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("SiteId")), encryptionKey);
                result.SiteName = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("SiteName")), encryptionKey);
                result.Phone = SSTCryptographer.Decrypt(rdr.GetString(rdr.GetOrdinal("Phone")), encryptionKey);
            }
            else
            {
                result.SSN = rdr.GetString(rdr.GetOrdinal("SSN"));
                result.Name = rdr.GetString(rdr.GetOrdinal("Name"));
                result.UID = rdr.GetString(rdr.GetOrdinal("DUZ"));
                result.SiteID = rdr.GetString(rdr.GetOrdinal("SiteId"));
                result.SiteName = rdr.GetString(rdr.GetOrdinal("SiteName"));
                result.Phone = rdr.GetString(rdr.GetOrdinal("Phone"));
            }
            return result;
        }

        internal string escapeString(string s)
        {
            string result = "";
            if (StringUtils.isEmpty(s))
            {
                return result;
            }
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '\'')
                {
                    result += s[i];
                }
                else
                {
                    result += "'" + s[i];
                }
            }
            return result;
        }

        Visitor getAdministrativeVisitor()
        {
            Visitor v = new Visitor();
            v.SSN = "";
            v.Name = "";
            v.UID = "";
            v.SiteID = "";
            v.SiteName = "";
            v.Phone = "No Phone";
            return v;
        }
    }
}
