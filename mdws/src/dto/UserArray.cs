using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class UserArray : AbstractArrayTO
    {
        public UserTO[] users;

        public UserArray() { }

        public UserArray(User[] mdoUsers)
        {
            if (mdoUsers == null)
            {
                return;
            }
            users = new UserTO[mdoUsers.Length];
            for (int i = 0; i < mdoUsers.Length; i++)
            {
                users[i] = new UserTO(mdoUsers[i]);
            }
            count = mdoUsers.Length;
        }
    }
}
