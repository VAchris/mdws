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
    public class AppointmentTO : AbstractTO
    {
        public string id;
        public string timestamp;
        public string title;
        public string status;
        public string text;
        public TaggedText facility;
        public HospitalLocationTO clinic;
        public string labDateTime;
        public string xrayDateTime;
        public string ekgDateTime;
        public string purpose;
        public string type;
        public string currentStatus;

        public AppointmentTO() { }

        public AppointmentTO(Appointment mdo)
        {
            this.id = mdo.Id;
            this.timestamp = mdo.Timestamp;
            this.title = mdo.Title;
            this.status = mdo.Status;
            this.text = mdo.Text;
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            if (mdo.Clinic != null)
            {
                this.clinic = new HospitalLocationTO(mdo.Clinic);
                if (mdo.Clinic.Facility != null)
                {
                    this.clinic.facility = new SiteTO();
                    this.clinic.facility.name = mdo.Clinic.Facility.Name;
                }
            }
            this.labDateTime = mdo.LabDateTime;
            this.xrayDateTime = mdo.XrayDateTime;
            this.ekgDateTime = mdo.EkgDateTime;
            this.purpose = mdo.Purpose;
            this.type = mdo.Type;
            this.currentStatus = mdo.CurrentStatus;
        }
    }
}
