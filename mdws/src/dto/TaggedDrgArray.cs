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
    public class TaggedDrgArray : AbstractTaggedArrayTO
    {
        public DrgTO[] items;

        public TaggedDrgArray() { }

        public TaggedDrgArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedDrgArray(string tag, Drg[] mdoItems)
        {
            this.tag = tag;
            if (mdoItems == null)
            {
                this.count = 0;
                return;
            }
            items = new DrgTO[mdoItems.Length];
            for (int i = 0; i < mdoItems.Length; i++)
            {
                items[i] = new DrgTO(mdoItems[i]);
            }
            count = items.Length;
        }

        public TaggedDrgArray(string tag, Drg drg)
        {
            this.tag = tag;
            if (drg == null)
            {
                this.count = 0;
                return;
            }
            this.items = new DrgTO[1];
            this.items[0] = new DrgTO(drg);
            this.count = 1;
        }
    }
}
