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
