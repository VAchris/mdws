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
    public class SurgeryReportTO : AbstractTO
    {
        public string id;
        public string title;
        public string timestamp;
        public AuthorTO author;
        public string text;
        public TaggedText facility;
        public string status;
        public TaggedText specialty;
        public string preOpDx;
        public string postOpDx;
        public string labWork;
        public string dictationTimestamp;
        public string transcriptionTimestamp;

        public SurgeryReportTO() { }

        public SurgeryReportTO(SurgeryReport mdo)
        {
            this.id = mdo.Id;
            this.title = mdo.Title;
            this.timestamp = mdo.Timestamp;
            if (mdo.Author != null)
            {
                this.author = new AuthorTO(mdo.Author);
            }
            this.text = mdo.Text;
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            this.status = mdo.Status;
            this.specialty = new TaggedText(mdo.Specialty);
            this.preOpDx = mdo.PreOpDx;
            this.postOpDx = mdo.PostOpDx;
            this.labWork = mdo.LabWork;
            this.dictationTimestamp = mdo.DictationTimestamp;
            this.transcriptionTimestamp = mdo.TranscriptionTimestamp;
        }
    }
}
