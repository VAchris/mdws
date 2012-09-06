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
    public class ThreadTO : BaseSmTO
    {
        public Int32 messageCategory;
        public string subject;
        public MessageTO[] messages;
        public AnnotationTO[] annotations;
        public TriageGroupTO mailGroup;

        public ThreadTO() { }

        public ThreadTO(gov.va.medora.mdo.domain.sm.Thread thread)
        {
            if (thread == null)
            {
                return;
            }

            id = thread.Id;
            oplock = thread.Oplock;
            mailGroup = new TriageGroupTO(thread.MailGroup);
            subject = thread.Subject;
            messageCategory = (Int32)thread.MessageCategoryType;

            if (thread.Annotations != null)
            {
                annotations = new AnnotationTO[thread.Annotations.Count];
                for (int i = 0; i < thread.Annotations.Count; i++)
                {
                    annotations[i] = new AnnotationTO(thread.Annotations[i]);
                }
            }
            if (thread.Messages != null)
            {
                messages = new MessageTO[thread.Messages.Count];
                for (int i = 0; i < thread.Messages.Count; i++)
                {
                    messages[i] = new MessageTO(thread.Messages[i]);
                }
            }
        }
    }
}