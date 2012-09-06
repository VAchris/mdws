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
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedPatientArray : AbstractTaggedArrayTO
    {
        public PatientTO[] patients;

        public TaggedPatientArray() { }

        public TaggedPatientArray(string tag, Patient[] patients)
        {
            this.tag = tag;
            if (patients == null)
            {
                this.count = 0;
                return;
            }
            this.patients = new PatientTO[patients.Length];
            for (int i = 0; i < patients.Length; i++)
            {
                this.patients[i] = new PatientTO(patients[i]);
            }
            this.count = patients.Length;
        }

        public TaggedPatientArray(string tag, Patient patient)
        {
            this.tag = tag;
            if (patient == null)
            {
                this.count = 0;
                return;
            }
            this.patients = new PatientTO[1];
            this.patients[0] = new PatientTO(patient);
            this.count = 1;
        }

        public TaggedPatientArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedPatientArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
