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
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using gov.va.medora.mdo;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    public class QuestionnaireLib
    {
        MySession mySession;

        public QuestionnaireLib(MySession theSession)
        {
            mySession = theSession;
        }

        public QuestionnaireSetTO getQuestionnaireSet(string name)
        {
            QuestionnaireSetTO result = new QuestionnaireSetTO();
            if (String.IsNullOrEmpty(name))
            {
                result.fault = new FaultTO("Missing set name");
                return result;
            }
            try
            {
                QuestionnaireSet mdo = QuestionnaireSet.getSet(name);
                result = new QuestionnaireSetTO(mdo);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }
    }
}
