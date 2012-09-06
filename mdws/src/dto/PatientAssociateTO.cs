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

namespace gov.va.medora.mdws.dto
{
    public class PatientAssociateTO : PersonTO
    {
        public string association;
        public string relationshipToPatient;
        public string facilityName;

        public PatientAssociateTO() { }

        public PatientAssociateTO(PatientAssociate mdo)
        {
            this.name = mdo.Name.getLastNameFirst();
            if (mdo.HomeAddress != null)
            {
                this.homeAddress = new AddressTO(mdo.HomeAddress);
            }
            if (mdo.HomePhone != null)
            {
                this.homePhone = new PhoneNumTO(mdo.HomePhone);
            }
            if (mdo.CellPhone != null)
            {
                this.cellPhone = new PhoneNumTO(mdo.CellPhone);
            }
            this.association = mdo.Association;
            this.relationshipToPatient = mdo.RelationshipToPatient;
            this.facilityName = mdo.FacilityName;
        }
    }
}
