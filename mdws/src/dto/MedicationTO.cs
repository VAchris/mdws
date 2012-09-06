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
    public class MedicationTO : AbstractTO
    {
        public string id;
        public string name;
        public string rxNum;
        public string quantity;
        public string expirationDate;
        public string issueDate;
        public string startDate;
        public string stopDate;
        public string orderId;
        public string status;
        public string refills;
        public bool isOutpatient;
        public bool isInpatient;
        public bool isIV;
        public bool isUnitDose;
        public bool isNonVA;
        public bool isImo;
        public string lastFillDate;
        public string remaining;
        public TaggedText facility;
        public AuthorTO provider;
        public string cost;
        public string sig;
        public string type;
        public string additives;
        public string solution;
        public string rate;
        public string route;
        public string dose;
        public string instruction;
        public string comment;
        public string dateDocumented;
        public AuthorTO documentor;
        public string detail;
        public string schedule;
        public string daysSupply;
        public TaggedText hospital;
        public TaggedText drug;

        public MedicationTO() { }

        public MedicationTO(Medication mdo)
        {
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.rxNum = mdo.RxNumber;
            this.quantity = mdo.Quantity;
            this.expirationDate = mdo.ExpirationDate;
            this.issueDate = mdo.IssueDate;
            this.startDate = mdo.StartDate;
            this.stopDate = mdo.StopDate;
            this.orderId = mdo.OrderId;
            this.status = mdo.Status;
            this.refills = mdo.Refills;
            this.isOutpatient = mdo.IsOutpatient;
            this.isInpatient = mdo.IsInpatient;
            this.isIV = mdo.IsIV;
            this.isUnitDose = mdo.IsUnitDose;
            this.isNonVA = mdo.IsNonVA;
            this.lastFillDate = mdo.LastFillDate;
            this.remaining = mdo.Remaining;
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            if (mdo.Provider != null)
            {
                this.provider = new AuthorTO(mdo.Provider);
            }
            this.cost = mdo.Cost;
            this.sig = mdo.Sig;
            this.type = mdo.Type;
            this.additives = mdo.Additives;
            this.solution = mdo.Solution;
            this.rate = mdo.Rate;
            this.route = mdo.Route;
            this.dose = mdo.Dose;
            this.instruction = mdo.Instruction;
            this.comment = mdo.Comment;
            this.dateDocumented = mdo.DateDocumented;
            if (mdo.Documentor != null)
            {
                this.documentor = new AuthorTO(mdo.Documentor);
            }
            this.detail = mdo.Detail;
            this.schedule = mdo.Schedule;
            this.daysSupply = mdo.DaysSupply;
            this.isImo = mdo.IsImo;
            if(!String.IsNullOrEmpty(mdo.Hospital.Value))
                this.hospital = new TaggedText(mdo.Hospital);
            if (!String.IsNullOrEmpty(mdo.Drug.Value))
                this.drug = new TaggedText(mdo.Drug);
        }
    }
}
