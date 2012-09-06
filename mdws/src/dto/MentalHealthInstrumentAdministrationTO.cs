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
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class MentalHealthInstrumentAdministrationTO : AbstractTO
    {
        public string id;
        public TaggedText patient;
        public TaggedText instrument;
        public string dateAdministered;
        public string dateSaved;
        public TaggedText orderedBy;
        public TaggedText administeredBy;
        public bool isSigned;
        public bool isComplete;
        public string numberOfQuestionsAnswered;
        public string transmitStatus;
        public string transmitTime;
        public TaggedText hospitalLocation;
        public MentalHealthInstrumentResultSetTO results;

        public MentalHealthInstrumentAdministrationTO() { }

        public MentalHealthInstrumentAdministrationTO(MentalHealthInstrumentAdministration mdo)
        {
            this.id = mdo.Id;
            this.patient = new TaggedText(mdo.Patient);
            this.instrument = new TaggedText(mdo.Instrument);
            this.dateAdministered = mdo.DateAdministered;
            this.dateSaved = mdo.DateSaved;
            this.orderedBy = new TaggedText(mdo.OrderedBy);
            this.administeredBy = new TaggedText(mdo.AdministeredBy);
            this.isSigned = mdo.IsSigned;
            this.isComplete = mdo.IsComplete;
            this.numberOfQuestionsAnswered = mdo.NumberOfQuestionsAnswered;
            this.transmitStatus = mdo.TransmissionStatus;
            this.transmitTime = mdo.TransmissionTime;
            this.hospitalLocation = new TaggedText(mdo.HospitalLocation);
            if (mdo.ResultSet != null)
            {
                this.results = new MentalHealthInstrumentResultSetTO(mdo.ResultSet);
            }
        }
    }
}