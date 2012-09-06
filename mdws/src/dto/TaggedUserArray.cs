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

namespace gov.va.medora.mdws.dto
{
    public class TaggedUserArray : AbstractTaggedArrayTO
    {
        public UserTO[] users;

        public TaggedUserArray() { }

        public TaggedUserArray(string tag, User[] mdoUsers)
        {
            this.tag = tag;
            if (mdoUsers == null)
            {
                this.count = 0;
                return;
            }
            users = new UserTO[mdoUsers.Length];
            for (int i = 0; i < mdoUsers.Length; i++)
            {
                users[i] = new UserTO(mdoUsers[i]);
            }
            count = mdoUsers.Length;
        }

        public TaggedUserArray(string tag, User user)
        {
            this.tag = tag;
            if (user == null)
            {
                this.count = 0;
                return;
            }
            this.users = new UserTO[1];
            this.users[0] = new UserTO(user);
            this.count = 1;
        }

        public TaggedUserArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }
    }
}
