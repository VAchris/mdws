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
using System.Collections;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class PatientAssociateArray : AbstractArrayTO
    {
        public PatientAssociateTO[] pas;

        public PatientAssociateArray() { }

        public PatientAssociateArray(PatientAssociate[] mdo)
        {
            setProps(mdo);
        }

        public PatientAssociateArray(ArrayList lst)
        {
            setProps((PatientAssociate[])lst.ToArray(typeof(PatientAssociate)));    
        }

        private void setProps(PatientAssociate[] mdo)
        {
            if (mdo == null)
            {
                return;
            }
            pas = new PatientAssociateTO[mdo.Length];
            for (int i = 0; i < mdo.Length; i++)
            {
                pas[i] = new PatientAssociateTO(mdo[i]);
            }
            count = mdo.Length;
        }

        public PatientAssociateArray(SortedList lst)
        {
            if (lst == null || lst.Count == 0)
            {
                count = 0;
                return;
            }
            pas = new PatientAssociateTO[lst.Count];
            IDictionaryEnumerator e = lst.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                pas[i++] = new PatientAssociateTO((PatientAssociate)e.Value);
            }
            count = lst.Count;
        }
    }
}
