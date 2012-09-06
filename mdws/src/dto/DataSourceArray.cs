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
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class DataSourceArray : AbstractArrayTO
    {
        public DataSourceTO[] items;

        public DataSourceArray() { }

        public DataSourceArray(DataSource mdo)
        {
            if (mdo == null)
            {
                return;
            }
            items = new DataSourceTO[1];
            items[0] = new DataSourceTO(mdo);
            count = 1;
        }

        public DataSourceArray(DataSource[] mdoItems)
        {
            if (mdoItems == null)
            {
                return;
            }
            items = new DataSourceTO[mdoItems.Length];
            for (int i = 0; i < mdoItems.Length; i++)
            {
                items[i] = new DataSourceTO(mdoItems[i]);
            }
            count = items.Length;
        }

        public DataSourceArray(IndexedHashtable t)
        {
            if (t.Count == 0)
            {
                return;
            }
            items = new DataSourceTO[t.Count];
            for (int i = 0; i < t.Count; i++)
            {
                if (t.GetValue(i).GetType().IsAssignableFrom(typeof(Exception)))
                {
                    fault = new FaultTO((Exception)t.GetValue(i));
                }
                //else if (t.GetValue(i) == null)
                //{
                //    items[i] = new TaggedAdtArray((string)t.GetKey(i));
                //}
                else
                {
                    items[i] = new DataSourceTO((DataSource)t.GetValue(i));
                }
            }
            count = items.Length;
        }
    }
}
