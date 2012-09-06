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
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class PatientArray : AbstractArrayTO
    {
        public PatientTO[] patients;

        public PatientArray() { }

        public PatientArray(Patient[] mdo)
        {
            setProps(mdo);
        }

        public PatientArray(ArrayList lst)
        {
            setProps((Patient[])lst.ToArray(typeof(Patient)));    
        }

        private void setProps(Patient[] mdo)
        {
            if (mdo == null)
            {
                return;
            }
            patients = new PatientTO[mdo.Length];
            for (int i = 0; i < mdo.Length; i++)
            {
                patients[i] = new PatientTO(mdo[i]);
            }
            count = mdo.Length;
        }

        public PatientArray(SortedList lst)
        {
            if (lst == null || lst.Count == 0)
            {
                count = 0;
                return;
            }
            patients = new PatientTO[lst.Count];
            IDictionaryEnumerator e = lst.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                patients[i++] = new PatientTO((Patient)e.Value);
            }
            count = lst.Count;
        }
    }
}
