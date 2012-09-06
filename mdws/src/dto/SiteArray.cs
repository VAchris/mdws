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
using System.Collections;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class SiteArray : AbstractArrayTO
    {
        public SiteTO[] sites;

        public SiteArray() { }

        public SiteArray(Site[] mdoSites)
        {
            setProps(mdoSites);
        }

        public SiteArray(ArrayList lst)
        {
            setProps((Site[])lst.ToArray(typeof(Site)));    
        }

        private void setProps(Site[] mdoSites)
        {
            if (mdoSites == null)
            {
                return;
            }
            ArrayList al = new ArrayList(mdoSites.Length);
            for (int i = 0; i < mdoSites.Length; i++)
            {
                if (mdoSites[i] != null)
                {
                    al.Add(new SiteTO(mdoSites[i]));
                }
            }
            sites = (SiteTO[])al.ToArray(typeof(SiteTO));
            count = sites.Length;
        }

        public SiteArray(SortedList lst)
        {
            if (lst == null || lst.Count == 0)
            {
                count = 0;
                return;
            }
            ArrayList al = new ArrayList(lst.Count);
            IDictionaryEnumerator e = lst.GetEnumerator();
            while (e.MoveNext())
            {
                Site s = (Site)e.Value;
                if (s != null)
                {
                    al.Add(new SiteTO(s));
                }
            }
            sites = (SiteTO[])al.ToArray(typeof(SiteTO));
            count = sites.Length;
        }
    }
}
