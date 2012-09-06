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
    public class MentalHealthInstrumentResultSetTO : AbstractTO
    {
        public string id;
        public string administrationId;
        public TaggedText scale;
        public string rawScore;
        public TaggedTextArray transformedScores;
        public TaggedText instrument;

        public MentalHealthInstrumentResultSetTO() { }

        public MentalHealthInstrumentResultSetTO(MentalHealthInstrumentResultSet mdo)
        {
            this.id = mdo.Id;
            this.administrationId = mdo.AdministrationId;
            this.scale = new TaggedText(mdo.Scale);
            this.rawScore = mdo.RawScore;
            this.transformedScores = new TaggedTextArray(mdo.TransformedScores);
            this.instrument = new TaggedText(mdo.Instrument);
        }
    }
}