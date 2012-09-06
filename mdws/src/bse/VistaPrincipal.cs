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

namespace gov.va.medora.mdws.bse
{
    public interface IPrincipal
    {
        string Name { get; }
        string Value { get; }
        string UserName { get; }
        string UserUid { get; }
        string MuiId { get; }
        string SiteName { get; }
        string SiteId { get; }
    }

    public class VistaPrincipal : IPrincipal
    {
        protected string myName;
        protected string mySiteId;
        protected string myValue;

        const int USERNAME_FLD = 2;
        const int USERDUZ_FLD = 5;
        const int MUIID_FLD = 1;
        const int SITENAME_FLD = 3;

        public VistaPrincipal(string siteId, string value)
        {
            mySiteId = siteId;
            myValue = value;
        }

        public string Name
        {
            get { return "vista:" + mySiteId; }
        }

        public string SiteId
        {
            get { return mySiteId; }
        }

        public string Value
        {
            get { return myValue; }
        }

        public static bool IsVistaPrincipal(IPrincipal principal)
        {
            return principal.GetType().IsInstanceOfType(typeof(VistaPrincipal));
        }

        public string UserName
        {
            get { return StringUtils.piece(myValue, StringUtils.CARET, USERNAME_FLD); }
        }

        public string UserUid
        {
            get { return StringUtils.piece(myValue, StringUtils.CARET, USERDUZ_FLD); }
        }

        public string MuiId
        {
            get { return StringUtils.piece(myValue, StringUtils.CARET, MUIID_FLD); }
        }

        public string SiteName
        {
            get { return StringUtils.piece(myValue, StringUtils.CARET, SITENAME_FLD); }
        }

    }
}
