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
    public class ProblemTO : AbstractTO
    {
        public string id;
        public string status;
        public string providerNarrative;
        public string onsetDate;
        public string modifiedDate;
        public string exposures;
        public string noteNarrative;
        public AuthorTO observer;
        public TaggedText facility;
        public ObservationTypeTO type;
        public string comment;
        public TaggedTextArray organizationalProperties;

        public ProblemTO() { }

        public ProblemTO(Problem mdo)
        {
            this.id = mdo.Id;
            this.status = mdo.Status;
            this.providerNarrative = mdo.ProviderNarrative;
            this.onsetDate = mdo.OnsetDate;
            this.modifiedDate = mdo.ModifiedDate;
            this.exposures = mdo.Exposures;
            this.noteNarrative = mdo.NoteNarrative;
            if (mdo.Observer != null)
            {
                this.observer = new AuthorTO(mdo.Observer);
            }
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            if (mdo.Type != null)
            {
                this.type = new ObservationTypeTO(mdo.Type);
            }
            this.comment = mdo.Comment;
            if (mdo.OrganizationProperties != null && mdo.OrganizationProperties.Count > 0)
            {
                this.organizationalProperties = new TaggedTextArray(mdo.OrganizationProperties);
            }
        }
    }
}
