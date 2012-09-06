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
    public class RatedDisabilityTO : AbstractTO
    {
        public string id;
        public string name;
        public string percent;
        public string serviceConnected;
        public string extremityAffected;
        public string originalEffectiveDate;
        public string currentEffectiveDate;

        public RatedDisabilityTO() { }

        public RatedDisabilityTO(RatedDisability mdo)
        {
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.percent = mdo.Percent;
            this.serviceConnected = (mdo.ServiceConnected == true ? "Y" : "N");
            this.extremityAffected = mdo.ExtremityAffected;
            this.originalEffectiveDate = mdo.OriginalEffectiveDate;
            this.currentEffectiveDate = mdo.CurrenEffectiveDate;
        }
    }
}