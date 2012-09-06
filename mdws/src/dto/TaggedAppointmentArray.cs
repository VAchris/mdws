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
    public class TaggedAppointmentArray : AbstractTaggedArrayTO
    {
        public AppointmentTO[] appts;

        public TaggedAppointmentArray() { }

        public TaggedAppointmentArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedAppointmentArray(string tag, Appointment[] mdos)
        {
            this.tag = tag;
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.appts = new AppointmentTO[mdos.Length];
            for (int i = 0; i < mdos.Length; i++)
            {
                this.appts[i] = new AppointmentTO(mdos[i]);
            }
            this.count = appts.Length;
        }

        public TaggedAppointmentArray(string tag, Appointment mdo)
        {
            this.tag = tag;
            if (mdo == null)
            {
                this.count = 0;
                return;
            }
            this.appts = new AppointmentTO[1];
            this.appts[0] = new AppointmentTO(mdo);
            this.count = 1;
        }

        public TaggedAppointmentArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
