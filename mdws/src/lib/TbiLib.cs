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
    public class TbiLib
    {
        MySession mySession;

        public TbiLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TaggedConsultArray getConsultsForPatient()
        {
            TaggedConsultArray result = new TaggedConsultArray();

            try
            {
                OrdersLib lib = new OrdersLib(mySession);
                TaggedConsultArrays ta = lib.getConsultsForPatient();
                if (ta.fault != null)
                {
                    result.fault = ta.fault;
                    return result;
                }
                result = ta.arrays[0];
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

    }
}
