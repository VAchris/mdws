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
using System.Collections;
using System.Linq;
using System.Web;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class PersonArray : AbstractArrayTO
    {
        public PersonTO[] persons;

        public PersonArray() { }

        public PersonArray(Person[] mdo)
        {
            setProps(mdo);
        }

        public PersonArray(ArrayList lst)
        {
            setProps((Person[])lst.ToArray(typeof(Person)));    
        }

        private void setProps(Person[] mdo)
        {
            if (mdo == null)
            {
                return;
            }
            persons = new PersonTO[mdo.Length];
            for (int i = 0; i < mdo.Length; i++)
            {
                persons[i] = new PersonTO(mdo[i]);
            }
            count = mdo.Length;
        }

        public PersonArray(SortedList lst)
        {
            if (lst == null || lst.Count == 0)
            {
                count = 0;
                return;
            }
            persons = new PersonTO[lst.Count];
            IDictionaryEnumerator e = lst.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                persons[i++] = new PersonTO((Person)e.Value);
            }
            count = lst.Count;
        }

        public PersonArray(IndexedHashtable t)
        {
            if (t == null || t.Count == 0)
            {
                count = 0;
                return;
            }
            persons = new PersonTO[t.Count];
            for (int i = 0; i < t.Count; i++)
            {
                if (t.GetValue(i) != null)
                {
                    persons[i] = new PersonTO((Person)t.GetValue(i));
                }
            }
            count = t.Count;
        }
    }
}