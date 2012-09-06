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
using gov.va.medora.utils;

namespace gov.va.medora.mdws.dto
{
    public class NoteTO : AbstractTO
    {
        public string id = "";
        public string timestamp = "";
        public string admitTimestamp = "";
        public string dischargeTimestamp = "";
        public string serviceCategory = "";
        public string localTitle = "";
        public string standardTitle = "";
        public AuthorTO author;
        public HospitalLocationTO location;
        public string text = "";
        public bool hasAddendum = false;
        public bool isAddendum = false;
        public string originalNoteID = "";
        public bool hasImages = false;
        public string itemId = "";
        public AuthorTO approvedBy;
        public string status = "";

        public NoteTO() { }

        public NoteTO(Note mdoNote)
        {
            if (mdoNote == null) // || ((mdoNote.Id == null || mdoNote.Id == "") && mdoNote.ApprovedBy == null))
            {
                return;
            }
            this.id = mdoNote.Id;
            this.timestamp = mdoNote.Timestamp;
            this.admitTimestamp = mdoNote.AdmitTimestamp;
            this.dischargeTimestamp = mdoNote.DischargeTimestamp;
            this.localTitle = mdoNote.LocalTitle;
            this.standardTitle = mdoNote.StandardTitle;
            this.serviceCategory = mdoNote.ServiceCategory;
            if (mdoNote.Author != null)
            {
                this.author = new AuthorTO(mdoNote.Author);
            }
            if (mdoNote.Location != null)
            {
                this.location = new HospitalLocationTO(mdoNote.Location);
            }
            else if (!String.IsNullOrEmpty(mdoNote.SiteId.Id) || !String.IsNullOrEmpty(mdoNote.SiteId.Name))
            {
                HospitalLocation hl = new HospitalLocation(mdoNote.SiteId.Id, mdoNote.SiteId.Name);
                this.location = new HospitalLocationTO(hl);
            }
            this.text = mdoNote.Text;
            this.hasAddendum = mdoNote.HasAddendum;
            this.isAddendum = mdoNote.IsAddendum;
            this.originalNoteID = mdoNote.OriginalNoteId;
            this.hasImages = mdoNote.HasImages;
            if (mdoNote.ApprovedBy != null)
            {
                this.approvedBy = new AuthorTO(mdoNote.ApprovedBy);
            }
            this.status = mdoNote.Status;
        }
    }
}
