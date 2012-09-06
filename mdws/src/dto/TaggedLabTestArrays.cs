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
    public class TaggedLabTestArrays : AbstractArrayTO
    {
        public TaggedLabTestArray[] arrays;

        public TaggedLabTestArrays() { }

        public TaggedLabTestArrays(IndexedHashtable labTests)
        {
            if (labTests == null || labTests.Count == 0)
            {
                return;
            }

            arrays = new TaggedLabTestArray[labTests.Count];

            for (int i = 0; i < labTests.Count; i++)
            {
                string tag = (string)labTests.GetKey(i);

                if (labTests.GetValue(i) == null)
                {
                    arrays[i] = new TaggedLabTestArray(tag);
                }
                if (labTests.GetValue(i) is IList<LabTest>)
                {
                    arrays[i] = new TaggedLabTestArray(tag, (IList<LabTest>)labTests.GetValue(i));
                }
                else if (labTests.GetValue(i) is LabTest[])
                {
                    arrays[i] = new TaggedLabTestArray(tag);
                }
                else if (labTests.GetValue(i) is LabTest)
                {
                    arrays[i] = new TaggedLabTestArray(tag);
                }
                else
                {
                    return; 
                }
            }
        }
    }
}