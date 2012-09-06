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