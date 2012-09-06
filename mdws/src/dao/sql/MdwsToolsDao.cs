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
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace gov.va.medora.mdws.dao.sql
{
    public class MdwsToolsDao
    {
        string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public MdwsToolsDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection getSqlConnection()
        {
            SqlConnection cxn = new SqlConnection(_connectionString);
            cxn.Open();
            return cxn;
        }

        /// <summary>
        /// Get the VhaSites.xml file from the official MDWS resources database
        /// </summary>
        /// <returns></returns>
        public byte[] getLatestSitesFile()
        {
            using (SqlConnection cxn = getSqlConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("getSitesFile", cxn);
                adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                return (byte[])adapter.SelectCommand.ExecuteScalar();
            }
        }

        /// <summary>
        /// Save a sites file file to the resources database
        /// </summary>
        /// <param name="file"></param>
        internal void saveSitesFile(byte[] file)
        {
            string sql = "DELETE FROM MdwsResources WHERE FileName=@fileName;\r\nINSERT INTO MdwsResources (FileName, LastUpdated, [File], Active) VALUES (" +
                "@fileName, @lastUpdated, @file, @active);";

            SqlParameter fileNameParam = new SqlParameter("@fileName", System.Data.SqlDbType.VarChar, 50);
            fileNameParam.Value = "VhaSites.xml";

            SqlParameter lastUpdatedParam = new SqlParameter("@lastUpdated", System.Data.SqlDbType.DateTime);
            lastUpdatedParam.Value = DateTime.Now;

            SqlParameter fileParam = new SqlParameter("@file", System.Data.SqlDbType.Image);
            fileParam.Value = file;

            SqlParameter activeParam = new SqlParameter("@active", System.Data.SqlDbType.Bit);
            activeParam.Value = true;

            using (SqlConnection cxn = getSqlConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sql, cxn);
                cmd.Parameters.Add(fileNameParam);
                cmd.Parameters.Add(lastUpdatedParam);
                cmd.Parameters.Add(fileParam);
                cmd.Parameters.Add(activeParam);
                adapter.InsertCommand = cmd;
                adapter.InsertCommand.ExecuteNonQuery();
            }
        }
    }
}