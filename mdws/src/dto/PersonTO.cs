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
