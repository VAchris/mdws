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
    public class TaggedHospitalLocationArray : AbstractTaggedArrayTO
    {
        public HospitalLocationTO[] locations;

        public TaggedHospitalLocationArray() { }

        public TaggedHospitalLocationArray(string tag, HospitalLocation[] mdoLocations)
        {
            this.tag = tag;
            if (mdoLocations == null)
            {
                this.count = 0;
                return;
            }
            locations = new HospitalLocationTO[mdoLocations.Length];
            for (int i = 0; i < mdoLocations.Length; i++)
            {
                locations[i] = new HospitalLocationTO(mdoLocations[i]);
            }
            count = mdoLocations.Length;
        }

        public TaggedHospitalLocationArray(string tag, HospitalLocation location)
        {
            this.tag = tag;
            if (location == null)
            {
                this.count = 0;
                return;
            }
            this.locations = new HospitalLocationTO[1];
            this.locations[0] = new HospitalLocationTO(location);
            this.count = 1;
        }

        public TaggedHospitalLocationArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }
    }
}
