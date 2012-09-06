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
