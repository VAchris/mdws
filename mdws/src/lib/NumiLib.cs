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
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    public class NumiLib
    {
        MySession mySession;

        public NumiLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TaggedHospitalLocationArrays getWards()
        {
            TaggedHospitalLocationArrays result = new TaggedHospitalLocationArrays();
            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterLib encounterLib = new EncounterLib(mySession);
                TaggedHospitalLocationArray wards = encounterLib.getWards();
                if (wards.fault != null)
                {
                    result.fault = wards.fault;
                    return result;
                }
                result.arrays = new TaggedHospitalLocationArray[] { wards };
                result.count = result.arrays.Length;
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }
    }
}
