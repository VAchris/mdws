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
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedPersonArray : AbstractTaggedArrayTO
    {
        public string sourceName;
        public PersonTO[] persons;

        public TaggedPersonArray() { }

        public TaggedPersonArray(string tag, Person[] persons)
        {
            this.tag = tag;
            setSourceName();
            if (persons == null)
            {
                this.count = 0;
                return;
            }
            this.persons = new PersonTO[persons.Length];
            for (int i = 0; i < persons.Length; i++)
            {
                this.persons[i] = new PersonTO(persons[i]);
            }
            this.count = persons.Length;
        }

        public TaggedPersonArray(string tag, List<Person> persons)
        {
            this.tag = tag;
            setSourceName();
            if (persons == null)
            {
                this.count = 0;
                return;
            }
            this.persons = new PersonTO[persons.Count];
            for (int i = 0; i < persons.Count; i++)
            {
                this.persons[i] = new PersonTO(persons[i]);
            }
            this.count = persons.Count;
        }

        public TaggedPersonArray(string tag, Person person)
        {
            this.tag = tag;
            setSourceName();
            if (person == null)
            {
                this.count = 0;
                return;
            }
            this.persons = new PersonTO[1];
            this.persons[0] = new PersonTO(person);
            this.count = 1;
        }

        public TaggedPersonArray(string tag)
        {
            this.tag = tag;
            setSourceName();
            this.count = 0;
        }

        public TaggedPersonArray(string tag, Exception e)
        {
            this.tag = tag;
            setSourceName();
            this.fault = new FaultTO(e);
        }


        internal void setSourceName()
        {
            if (tag == "ADR")
            {
                this.sourceName = "Administrative Data Repository";
            }
            else if (tag == "VADIR")
            {
                this.sourceName = "VA-DoD Information Repository";
            }
            else if (tag == "VBACORP")
            {
                this.sourceName = "VBA Corp";
            }
            else
            {
                this.sourceName = "National Patient Table";
            }
        }
    }
}
