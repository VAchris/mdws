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

/// <summary>
/// Summary description for ObservationTypeTO
/// </summary>

namespace gov.va.medora.mdws.dto
{
    public class LabObservationTypeTO : AbstractTO
    {
        public String id = "";
        public String title = "";
        public String units = "";
        public String range = "";

        public LabObservationTypeTO() { }

        public LabObservationTypeTO(LabObservationType mdoObj)
        {
            if (mdoObj == null)
            {
                return;
            }
            if (mdoObj.Id != "")
            {
                this.id = mdoObj.Id;
            }
            if (mdoObj.Title != "")
            {
                this.title = mdoObj.Title;
            }
            if (mdoObj.Units != "")
            {
                this.units = mdoObj.Units;
            }
            if (mdoObj.Range != "")
            {
                this.range = mdoObj.Range;
            }
        }
    }
}