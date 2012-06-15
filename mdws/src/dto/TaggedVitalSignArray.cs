using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedVitalSignArray : AbstractTaggedArrayTO
    {
        public VitalSignTO[] vitals;

        public TaggedVitalSignArray() { }

        public TaggedVitalSignArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedVitalSignArray(string tag, VitalSign[] mdos)
        {
            this.tag = tag;
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.vitals = new VitalSignTO[mdos.Length];
            for (int i = 0; i < mdos.Length; i++)
            {
                this.vitals[i] = new VitalSignTO(mdos[i]);
            }
            this.count = vitals.Length;
        }

        public TaggedVitalSignArray(string tag, VitalSign mdo)
        {
            this.tag = tag;
            if (mdo == null)
            {
                this.count = 0;
                return;
            }
            this.vitals = new VitalSignTO[1];
            this.vitals[0] = new VitalSignTO(mdo);
            this.count = 1;
        }

        public TaggedVitalSignArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
