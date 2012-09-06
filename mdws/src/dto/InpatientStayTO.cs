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
    public class InpatientStayTO : AbstractTO
    {
        public PatientTO patient;
        public HospitalLocationTO location;
        public string admitTimestamp;
        public string dischargeTimestamp;
        public DischargeDiagnosesTO dischargeDiagnoses;
        public string type;
        public AdtTO[] adts;
        public string movementCheckinId;

        public InpatientStayTO() { }

        public InpatientStayTO(InpatientStay mdo)
        {
            if (mdo.Patient != null)
            {
                this.patient = new PatientTO(mdo.Patient);
            }
            if (mdo.Location != null)
            {
                this.location = new HospitalLocationTO(mdo.Location);
            }
            this.admitTimestamp = mdo.AdmitTimestamp;
            this.dischargeTimestamp = mdo.DischargeTimestamp;
            if (mdo.DischargeDiagnoses != null)
            {
                this.dischargeDiagnoses = new DischargeDiagnosesTO(mdo.DischargeDiagnoses);
            }
            this.type = mdo.Type;
            if (mdo.Adts != null && mdo.Adts.Length > 0)
            {
                this.adts = new AdtTO[mdo.Adts.Length];
                for (int i = 0; i < mdo.Adts.Length; i++)
                {
                    this.adts[i] = new AdtTO(mdo.Adts[i]);
                }
            }
            this.movementCheckinId = mdo.MovementCheckinId;
        }
    }
}
