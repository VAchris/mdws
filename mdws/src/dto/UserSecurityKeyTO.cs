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
using System.Data;
using System.Configuration;
using gov.va.medora.mdo;
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdws.dto
{
    public class UserSecurityKeyTO : AbstractTO
    {
        public string id = "";
        public string name = "";
        public string descriptiveName = "";
        public string creatorId = "";
        public string creatorName = "";
        public string creationDate = "";
        public string reviewDate = "";

        public UserSecurityKeyTO() { }

        public UserSecurityKeyTO(UserSecurityKey mdo)
        {
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.descriptiveName = mdo.DescriptiveName;
            this.creatorId = mdo.CreatorId;
            this.creatorName = mdo.CreatorName;
            this.creationDate = mdo.CreationDate.Year == 1 ? "" : mdo.CreationDate.ToString("yyyyMMdd.HHmmss");
            this.reviewDate = mdo.ReviewDate.Year == 1 ? "" : mdo.ReviewDate.ToString("yyyyMMdd.HHmmss");
        }

        public UserSecurityKeyTO(AbstractPermission p)
        {
            if (p.Type != PermissionType.SecurityKey)
            {
                fault = new FaultTO(p.Name + " is not a Security Key");
                return;
            }
            this.id = p.PermissionId;
            this.name = p.Name;
        }
    }
}
