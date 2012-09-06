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

namespace gov.va.medora.mdws.dto.sm
{
    [Serializable]
    public class TriageGroupTO : BaseSmTO
    {
        public string name;
        public string description;
        public string vistaDiv;
        public SmClinicianTO[] clinicians;
        public SmPatientTO[] patients;
        
        //private TriageRelationTO[] relations;

        public TriageGroupTO() { }

        public TriageGroupTO(gov.va.medora.mdo.domain.sm.TriageGroup triageGroup)
        {
            if (triageGroup == null)
            {
                return;
            }

            id = triageGroup.Id;
            oplock = triageGroup.Oplock;
            name = triageGroup.Name;
            description = triageGroup.Description;
            vistaDiv = triageGroup.VistaDiv;

            if (triageGroup.Clinicians != null && triageGroup.Clinicians.Count > 0)
            {
                clinicians = new SmClinicianTO[triageGroup.Clinicians.Count];
                for (int i = 0; i < triageGroup.Clinicians.Count; i++)
                {
                    clinicians[i] = new SmClinicianTO(triageGroup.Clinicians[i]);
                }
            }

            if (triageGroup.Patients != null && triageGroup.Patients.Count > 0)
            {
                patients = new SmPatientTO[triageGroup.Patients.Count];
                for (int i = 0; i < triageGroup.Patients.Count; i++)
                {
                    patients[i] = new SmPatientTO(triageGroup.Patients[i]);
                }
            }
        }
    }
}