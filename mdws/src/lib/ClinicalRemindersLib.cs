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
using System.Collections.Specialized;
using gov.va.medora.mdo;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    public class ClinicalRemindersLib
    {
        MySession mySession;

        public ClinicalRemindersLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TextArray getReminderReportTemplates()
        {
            TextArray result = new TextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                string[] templates = ClinicalReminder.getReminderReportTemplates(mySession.ConnectionSet.BaseConnection);
                result = new TextArray(templates);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getActiveReminderReports()
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                OrderedDictionary d = ClinicalReminder.getActiveReminderReports(mySession.ConnectionSet.BaseConnection);
                result = new TaggedTextArray(d);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public ReminderReportPatientListTO getPatientListForReminderReport(string rptId)
        {
            ReminderReportPatientListTO result = new ReminderReportPatientListTO();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(rptId))
            {
                result.fault = new FaultTO("Empty rptId");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                ReminderReportPatientList lst = ClinicalReminder.getPatientListForReminderReport(mySession.ConnectionSet.BaseConnection, rptId);
                result = new ReminderReportPatientListTO(lst);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }
    }
}