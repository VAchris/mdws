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
using gov.va.medora.mdws.conf;

namespace gov.va.medora.mdws.bse
{
    public interface IUserSecurityProvider
    {
        IPrincipal getUserPrincipal(string key);
    }

    public class VistaUserSecurityProvider : IUserSecurityProvider
    {
        public VistaUserSecurityProvider()
        {
        }

        public IPrincipal getUserPrincipal(string key)
        {
            MdwsConfiguration conf = new mdws.conf.MdwsConfiguration();
            string connectionString = conf.BseValidatorConnectionString;
            string encryptionKey = 
                conf.AllConfigs[MdwsConfigConstants.MDWS_CONFIG_SECTION][MdwsConfigConstants.BSE_SQL_ENCRYPTION_KEY];
            IDao dao = new UserValidationDao(connectionString);
            return dao.getVisitor(key, encryptionKey).Principal;
        }
    }
}
