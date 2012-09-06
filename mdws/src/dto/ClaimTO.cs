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
using System.Linq;
using System.Web;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class ClaimTO : AbstractTO
    {
        public string id;
        public string patientId;
        public string patientName;
        public string patientSsn;
        public string episodeDate;
        public string timestamp;
        public string lastEditTimestamp;
        public string insuranceName;
        public string cost;
        public string billableStatus;
        public string condition;
        public string serviceConnectedPercent;
        public string consultId;
        public string comment;

        public ClaimTO() { }

        public ClaimTO(Claim mdo)
        {
            this.id = mdo.Id;
            this.patientId = mdo.PatientId;
            this.patientName = mdo.PatientName;
            this.patientSsn = mdo.PatientSSN;
            this.episodeDate = mdo.EpisodeDate;
            this.timestamp = mdo.Timestamp;
            this.lastEditTimestamp = mdo.LastEditTimestamp;
            this.insuranceName = mdo.InsuranceName;
            this.cost = mdo.Cost;
            this.billableStatus = mdo.BillableStatus;
            this.condition = mdo.Condition;
            this.serviceConnectedPercent = mdo.ServiceConnectedPercent;
            this.consultId = mdo.ConsultId;
            this.comment = mdo.Comment;
        }
    }
}