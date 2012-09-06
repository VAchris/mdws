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
    public class VisitTO : AbstractTO
    {
        public string id;
        public string type;
        public PatientTO patient;
        public UserTO attending;
        public UserTO provider;
        public string service;
        public HospitalLocationTO location;
        public string patientType;
        public string visitId;
        public string timestamp;
        public string status;

        public VisitTO() { }

        public VisitTO(Visit mdo)
        {
            this.id = mdo.Id;
            this.type = mdo.Type;
            if (mdo.Patient != null)
            {
                this.patient = new PatientTO(mdo.Patient);
            }
            if (mdo.Attending != null)
            {
                this.attending = new UserTO(mdo.Attending);
            }
            if (mdo.Provider != null)
            {
                this.provider = new UserTO(mdo.Provider);
            }
            this.service = mdo.Service;
            if (mdo.Location != null)
            {
                this.location = new HospitalLocationTO(mdo.Location);
            }
            this.patientType = mdo.PatientType;
            this.visitId = mdo.VisitId;
            this.timestamp = mdo.Timestamp;
            this.status = mdo.Status;
        }
    }
}
