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
    public class CityArray : AbstractArrayTO
    {
        public CityTO[] cities;

        public CityArray() { }

        public CityArray(City[] mdoCities)
        {
            if (mdoCities == null || mdoCities.Length == 0)
            {
                count = 0;
                return;
            }
            cities = new CityTO[mdoCities.Length];
            for (int i = 0; i < mdoCities.Length; i++)
            {
                cities[i] = new CityTO(mdoCities[i]);
            }
            count = mdoCities.Length;
        }

        public CityArray(SortedList lst)
        {
            if (lst == null || lst.Count == 0)
            {
                count = 0;
                return;
            }
            cities = new CityTO[lst.Count];
            IDictionaryEnumerator e = lst.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                cities[i++] = new CityTO((City)e.Value);
            }
            count = lst.Count;
        }
    }
}
