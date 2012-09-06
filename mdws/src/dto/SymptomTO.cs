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
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class SymptomTO : AbstractTO
    {
        public string id;
        public string name;
        public bool isNational;
        public string vuid;
        public ObservationTypeTO type;
        public AuthorTO observer;
        public string timestamp;
        public TaggedText facility;

        public SymptomTO() { }

        public SymptomTO(Symptom mdo)
        {
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.isNational = mdo.IsNational;
            this.vuid = mdo.Vuid;
            if (mdo.Type != null)
            {
                this.type = new ObservationTypeTO(mdo.Type);
            }
            if (mdo.Observer != null)
            {
                this.observer = new AuthorTO(mdo.Observer);
            }
            this.timestamp = mdo.Timestamp;
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
        }
    }
}
