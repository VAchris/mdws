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

/// <summary>
/// Summary description for ObservationTypeTO
/// </summary>

namespace gov.va.medora.mdws.dto
{
    public class ObservationTypeTO : AbstractTO
    {
        public string id;
        public string category;
        public string name;
        public string shortName;
        public string dataId;
        public string dataName;
        public string dataType;

        public ObservationTypeTO() { }

        public ObservationTypeTO(ObservationType mdo)
        {
            if (mdo == null)
            {
                return;
            }
            this.id = mdo.Id;
            this.name = mdo.Name;
            this.category = mdo.Category;
            this.shortName = mdo.ShortName;
            this.dataId = mdo.DataId;
            this.dataName = mdo.DataName;
            this.dataType = mdo.DataType;
        }
    }
}
