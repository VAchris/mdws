using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    [Serializable]
    public class LabReportTO : AbstractTO
    {
        public AuthorTO Author { get; set; }
        public string CaseNumber { get; set; }
        public string Comment { get; set; }
        public SiteTO Facility { get; set; }
        public string Id { get; set; }
        public LabSpecimenTO Specimen { get; set; }
        public string Timestamp { get; set; }
        public string Title { get; set; }

        public LabReportTO(LabReport report)
        {
            Author = new AuthorTO(report.Author);
            CaseNumber = report.CaseNumber;
            Comment = report.Comment;
            if (report.Facility != null)
            {
                Facility = new SiteTO(new mdo.Site(report.Facility.Id, report.Facility.Name));
            }
            Id = report.Id;
            Specimen = new LabSpecimenTO(report.Specimen);
            Timestamp = report.Timestamp;
            Title = report.Title;
        }
    }
}