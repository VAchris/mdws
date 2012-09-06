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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws.dao.sql
{
    public class ConnectionTester
    {
        string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// ConnectionTest constructor. Tries to obtan connection string from HTTP Session, fails over to .config file. Throws
        /// a ConfigurationErrorsException if neither is available
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException" />
        public ConnectionTester()
        {
            try
            {
                MySession session = HttpContext.Current.Session["MySession"] as MySession;
                _connectionString = session.MdwsConfiguration.SqlConnectionString;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ConnectionTester(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Using the configured SQL connection string, opens a connection to the database and attempts
        /// to wrtie a record from the ApplicationSessions table 
        /// </summary>
        /// <returns>BoolTO with true value if read was successful, FaultTO otherwise</returns>
        public BoolTO canWrite()
        {
            UsageDao dao = new UsageDao(_connectionString);
            string sessionId = gov.va.medora.utils.StringUtils.getNCharRandom(24);
            ApplicationSession session = new ApplicationSession(sessionId, System.Net.IPAddress.Loopback.ToString(), DateTime.Now);
            session.End = DateTime.Now;
            session.LocalhostName = System.Net.IPAddress.Loopback.ToString();
            ApplicationRequest request = new ApplicationRequest(sessionId, new Uri("http://mdws.va.gov/getSomething"), DateTime.Now, DateTime.Now, "<Soap>...</Soap>", "<Soap>...</Soap>");
            session.Requests.Add(request);
            dao.saveSession(session);
            return dao.deleteSession(sessionId);
        }

        /// <summary>
        /// Using the configured SQL connection string, opens a connection to the database
        /// </summary>
        /// <returns>BoolTO with true value if read was successful, FaultTO otherwise</returns>
        public BoolTO canConnect()
        {
            BoolTO result = new BoolTO();
            SqlConnection conn = new SqlConnection(_connectionString);
            try
            {
                conn.Open();
                result.trueOrFalse = true;
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}