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
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class PatientRecordFlagArray : AbstractArrayTO
    { 
        public PatientRecordFlagTO[] flags;

        public PatientRecordFlagArray() { }

        public PatientRecordFlagArray(PatientRecordFlag[] mdos)
        {
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.flags = new PatientRecordFlagTO[mdos.Length];
            for (int i = 0; i < mdos.Length; i++)
            {
                this.flags[i] = new PatientRecordFlagTO(mdos[i]);
            }
            this.count = flags.Length;
        }

        public PatientRecordFlagArray(PatientRecordFlag mdo)
        {
            if (mdo == null)
            {
                this.count = 0;
                return;
            }
            this.flags = new PatientRecordFlagTO[1];
            this.flags[0] = new PatientRecordFlagTO(mdo);
            this.count = 1;
        }

        public PatientRecordFlagArray(Exception e)
        {
            this.fault = new FaultTO(e);
        }
    }
}
