using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gov.va.medora.mdws.dto
{
    public class TaggedLabReportArray : AbstractArrayTO
    {
        public LabReportTO[] Arrays { get; set; }
    }
}