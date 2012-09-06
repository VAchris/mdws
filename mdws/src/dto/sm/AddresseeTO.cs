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
    public class AddresseeTO : BaseSmTO
    {
        public FolderTO folder;
        public DateTime readDate;
        public SmUserTO owner;
        //public MessageTO message;
        public Int32 messageId;
        public DateTime reminderDate;
        public Int32 role;

        public AddresseeTO() { }

        public AddresseeTO(mdo.domain.sm.Addressee addressee)
        {
            if (addressee == null)
            {
                return;
            }

            this.id = addressee.Id;
            this.oplock = addressee.Oplock;
            folder = new FolderTO(addressee.Folder);
            readDate = addressee.ReadDate;
            owner = new SmUserTO(addressee.Owner);
            //message = new MessageTO(addressee.Message); // having the message object was making it too easy to cause an infinite recursive loop - hopefully the message ID is enough
            reminderDate = addressee.ReminderDate;
            role = (Int32)addressee.Role;

            if (addressee.Message != null)
            {
                messageId = addressee.Message.Id;
            }
        }

    }
}