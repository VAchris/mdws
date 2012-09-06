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
using gov.va.medora.mdo;
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdws.dto
{
    public class UserSecurityKeyArray : AbstractArrayTO
    {
        public UserSecurityKeyTO[] keys;

        public UserSecurityKeyArray() { }

        public UserSecurityKeyArray(UserSecurityKey[] mdo)
        {
            if (mdo == null)
            {
                return;
            }
            keys = new UserSecurityKeyTO[mdo.Length];
            for (int i = 0; i < mdo.Length; i++)
            {
                keys[i] = new UserSecurityKeyTO(mdo[i]);
            }
            count = mdo.Length;
        }

        public UserSecurityKeyArray(Dictionary<string, AbstractPermission> mdo)
        {
            if (mdo == null)
            {
                return;
            }
            keys = new UserSecurityKeyTO[mdo.Count];
            int i = 0;
            foreach (KeyValuePair<string, AbstractPermission> kvp in mdo)
            {
                keys[i++] = new UserSecurityKeyTO(kvp.Value);
            }
            count = mdo.Count;
        }
    }
}
