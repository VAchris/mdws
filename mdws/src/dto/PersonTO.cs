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
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class PersonTO : AbstractTO
    {
        public string name;
        public string ssn;
        public string gender;
        public string dob;
        public string ethnicity;
        public int age;
        public string maritalStatus;
        public AddressTO homeAddress;
        public PhoneNumTO homePhone;
        public PhoneNumTO cellPhone;
        public DemographicSetTO[] demographics;

        public PersonTO() { }

        public PersonTO(Person mdo)
        {
            this.name = mdo.Name.getLastNameFirst();
            if (mdo.SSN != null)
            {
                this.ssn = mdo.SSN.toHyphenatedString();
            }
            this.gender = mdo.Gender;
            this.dob = mdo.DOB;
            this.ethnicity = mdo.Ethnicity;
            this.age = mdo.Age;
            this.maritalStatus = mdo.MaritalStatus;
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
            if (mdo.Demographics != null && mdo.Demographics.Count > 0)
            {
                this.demographics = new DemographicSetTO[mdo.Demographics.Count];
                int i = 0;
                foreach (KeyValuePair<string, DemographicSet> kvp in mdo.Demographics)
                {
                    this.demographics[i++] = new DemographicSetTO(kvp);
                }
            }
        }
    }
}
