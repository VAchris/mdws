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
using System.Collections.Specialized;
using gov.va.medora.mdws.dto;
using gov.va.medora.mdo;
using gov.va.medora.mdo.api;
using gov.va.medora.mdo.dao;
using System.Collections.Generic;

namespace gov.va.medora.mdws
{
    public class EncounterLib
    {
        MySession mySession;

        public EncounterLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TaggedPatientArrays getPatientsWithUpdatedFutureAppointments(string username, string pwd, string updatedSince)
        {
            TaggedPatientArrays result = new TaggedPatientArrays();
            //if (String.IsNullOrEmpty(username) | String.IsNullOrEmpty(pwd) | String.IsNullOrEmpty(updatedSince))
            //{
            //    result.fault = new FaultTO("Must supply all arguments");   
            //}
            try
            {
                EncounterApi api = new EncounterApi();
                DataSource ds = new DataSource { ConnectionString = mySession.MdwsConfiguration.CdwConnectionString }; 
                AbstractConnection cxn = new gov.va.medora.mdo.dao.sql.cdw.CdwConnection(ds);
                Dictionary<string, HashSet<string>> dict = api.getUpdatedFutureAppointments(cxn, DateTime.Parse(updatedSince));
                result.arrays = new TaggedPatientArray[dict.Keys.Count];
                int arrayCount = 0;

                foreach (string key in dict.Keys)
                {
                    TaggedPatientArray tpa = new TaggedPatientArray(key);
                    tpa.patients = new PatientTO[dict[key].Count];
                    int patientCount = 0;
                    foreach (string patientICN in dict[key])
                    {
                        PatientTO p = new PatientTO { mpiPid = patientICN };
                        tpa.patients[patientCount] = p;
                        patientCount++;
                    }
                    result.arrays[arrayCount] = tpa;
                    arrayCount++;
                }
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }
            return result;
        }

        public TaggedHospitalLocationArray getLocations(string target, string direction)
        {
            return getLocations(null, target, direction);
        }

