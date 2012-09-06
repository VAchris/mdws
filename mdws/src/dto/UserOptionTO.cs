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
using gov.va.medora.mdo.dao.vista;

namespace gov.va.medora.mdws.dto
{
    public class UserOptionTO : AbstractTO
    {
        public string number;
        public string id;
        public string name;
        public string displayName;
        public string key;
        public string reverseKey;
        public string type;
        public bool primaryOption;

        public UserOptionTO() { }

        public UserOptionTO(UserOption mdo)
        {
            this.number = mdo.Number;
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.displayName = mdo.DisplayName;
            this.key = mdo.Key;
            this.reverseKey = mdo.ReverseKey;
            this.type = mdo.Type;
            this.primaryOption = mdo.PrimaryOption;
        }
    }
}
