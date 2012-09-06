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
    public class AttachmentTO : BaseSmTO
    {
        public string attachmentName;
        public string mimeType;
        public byte[] attachment;

        public AttachmentTO() { }

        public AttachmentTO(gov.va.medora.mdo.domain.sm.MessageAttachment messageAttachment)
        {
            if (messageAttachment == null)
            {
                return;
            }

            attachmentName = messageAttachment.AttachmentName;
            mimeType = messageAttachment.MimeType;
            attachment = messageAttachment.SmFile;
            this.id = messageAttachment.Id;
            this.oplock = messageAttachment.Oplock;
        }
    }
}