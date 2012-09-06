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
using System.Collections;

//NhinTypes = accession,allergy,appointment,document,immunization,lab,med,panel,patient,problem,procedure,radiology,rx,surgery,visit,vital

namespace gov.va.medora.mdws.dto
{
    [Serializable]
    public class PatientMedicalRecordTO : AbstractTO
    {
        public PatientMedicalRecordTO()
        {
            // empty constructor
        }

        public PatientMedicalRecordTO(IndexedHashtable ihs)
        {
            initCollections();

            if (ihs == null || ihs.Count == 0)
            {
                return;
            }

            for (int i = 0; i < ihs.Count; i++)
            {
                object key = ihs.GetKey(i);

                if (!(key is string))
                {
                    continue;
                }

                string sitecode = key as string;

                if (!(ihs.GetValue(i) is Hashtable))
                {
                    continue;
                }

                Hashtable domains = ihs.GetValue(i) as Hashtable;

                Meds.add(sitecode, domains["meds"] as IList<Medication>);
                Patient = new PatientTO(domains["demographics"] as Patient);
                Allergies.add(sitecode, domains["reactions"] as IList<Allergy>);
                Notes.add(sitecode, domains["documents"] as IList<Note>);
                Problems.add(sitecode, domains["problems"] as IList<Problem>);
                Appointments.add(sitecode, domains["appointments"] as IList<Appointment>);
                ChemHemReports.add(sitecode, domains["labs"] as IList<LabReport>);

                IList<HealthSummary> healthSummaries = domains["healthFactors"] as IList<HealthSummary>;

                

                // TODO - implement the remaining hashtable keys
                //results.Add("healthFactors", healthSummaries);
                //results.Add("flags", flags);
                //results.Add("consults", consults);
                //results.Add("procedures", null);
                //results.Add("visits", visits);
                //results.Add("appointments", appointments);
                //results.Add("problems", problems);
                //results.Add("vitals", vitals);
                //results.Add("labs", labs);
                //results.Add("immunizations", null);
            }
        }

        private void initCollections()
        {
            ContinuityOfCareDocuments = new TaggedTextArray();
            Patient = new PatientTO();
            Meds = new TaggedMedicationArrays();
            Allergies = new TaggedAllergyArrays();
            Appointments = new TaggedAppointmentArrays();
            Notes = new TaggedNoteArrays();
            ChemHemReports = new TaggedChemHemRptArrays();
            MicroReports = new TaggedMicrobiologyRptArrays();
            Problems = new TaggedProblemArrays();
            RadiologyReports = new TaggedRadiologyReportArrays();
            SurgeryReports = new TaggedSurgeryReportArrays();
            Vitals = new TaggedVitalSignArrays();
        }

        /// <summary>
        /// CCD documents from the patient's sites serialized as XML for easy domain object portability. 
        /// </summary>
        public TaggedTextArray ContinuityOfCareDocuments { get; set; }

        public PatientTO Patient { get; set; }

        public TaggedMedicationArrays Meds { get; set; }

        public TaggedAllergyArrays Allergies { get; set; }

        public TaggedAppointmentArrays Appointments { get; set; }

        public TaggedNoteArrays Notes { get; set; }

        public TaggedChemHemRptArrays ChemHemReports { get; set; }

        public TaggedMicrobiologyRptArrays MicroReports { get; set; }

        public TaggedProblemArrays Problems { get; set; }

        public TaggedRadiologyReportArrays RadiologyReports { get; set; }

        public TaggedSurgeryReportArrays SurgeryReports { get; set; }

        public TaggedVitalSignArrays Vitals { get; set; }
    }
}