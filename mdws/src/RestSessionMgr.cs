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

namespace gov.va.medora.mdws
{
    public class RestSessionMgr
    {
        #region Singleton Code
        static RestSessionMgr _mgr;

        private RestSessionMgr() { }

        public static RestSessionMgr Instance()
        {
            if (_mgr == null)
            {
                _mgr = new RestSessionMgr();
            }
            return _mgr;
        }
        #endregion

        public MySession getSession(string token)
        {
            if (_sessions == null)
            {
                _sessions = new Dictionary<string, MySession>();
            }
            if (String.IsNullOrEmpty(token))
            {
                token = gov.va.medora.utils.StringUtils.getNCharRandom(32);
                _sessions.Add(token, new MySession() { Token = token });
            }
            if (!_sessions.ContainsKey(token))
            {
                throw new ApplicationException("Don't try it");
            }
            return _sessions[token];
        }
        Dictionary<string, MySession> _sessions;
    }
}