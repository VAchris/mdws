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
    public class HospitalLocationTO : AbstractTO
    {
        public string id = "";
        public string name = "";
        public TaggedText department;
        public TaggedText service;
        public TaggedText specialty;
        public SiteTO facility;
        public string building = "";
        public string floor = "";
        public string room = "";
        public string bed = "";
        public string status = "";
        public string phone = "";
        public string appointmentTimestamp = "";
        public string type = "";
        public string physicalLocation = ""; // free text description string - parsability is undefined. see #2917

        public HospitalLocationTO() { }

        public HospitalLocationTO(HospitalLocation mdo)
        {
            if (mdo == null)
            {
                return;
            }
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.department = new TaggedText(mdo.Department);
            this.service = new TaggedText(mdo.Service);
            this.specialty = new TaggedText(mdo.Specialty);
            if (mdo.Facility != null)
            {
                this.facility = new SiteTO(mdo.Facility);
            }
            this.building = mdo.Building;
            this.floor = mdo.Floor;
            this.room = mdo.Room;
            this.bed = mdo.Bed;
            this.status = mdo.Status;
            this.phone = mdo.Phone;
            this.appointmentTimestamp = mdo.AppointmentTimestamp;
            this.type = mdo.Type;
            this.physicalLocation = mdo.PhysicalLocation;
        }
    }
}
