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
using System.Collections.Specialized;
using gov.va.medora.mdws.dto;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws
{
    public class VitalsLib
    {
        MySession mySession;

        public VitalsLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TaggedVitalSignSetArrays getVitalSigns()
        {
            TaggedVitalSignSetArrays result = new TaggedVitalSignSetArrays();

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
                IndexedHashtable t = VitalSignSet.getVitalSigns(mySession.ConnectionSet);
                result = new TaggedVitalSignSetArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedVitalSignArrays getLatestVitalSigns()
        {
            TaggedVitalSignArrays result = new TaggedVitalSignArrays();

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
                IndexedHashtable t = VitalSign.getLatestVitalSigns(mySession.ConnectionSet);
                result = new TaggedVitalSignArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

    }
}
