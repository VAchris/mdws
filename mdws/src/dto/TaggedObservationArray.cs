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
    public class TaggedObservationArray : AbstractTaggedArrayTO
    {
        public LabObservationTO[] observations;

        public TaggedObservationArray() { }

        public TaggedObservationArray(string tag, LabObservation[] observations)
        {
            this.tag = tag;
            if (observations == null)
            {
                this.count = 0;
                return;
            }
            this.observations = new LabObservationTO[observations.Length];
            for (int i = 0; i < observations.Length; i++)
            {
                this.observations[i] = new LabObservationTO(observations[i]);
            }
            this.count = observations.Length;
        }

        public TaggedObservationArray(string tag, LabObservation observation)
        {
            this.tag = tag;
            if (observation == null)
            {
                this.count = 0;
                return;
            }
            this.observations = new LabObservationTO[1];
            this.observations[0] = new LabObservationTO(observation);
            this.count = 1;
        }

        public TaggedObservationArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }
    }
}
