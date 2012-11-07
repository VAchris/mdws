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
using gov.va.medora.mdws.dto;
using gov.va.medora.mdo;
using gov.va.medora.mdo.api;

namespace gov.va.medora.mdws
{
    public class SchedulingLib
    {
        MySession _mySession;

        public SchedulingLib(MySession mySession)
        {
            _mySession = mySession;
        }

        public TaggedHospitalLocationArray getClinics(string target)
        {
            return new EncounterLib(_mySession).getClinics(target);
        }

        public AppointmentTypeArray getAppointmentTypes(string target)
        {
            AppointmentTypeArray result = new AppointmentTypeArray();

            if (!_mySession.ConnectionSet.IsAuthorized)
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
                IList<AppointmentType> types = new EncounterApi().getAppointmentTypes(_mySession.ConnectionSet.BaseConnection, target);
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

            if (!_mySession.ConnectionSet.IsAuthorized)
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
                IList<Appointment> appts = new EncounterApi().getPendingAppointments(_mySession.ConnectionSet.BaseConnection, startDate);
                result = new TaggedAppointmentArray(_mySession.ConnectionSet.BaseConnection.DataSource.SiteId.Id, appts);
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

            if (!_mySession.ConnectionSet.IsAuthorized)
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
                string availabilityString = new EncounterApi().getClinicAvailability(_mySession.ConnectionSet.BaseConnection, clinicId);
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

            if (!_mySession.ConnectionSet.IsAuthorized)
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
                appt = new EncounterApi().makeAppointment(_mySession.ConnectionSet.BaseConnection, appt);
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

            if (!_mySession.ConnectionSet.IsAuthorized)
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
                result = new HospitalLocationTO(new EncounterApi().getClinicSchedulingDetails(_mySession.ConnectionSet.BaseConnection, clinicId));
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
            }

            return result;
        }

        public PatientArray getPatientsByClinic(string clinicId, string startDate, string stopDate)
        {
            PatientArray result = new PatientArray();

            string msg = MdwsUtils.isAuthorizedConnection(_mySession);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            else if (String.IsNullOrEmpty(clinicId))
            {
                result.fault = new FaultTO("Missing clinicId");
            }

            if (result.fault != null)
            {
                return result;
            }

            try
            {
                Patient[] matches = new PatientApi().getPatientsByClinic(_mySession.ConnectionSet.BaseConnection, clinicId, startDate, stopDate);
                result = new PatientArray(matches);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }

            return result;
        }


    }
}