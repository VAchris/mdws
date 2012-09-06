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
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedOrderArrays : AbstractArrayTO
    {
        public TaggedOrderArray[] arrays;

        public TaggedOrderArrays() { /* Empty Constructor */ }

        public TaggedOrderArrays(IndexedHashtable t)
        {
            this.count = 0;
            if(t == null || t.Count == 0)
            {
                return;
            }

            this.count = t.Count;
            arrays = new TaggedOrderArray[t.Count];

            for (int i = 0; i < t.Count; i++)
            {
                if (t.GetValue(i) == null)
                {
                    arrays[i] = new TaggedOrderArray();
                }
                else if (MdwsUtils.isException(t.GetValue(i)))
                {
                    arrays[i] = new TaggedOrderArray();
                    arrays[i].fault = new FaultTO((Exception)t.GetValue(i));
                }
                else if (t.GetValue(i).GetType() != typeof(Order[]))
                {
                    arrays[i] = new TaggedOrderArray((string)t.GetKey(i), null);
                }
                else
                {
                    arrays[i] = new TaggedOrderArray((string)t.GetKey(i), (Order[])t.GetValue(i));
                }
            }
        }
    }
}