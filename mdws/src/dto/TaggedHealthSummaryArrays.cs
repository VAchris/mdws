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
    public class TaggedHealthSummaryArrays : AbstractArrayTO
    {
        public TaggedHealthSummaryArray[] healthSummaryArrays;

        public TaggedHealthSummaryArrays() { }

#if false
        public TaggedHealthSummaryArray(HealthSummary[] mdo)
        {
            Init(mdo);
        }
        
#endif
        public TaggedHealthSummaryArrays(IndexedHashtable mdo)
        {
            Init(mdo);
        }

        public void Init(IndexedHashtable mdo)
        {
            if (mdo == null)
            {
                return;
            }
            healthSummaryArrays = new TaggedHealthSummaryArray[mdo.Count];
            for (int i = 0; i < mdo.Count; i++)
            {
                //healthSummaryArrays[i] = new HealthSummaryTO((TaggedHealthSummaryArray)mdo.GetValue(i),(string)mdo.GetKey(i));
            }
            count = mdo.Count;
        }

#if false
        public void Init(HealthSummary[] mdo)
        {
            if (mdo == null)
            {
                return;
            }
            healthSummaries = new HealthSummaryTO[mdo.Length];
            for (int i = 0; i < mdo.Length; i++)
            {
                healthSummaries[i] = new HealthSummaryTO(mdo[i]);
            }
            count = mdo.Length;
        } 
#endif
    }
}
