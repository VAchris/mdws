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
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedMentalHealthInstrumentAdministrationArray : AbstractTaggedArrayTO
    {
        public MentalHealthInstrumentAdministrationTO[] items;

        public TaggedMentalHealthInstrumentAdministrationArray() { }

        public TaggedMentalHealthInstrumentAdministrationArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedMentalHealthInstrumentAdministrationArray(string tag, MentalHealthInstrumentAdministration[] mdoItems)
        {
            this.tag = tag;
            if (mdoItems == null)
            {
                this.count = 0;
                return;
            }
            items = new MentalHealthInstrumentAdministrationTO[mdoItems.Length];
            for (int i = 0; i < mdoItems.Length; i++)
            {
                items[i] = new MentalHealthInstrumentAdministrationTO(mdoItems[i]);
            }
            count = items.Length;
        }

        public TaggedMentalHealthInstrumentAdministrationArray(string tag, List<MentalHealthInstrumentAdministration> mdoItems)
        {
            this.tag = tag;
            if (mdoItems == null)
            {
                this.count = 0;
                return;
            }
            items = new MentalHealthInstrumentAdministrationTO[mdoItems.Count];
            for (int i = 0; i < mdoItems.Count; i++)
            {
                items[i] = new MentalHealthInstrumentAdministrationTO(mdoItems[i]);
            }
            count = items.Length;
        }

        public TaggedMentalHealthInstrumentAdministrationArray(string tag, MentalHealthInstrumentAdministration administration)
        {
            this.tag = tag;
            if (administration == null)
            {
                this.count = 0;
                return;
            }
            this.items = new MentalHealthInstrumentAdministrationTO[1];
            this.items[0] = new MentalHealthInstrumentAdministrationTO(administration);
            this.count = 1;
        }
    }
}
