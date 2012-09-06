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
using gov.va.medora.mdo.conf;

namespace gov.va.medora.mdws.conf
{
    public class MdwsConfigConstants : ConfigFileConstants
    {
        public static string MDWS_CONFIG_SECTION = "MDWS";
        /// <summary>
        /// MDWS sessions log level
        /// </summary>
        public static string SESSIONS_LOG_LEVEL = "SessionsLogLevel";
        /// <summary>
        /// MDWS sessions logging
        /// </summary>
        public static string SESSIONS_LOGGING = "SessionsLogging";
        /// <summary>
        /// Production installation
        /// </summary>
        public static string MDWS_PRODUCTION = "Production";
        /// <summary>
        /// The facade sites file name
        /// </summary>
        public static string FACADE_SITES_FILE = "FacadeSitesFile";
        /// <summary>
        /// True/False
        /// </summary>
        public static string FACADE_PRODUCTION = "FacadeProduction";
        /// <summary>
        /// The facade version information
        /// </summary>
        public static string FACADE_VERSION = "FacadeVersion";

        public static string DEFAULT_VISIT_METHOD = "VisitMethod";

        public static string DEFAULT_CONTEXT = "DefaultContext";
        
        public static string EXCLUDE_SITE_200 = "ExcludeSite200";

        public static string WATCH_SITES_FILE = "WatchSitesFile";

        public static string BHIE_PASSWORD = "BhiePassword";

        /// <summary>
        /// The BSE data encryption key
        /// </summary>
        public static string BSE_SQL_ENCRYPTION_KEY = "EncryptionKey";
        /// <summary>
        /// Valid NHIN data types
        /// </summary>
        public static string NHIN_TYPES = "NhinTypes";
        /// <summary>
        /// MDWS service account federated ID
        /// </summary>
        public static string SERVICE_ACCOUNT_FED_UID = "ServiceAccountFederatedUid";
        /// <summary>
        /// MDWS service account name
        /// </summary>
        public static string SERVICE_ACCOUNT_NAME = "ServiceAccountSubjectName";
        /// <summary>
        /// MDWS service account BSE password
        /// </summary>
        public static string SERVICE_ACCOUNT_PASSWORD = "ServiceAccountPassword";
    }
}