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

namespace gov.va.medora.mdws.dto.sm
{
    [Serializable]
    public class TriageGroupsTO : AbstractTO
    {
        public int count;
        public TriageGroupTO[] triageGroups;

        public TriageGroupsTO() { }

        public TriageGroupsTO(IList<gov.va.medora.mdo.domain.sm.TriageGroup> groups)
        {
            if (groups == null || groups.Count <= 0)
            {
                return;
            }

            count = groups.Count;
            triageGroups = new TriageGroupTO[count];

            for (int i = 0; i < count; i++)
            {
                triageGroups[i] = new TriageGroupTO(groups[i]);
            }
        }
    }
}