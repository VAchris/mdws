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
    public class MailboxTO : BaseSmTO
    {
        public FolderTO[] folders;

        public MailboxTO() { }

        public MailboxTO(gov.va.medora.mdo.domain.sm.Mailbox mailbox)
        {
            if (mailbox == null)
            {
                return;
            }

            IList<FolderTO> allFolders = new List<FolderTO>();

            if (mailbox.UserFolders != null && mailbox.UserFolders.Count > 0)
            {
                for (int i = 0; i < mailbox.UserFolders.Count; i++)
                {
                    allFolders.Add(new FolderTO(mailbox.UserFolders[i]));
                }
            }

            if (mailbox.SystemFolders != null && mailbox.SystemFolders.Count > 0)
            {
                for (int i = 0; i < mailbox.SystemFolders.Count; i++)
                {
                    allFolders.Add(new FolderTO(mailbox.SystemFolders[i]));
                }
            }

            if (allFolders.Count > 0)
            {
                folders = new FolderTO[allFolders.Count];
                allFolders.CopyTo(folders, 0);
            }
        }
    }
}