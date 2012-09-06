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
    public class TaggedSurgicalPathologyRptArray : AbstractTaggedArrayTO
    {
        public SurgicalPathologyRpt[] rpts;

        public TaggedSurgicalPathologyRptArray() { }

        public TaggedSurgicalPathologyRptArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedSurgicalPathologyRptArray(string tag, SurgicalPathologyReport[] mdos)
        {
            this.tag = tag;
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.rpts = new SurgicalPathologyRpt[mdos.Length];
            for (int i = 0; i < mdos.Length; i++)
            {
                this.rpts[i] = new SurgicalPathologyRpt(mdos[i]);
            }
            this.count = rpts.Length;
        }

        public TaggedSurgicalPathologyRptArray(string tag, SurgicalPathologyReport mdo)
        {
            this.tag = tag;
            if (mdo == null)
            {
                this.count = 0;
                return;
            }
            this.rpts = new SurgicalPathologyRpt[1];
            this.rpts[0] = new SurgicalPathologyRpt(mdo);
            this.count = 1;
        }

        public TaggedSurgicalPathologyRptArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
