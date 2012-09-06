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
using System.Web;
using System.Collections.Specialized;
using System.Collections;
using gov.va.medora.mdws.dto;
using gov.va.medora.mdo.api;
using gov.va.medora.mdo;
using gov.va.medora.utils;
using gov.va.medora.mdo.dao.soap.cds;

namespace gov.va.medora.mdws
{
    public class MhvLib
    {
        MySession mySession;

        public MhvLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TextTO getHealthSummary(string pwd, string sitecode, string mpiPid, string displayName)
        {
            TextTO result = new TextTO();

            if (String.IsNullOrEmpty(sitecode))
            {
                result.fault = new FaultTO("Missing sitecode");
            }
            else if (mpiPid == "")
            {
                result.fault = new FaultTO("Missing mpiPid");
            }
            else if (displayName == "")
            {
                result.fault = new FaultTO("Missing displayName");
            }
            if (result.fault != null)
            {
                return result;
            }

            AccountLib acctLib = new AccountLib(mySession);
            try
            {
                SiteArray sites = acctLib.patientVisit(pwd, sitecode, mpiPid, false);
                if (sites.fault != null)
                {
                    result.fault = sites.fault;
                    return result;
                }

                // Get the labs...
                ClinicalLib clinicalLib = new ClinicalLib(mySession);
                result = clinicalLib.getAdHocHealthSummaryByDisplayName(sitecode, displayName);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            finally
            {
                mySession.close();
            }
            return result;
        }

        public TaggedChemHemRptArray getChemHemReportsByReportDateFromSite(
            string pwd, string sitecode, string mpiPid, string fromDate, string toDate)
        {
            TaggedChemHemRptArray result = new TaggedChemHemRptArray();

            if (String.IsNullOrEmpty(sitecode))
            {
                result.fault = new FaultTO("Missing sitecode");
            }
            else if (mpiPid == "")
            {
                result.fault = new FaultTO("Missing mpiPid");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            if (toDate == "")
            {
                toDate = DateTime.Now.ToString("yyyyMMdd");
            }

            AccountLib acctLib = new AccountLib(mySession);
            try
            {
                SiteArray sites = acctLib.patientVisit(pwd, sitecode, mpiPid, false);
                if (sites.fault != null)
                {
                    result.fault = sites.fault;
                    return result;
                }

                // Get the labs...
                ChemHemReport[] rpts = ChemHemReport.getChemHemReports(mySession.ConnectionSet.getConnection(sitecode), fromDate, toDate);
                result = new TaggedChemHemRptArray(sitecode, rpts);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            finally
            {
                mySession.close();
            }
            return result;
        }

        public TaggedAppointmentArray getAppointmentsFromSite(
            string pwd, string sitecode, string mpiPid)
        {
            TaggedAppointmentArray result = new TaggedAppointmentArray();

            if (String.IsNullOrEmpty(sitecode))
            {
                result.fault = new FaultTO("Missing sitecode");
            }
            else if (String.IsNullOrEmpty(mpiPid))
            {
                result.fault = new FaultTO("Missing mpiPid");
            }
            if (result.fault != null)
            {
                return result;
            }

            AccountLib acctLib = new AccountLib(mySession);
            try
            {
                SiteArray sites = acctLib.patientVisit(pwd, sitecode, mpiPid, false);
                if (sites.fault != null)
                {
                    result.fault = sites.fault;
                    return result;
                }

                // Get the labs...
                EncounterApi api = new EncounterApi();
                Appointment[] appts = api.getAppointments(mySession.ConnectionSet.getConnection(sitecode));
                for (int i = 0; i < appts.Length; i++)
                {
                    appts[i].Status = undecodeApptStatus(appts[i].Status);
                }
                result = new TaggedAppointmentArray(sitecode, appts);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            finally
            {
                mySession.close();
            }
            return result;
        }

        internal string undecodeApptStatus(string status)
        {
            if (status == "NO-SHOW")
            {
                return "N";
            }
            if (status == "CANCELLED BY CLINIC")
            {
                return "C";
            }
            if (status == "NO-SHOW & AUTO RE-BOOK")
            {
                return "NA";
            }
            if (status == "CANCELLED BY CLINIC & AUTO RE-BOOK")
            {
                return "CA";
            }
            if (status == "INPATIENT APPOINTMENT")
            {
                return "I";
            }
            if (status == "CANCELLED BY PATIENT")
            {
                return "PC";
            }
            if (status == "CANCELLED BY PATIENT & AUTO-REBOOK")
            {
                return "PCA";
            }
            if (status == "NO ACTION TAKEN")
            {
                return "NT";
            }
            return status;
        }

        public TextTO getAllergiesAsXML(string appPwd, string patientICN)
        {
            TextTO result = new TextTO();

            if (mySession == null || mySession.SiteTable == null || mySession.SiteTable.getSite("201") == null ||
                mySession.SiteTable.getSite("201").Sources == null || mySession.SiteTable.getSite("201").Sources[0] == null)
            {
                result.fault = new FaultTO("No CDS endpoint (site 201) in sites file!");
            }
            if (result.fault != null)
            {
                return result;
            }

            CdsConnection cxn = new CdsConnection(mySession.SiteTable.getSite("201").Sources[0]);
            cxn.Pid = patientICN;
            CdsClinicalDao dao = new CdsClinicalDao(cxn);

            try
            {
                result.text = dao.getAllergiesAsXML();
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }
            return result;
        }

        public AllergyTO[] getAllergies(string appPwd, string patientICN)
        {
            AllergyTO[] result = new AllergyTO[1];
            result[0] = new AllergyTO() { fault = new FaultTO("Not implemented") };
            return result;
        }

        public TextTO getLabReportsAsXML(string appPwd, string patientICN, string fromDate, string toDate)
        {
            TextTO result = new TextTO();

            if (mySession == null || mySession.SiteTable == null || mySession.SiteTable.getSite("201") == null ||
                mySession.SiteTable.getSite("201").Sources == null || mySession.SiteTable.getSite("201").Sources[0] == null)
            {
                result.fault = new FaultTO("No CDS endpoint (site 201) in sites file!");
            }
            if (result.fault != null)
            {
                return result;
            }

            CdsConnection cxn = new CdsConnection(mySession.SiteTable.getSite("201").Sources[0]);
            cxn.Pid = patientICN;
            CdsLabsDao dao = new CdsLabsDao(cxn);

            try
            {
                // TODO - validate app password
                result.text = dao.getAllLabReports(fromDate, toDate, 0); // function is probably ignoring these params
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }
            return result;
        }

        public LabResultTO[] getLabReports(string appPwd, string patientICN, string fromDate, string toDate)
        {
            LabResultTO[] result = new LabResultTO[1];
            result[0] = new LabResultTO() { fault = new FaultTO("Not implemented") };
            return result;
        }
    }
}
