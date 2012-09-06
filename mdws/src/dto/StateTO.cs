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
    public class StateTO : AbstractTO
    {
        public string name;
        public string abbr;
        public string fips;
        public SiteArray sites;
        public CityArray cities;

        public StateTO() { }

        public StateTO(State mdoState)
        {
            this.name = mdoState.Name;
            this.abbr = mdoState.Abbr;
            this.fips = mdoState.Fips;
            if (mdoState.Sites != null)
            {
                for (int i = 0; i < mdoState.Sites.Count; i++)
                {
                    this.sites = new SiteArray(mdoState.Sites);
                }
            }
            if (mdoState.Cities != null)
            {
                for (int i = 0; i < mdoState.Cities.Count; i++)
                {
                    this.cities = new CityArray(mdoState.Cities);
                }
            }
        }
    }
}
