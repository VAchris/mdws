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
using System.Collections.Specialized;
using gov.va.medora.mdo;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    public class LocationLib
    {
        MySession mySession;

        public LocationLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TaggedTextArray getSitesForStation()
        {
            TaggedTextArray result = new TaggedTextArray();

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
                List<SiteId> lst = HospitalLocation.getSitesForStation(mySession.ConnectionSet.BaseConnection);
                if (lst == null || lst.Count == 0)
                {
                    return null;
                }
                result.results = new TaggedText[lst.Count];
                for (int i = 0; i < lst.Count; i++)
                {
                    result.results[i] = new TaggedText(lst[i].Id, lst[i].Name);
                }
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getClinicsByName(string name)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(name))
            {
                result.fault = new FaultTO("Empty clinic name");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                OrderedDictionary d = HospitalLocation.getClinicsByName(mySession.ConnectionSet.BaseConnection, name);
                result = new TaggedTextArray(d);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

    }
}