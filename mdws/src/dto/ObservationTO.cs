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

/// <summary>
/// Summary description for ObservationTO
/// </summary>

namespace gov.va.medora.mdws.dto
{
    public class ObservationTO : AbstractTO
    {
        public AuthorTO observer;
        public AuthorTO recorder;
        public string timestamp;
        public TaggedText facility;
        public HospitalLocationTO location;
        public ObservationTypeTO type;
        public string comment;

        public ObservationTO() { }

        public ObservationTO(Observation mdo)
        {
            if (mdo == null)
            {
                return;
            }
            if (mdo.Observer != null)
            {
                this.observer = new AuthorTO(mdo.Observer);
            }
            if (mdo.Recorder != null)
            {
                this.recorder = new AuthorTO(mdo.Recorder);
            }
            this.timestamp = mdo.Timestamp;
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            if (mdo.Location != null)
            {
                this.location = new HospitalLocationTO(mdo.Location);
            }
            if (mdo.Type != null)
            {
                this.type = new ObservationTypeTO(mdo.Type);
            }
            this.comment = mdo.Comment;
        }
    }
}