        public TaggedHospitalLocationArray getLocations(string sitecode, string target, string direction)
        {
            TaggedHospitalLocationArray result = new TaggedHospitalLocationArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            if (direction == "")
            {
                direction = "1";
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                HospitalLocation[] locations = api.getLocations(cxn,target,direction);
                result = new TaggedHospitalLocationArray(sitecode,locations);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedVisitArray getVisits(string fromDate, string toDate)
        {
            return getVisits(null, fromDate, toDate);
        }

        public TaggedVisitArray getVisits(string sitecode, string fromDate, string toDate)
        {
            TaggedVisitArray result = new TaggedVisitArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                toDate = DateTime.Today.ToString("yyyyMMdd");
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                Visit[] v = api.getVisits(cxn, fromDate, toDate);
                result = new TaggedVisitArray(sitecode, v);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedInpatientStayArray getAdmissions()
        {
            return getAdmissions(null);
        }

        public TaggedInpatientStayArray getAdmissions(string sitecode)
        {
            TaggedInpatientStayArray result = new TaggedInpatientStayArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                InpatientStay[] stays = api.getAdmissions(cxn);
                result = new TaggedInpatientStayArray(sitecode, stays);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TextTO getNoteEncounterString(string noteId)
        {
            return getNoteEncounterString(null, noteId);
        }

        public TextTO getNoteEncounterString(string sitecode, string noteId)
        {
            TextTO result = new TextTO();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            else if (noteId == "")
            {
                result.fault = new FaultTO("Missing noteId");
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                NoteApi api = new NoteApi();
                result.text = api.getNoteEncounterString(cxn, noteId);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedHospitalLocationArray getWards()
        {
            return getWards(null);
        }

        public TaggedHospitalLocationArray getWards(string sitecode)
        {
            TaggedHospitalLocationArray result = new TaggedHospitalLocationArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                HospitalLocation[] wards = api.getWards(cxn);
                result = new TaggedHospitalLocationArray(sitecode, wards);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedHospitalLocationArray getClinics(string target)
        {
            return getClinics(null, target, null);
        }

        public TaggedHospitalLocationArray getClinics(string target, string direction)
        {
            return getClinics(null, target, direction);
        }

        public TaggedHospitalLocationArray getClinics(string sitecode, string target, string direction)
        {
            TaggedHospitalLocationArray result = new TaggedHospitalLocationArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                HospitalLocation[] clinics = api.getClinics(cxn, target, direction);
                result = new TaggedHospitalLocationArray(sitecode, clinics);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedText getSpecialties()
        {
            return getSpecialties(null);
        }

        public TaggedText getSpecialties(string sitecode)
        {
            TaggedText result = new TaggedText();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                DictionaryHashList specialties = api.getSpecialties(cxn);
                result = new TaggedText(sitecode, specialties);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedText getTeams()
        {
            return getTeams(null);
        }

        public TaggedText getTeams(string sitecode)
        {
            TaggedText result = new TaggedText();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                DictionaryHashList teams = api.getTeams(cxn);
                result = new TaggedText(sitecode, teams);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedTextArray getOutpatientEncounterReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getOutpatientEncounterReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TaggedTextArray getAdmissionsReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getAdmissionsReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getExpandedAdtReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getExpandedAdtReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getDischargeDiagnosisReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getDischargeDiagnosisReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getDischargesReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getDischargesReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getTransfersReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getTransfersReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getFutureClinicVisitsReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getFutureClinicVisitsReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getPastClinicVisitsReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getPastClinicVisitsReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getTreatingSpecialtyReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getTreatingSpecialtyReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getCompAndPenReports(string fromDate, string toDate, int nrpts)
        {
            TaggedTextArray result = new TaggedTextArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getCompAndPenReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedTextArray getCareTeamReports()
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
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getCareTeamReports(mySession.ConnectionSet);
                result = new TaggedTextArray(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedIcdRptArrays getIcdProceduresReports(string fromDate, string toDate, int nrpts)
        {
            TaggedIcdRptArrays result = new TaggedIcdRptArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getIcdProceduresReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedIcdRptArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedIcdRptArrays getIcdSurgeryReports(string fromDate, string toDate, int nrpts)
        {
            TaggedIcdRptArrays result = new TaggedIcdRptArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getIcdSurgeryReports(mySession.ConnectionSet, fromDate, toDate, nrpts);
                result = new TaggedIcdRptArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedAppointmentArrays getAppointments()
        {
            TaggedAppointmentArrays result = new TaggedAppointmentArrays();

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
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getAppointments(mySession.ConnectionSet);
                return new TaggedAppointmentArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
                return result;
            }
        }

        public TextTO getAppointmentText(string siteId, string apptId)
        {
            TextTO result = new TextTO();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (siteId == "")
            {
                result.fault = new FaultTO("Missing siteId");
            }
            else if (apptId == "")
            {
                result.fault = new FaultTO("Missing apptId");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                string s = api.getAppointmentText(mySession.ConnectionSet.getConnection(siteId), apptId);
                result = new TextTO(s);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedDrgArrays getDRGRecords()
        {
            TaggedDrgArrays result = new TaggedDrgArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getDRGRecords(mySession.ConnectionSet);
                result = new TaggedDrgArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedAdtArrays getInpatientMoves()
        {
            TaggedAdtArrays result = new TaggedAdtArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getInpatientMoves(mySession.ConnectionSet);
                result = new TaggedAdtArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedAdtArrays getInpatientMoves(string fromDate, string toDate)
        {
            TaggedAdtArrays result = new TaggedAdtArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getInpatientMoves(mySession.ConnectionSet, fromDate, toDate);
                result = new TaggedAdtArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedAdtArrays getInpatientMoves(string fromDate, string toDate, string iterLength)
        {
            TaggedAdtArrays result = new TaggedAdtArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getInpatientMoves(mySession.ConnectionSet, fromDate, toDate, iterLength);
                result = new TaggedAdtArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        // This needs to be changed to a single-site call.
        public TaggedAdtArrays getInpatientMovesByCheckinId(string checkinId)
        {
            TaggedAdtArrays result = new TaggedAdtArrays();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getInpatientMovesByCheckinId(mySession.ConnectionSet, checkinId);
                result = new TaggedAdtArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedAdtArray getInpatientDischarges(string sitecode, string pid)
        {
            TaggedAdtArray result = new TaggedAdtArray();

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
                EncounterApi api = new EncounterApi();
                Adt[] adts = api.getInpatientDischarges(mySession.ConnectionSet.getConnection(sitecode), pid);
                result = new TaggedAdtArray(sitecode, adts);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public TaggedInpatientStayArrays getStayMovementsByDateRange(string fromDate, string toDate)
        {
            TaggedInpatientStayArrays result = new TaggedInpatientStayArrays();
            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (fromDate == "")
            {
                result.fault = new FaultTO("Missing fromDate");
            }
            else if (toDate == "")
            {
                result.fault = new FaultTO("Missing toDate");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getStayMovementsByDateRange(mySession.ConnectionSet,fromDate,toDate);
                result = new TaggedInpatientStayArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public InpatientStayTO getStayMovements(string checkinId)
        {
            return getStayMovements(null, checkinId);
        }

        public InpatientStayTO getStayMovements(string sitecode, string checkinId)
        {
            InpatientStayTO result = new InpatientStayTO();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            else if (checkinId == "")
            {
                result.fault = new FaultTO("Missing checkinId");
            }
            if (result.fault != null)
            {
                return result;
            }

            if (sitecode == null)
            {
                sitecode = mySession.ConnectionSet.BaseSiteId;
            }

            try
            {
                AbstractConnection cxn = mySession.ConnectionSet.getConnection(sitecode);
                EncounterApi api = new EncounterApi();
                InpatientStay stay = api.getStayMovements(cxn, checkinId);
                result = new InpatientStayTO(stay);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }
        public TaggedInpatientStayArrays getStayMovementsByPatient()
        {
            TaggedInpatientStayArrays result = new TaggedInpatientStayArrays();
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
                EncounterApi api = new EncounterApi();
                IndexedHashtable t = api.getStayMovementsByPatient(mySession.ConnectionSet, mySession.Patient.LocalPid);
                result = new TaggedInpatientStayArrays(t);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public SiteArray getSiteDivisions(string sitecode)
        {
            SiteArray result = new SiteArray();
            if (sitecode == "")
            {
                result.fault = new FaultTO("Missing sitecode");
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                EncounterApi api = new EncounterApi();
                Site[] sites = api.getSiteDivisions(mySession.ConnectionSet.getConnection(sitecode), sitecode);
                result = new SiteArray(sites);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e);
            }
            return result;
        }

        public AppointmentTypeArray getAppointmentTypes(string target)
        {
            AppointmentTypeArray result = new AppointmentTypeArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            if (String.IsNullOrEmpty(target))
            {
                target = "A";
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                IList<AppointmentType> types = new EncounterApi().getAppointmentTypes(mySession.ConnectionSet.BaseConnection, target);
                result = new AppointmentTypeArray(types);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }

        public TaggedAppointmentArray getPendingAppointments(string startDate)
        {
            TaggedAppointmentArray result = new TaggedAppointmentArray();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(startDate))
            {
                result.fault = new FaultTO("Missing startDate");
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                IList<Appointment> appts = new EncounterApi().getPendingAppointments(mySession.ConnectionSet.BaseConnection, startDate);
                result = new TaggedAppointmentArray(mySession.ConnectionSet.BaseConnection.DataSource.SiteId.Id, appts);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }

        public TextTO getClinicAvailability(string clinicId)
        {
            TextTO result = new TextTO();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(clinicId))
            {
                result.fault = new FaultTO("Missing clinic ID");
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                string availabilityString = new EncounterApi().getClinicAvailability(mySession.ConnectionSet.BaseConnection, clinicId);
                result = new TextTO(availabilityString);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }

        public AppointmentTO makeAppointment(string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength)
        {
            AppointmentTO result = new AppointmentTO();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(clinicId))
            {
                result.fault = new FaultTO("Missing clinic ID");
            }
            else if (String.IsNullOrEmpty(appointmentTimestamp))
            {
                result.fault = new FaultTO("Missing appointment timestamp");
            }
            else if (String.IsNullOrEmpty(appointmentType))
            {
                result.fault = new FaultTO("Missing appointment type");
            }
            else if (String.IsNullOrEmpty(appointmentLength))
            {
                result.fault = new FaultTO("Missing appointment length");
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                Appointment appt = new Appointment()
                {
                    AppointmentType = new AppointmentType() { ID = appointmentType },
                    Clinic = new HospitalLocation() { Id = clinicId },
                    Length = appointmentLength,
                    Timestamp = appointmentTimestamp
                };
                appt = new EncounterApi().makeAppointment(mySession.ConnectionSet.BaseConnection, appt);
                result = new AppointmentTO(appt);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }

        public HospitalLocationTO getClinicSchedulingDetails(string clinicId)
        {
            HospitalLocationTO result = new HospitalLocationTO();

            if (!mySession.ConnectionSet.IsAuthorized)
            {
                result.fault = new FaultTO("Connections not ready for operation", "Need to login?");
            }
            else if (String.IsNullOrEmpty(clinicId))
            {
                result.fault = new FaultTO("Missing clinic ID");
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                result = new HospitalLocationTO(new EncounterApi().getClinicSchedulingDetails(mySession.ConnectionSet.BaseConnection, clinicId));
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }
    }
}
