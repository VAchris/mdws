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
using gov.va.medora.mdo;

/// <summary>
/// Summary description for UserTO
/// </summary>

namespace gov.va.medora.mdws.dto
{
    public class UserTO : AbstractTO
    {
        public string name;
        public string SSN;
        public string DUZ;
        public string siteId;
        public string office;
        public string phone;
        public string pager;
        public string service;
        public string title;
        public string orderRole;
        public string userClass;
        public string greeting;
        public string siteMessage;
        //public UserSecurityKeyArray securityKeys;
        //public UserOptionArray menuOptions;
        //public UserOptionArray delegatedOptions;
        //public TaggedTextArray divisions;

        public UserTO() { }

        public UserTO(User mdoUser)
        {
            if (mdoUser == null)
            {
                return;
            }
            this.name = mdoUser.Name == null ? "" : mdoUser.Name.getLastNameFirst();
            this.SSN = mdoUser.SSN == null ? "" : mdoUser.SSN.toString();
            this.DUZ = mdoUser.Uid;
            this.siteId = mdoUser.LogonSiteId == null ? "" : mdoUser.LogonSiteId.Id;
            this.office = mdoUser.Office;
            this.phone = mdoUser.Phone;
            this.pager = mdoUser.VoicePager;
            if (mdoUser.Service != null)
            {
                this.service = mdoUser.Service.Name;
            }
            this.title = mdoUser.Title;
            this.orderRole = mdoUser.OrderRole;
            this.userClass = mdoUser.UserClass;
            this.greeting = mdoUser.Greeting;
            //if (mdoUser.SecurityKeys != null)
            //{
            //    this.securityKeys = new UserSecurityKeyArray(mdoUser.SecurityKeys);
            //}
            //if (mdoUser.MenuOptions != null)
            //{
            //    this.menuOptions = new UserOptionArray(mdoUser.MenuOptions);
            //}
            //if (mdoUser.DelegatedOptions != null)
            //{
            //    this.delegatedOptions = new UserOptionArray(mdoUser.DelegatedOptions);
            //}
            //if (mdoUser.Divisions != null)
            //{
            //    this.divisions = new TaggedTextArray(mdoUser.Divisions);
            //}
        }
    }
}
