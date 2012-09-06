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
using System.Linq;
using System.Web;

namespace gov.va.medora.mdws.dto.sm
{
    [Serializable]
    public class FolderTO : BaseSmTO
    {
        public string name;
        public bool isSystemFolder;

        public FolderTO() { }

        public FolderTO(mdo.domain.sm.Folder folder)
        {
            if (folder == null)
            {
                return;
            }
            this.id = folder.Id;
            this.oplock = folder.Oplock;
            this.name = folder.Name;
            isSystemFolder = folder.SystemFolder;
        }
    }
}