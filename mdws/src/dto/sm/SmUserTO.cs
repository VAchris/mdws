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
    public class SmUserTO : BaseSmTO
    {
        public string username;
        public string lastName;
        public string firstName;
        public string middleName;
        public string email;
        public string ssn;
        public string nSsn;
        public DateTime lastNotification;
        public TriageGroupTO[] groups;
        public MailboxTO mailbox;

        //public ParticipantTypeEnum ParticipantType { get; set; }
        //public UserTypeEnum UserType { get; set; }
       // public List<TriageGroup> UserAssociatedGroups { get; set; }
        //list of groups that the actor belongs 
        //public UserStatusEnum Status { get; set; }
        //public EmailNotificationEnum EmailNotification { get; set; }
        //public MessageFilterEnum MessageFilter { get; set; }

        //public Mailbox Mailbox { get; set; }

        //public List<AdminRole> AdminRoles { get; set; }

        public SmUserTO() { }

        public SmUserTO(mdo.domain.sm.User user)
        {
            if (user == null)
            {
                return;
            }

            id = user.Id;
            username = user.Username;
            lastName = user.LastName;
            firstName = user.FirstName;
            middleName = user.MiddleName;
            email = user.Email;
            ssn = user.Ssn;
            nSsn = user.Nssn;
            lastNotification = user.LastNotification;

            if (user.Groups != null && user.Groups.Count > 0)
            {
                groups = new TriageGroupTO[user.Groups.Count];
                for (int i = 0; i < user.Groups.Count; i++)
                {
                    groups[i] = new TriageGroupTO(user.Groups[i]);
                }
            }

            if (user.Mailbox != null)
            {
                mailbox = new MailboxTO(user.Mailbox);
            }
        }
    }
}