using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo;
using gov.va.medora.utils;
namespace gov.va.medora.mdws.dto
{
    public class LabResultTO : AbstractTO
    {
        public LabTestTO test;
        public string specimenType;
        public string comment;
        public string value;
        public string boundaryStatus;
        public string labSiteId;

        public LabResultTO() { }

        public LabResultTO(LabResult mdo)
        {
            if (mdo.Test != null)
            {
                this.test = new LabTestTO(mdo.Test);
            }
            this.specimenType = mdo.SpecimenType;
            this.comment = mdo.Comment;
            this.value = StringUtils.stripInvalidXmlCharacters(mdo.Value); // http://trac.medora.va.gov/web/ticket/1447
            this.boundaryStatus = mdo.BoundaryStatus;
            this.labSiteId = mdo.LabSiteId;
        }
    }
}
