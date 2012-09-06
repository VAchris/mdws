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
/// Summary description for ObservationTO
/// </summary>

namespace gov.va.medora.mdws.dto
{
    public class LabObservationTO : AbstractTO
    {
        public LabObservationTypeTO observationType;
        public String value;
        public String value1;
        public String value2;
        public String method;
        public String qualifier;
        public String standardized;
        public String device;
        public String status;
        public String timestamp;

        public LabObservationTO() { }

        public LabObservationTO(LabObservation mdoObj)
        {
            if (mdoObj == null)
            {
                return;
            }
            if (mdoObj.Type != null)
            {
                this.observationType = new LabObservationTypeTO(mdoObj.Type);
            }
            if (mdoObj.Value != "")
            {
                this.value = mdoObj.Value;
            }
            if (mdoObj.Value1 != "")
            {
                this.value1 = mdoObj.Value1;
            }
            if (mdoObj.Value2 != "")
            {
                this.value2 = mdoObj.Value2;
            }
            if (mdoObj.Method != "")
            {
                this.method = mdoObj.Method;
            }
            if (mdoObj.Qualifier != "")
            {
                this.qualifier = mdoObj.Qualifier;
            }
            if (mdoObj.Standardized != "")
            {
                this.standardized = mdoObj.Standardized;
            }
            if (mdoObj.Device != "")
            {
                this.device = mdoObj.Device;
            }
            if (mdoObj.Status != "")
            {
                this.status = mdoObj.Status;
            }
            if (mdoObj.Timestamp != null)
            {
                this.timestamp = mdoObj.Timestamp.ToString("yyyyMMdd.HHmmss");
            }
        }
    }
}