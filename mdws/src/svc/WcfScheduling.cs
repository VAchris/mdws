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
using gov.va.medora.mdws.dto;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace gov.va.medora.mdws
{
    // WCF
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class WcfSchedulingSvc : IWcfSchedulingSvc
    {
        public WcfSchedulingSvc() { }

        public TaggedHospitalLocationArray getClinics(string token, string target)
        {
            return (TaggedHospitalLocationArray)RestSessionMgr.Instance().getSession(token).execute("EncounterLib", "getClinics", new object[] { target });
        }

        public TextTO getClinicAvailability(string token, string clinicId)
        {
            return (TextTO)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "getClinicAvailability", new object[] { clinicId });
        }

        public HospitalLocationTO getClinicSchedulingDetails(string token, string clinicId)
        {
            return (HospitalLocationTO)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "getClinicSchedulingDetails", new object[] { clinicId });
        }

        public AppointmentTypeArray getAppointmentTypes(string token, string target)
        {
            return (AppointmentTypeArray)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "getAppointmentTypes", new object[] { target });
        }

        public AppointmentTO makeAppointment(string token, string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength)
        {
            return (AppointmentTO)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "makeAppointment", new object[] { clinicId, appointmentType, appointmentTimestamp, appointmentLength });
        }

        public TaggedAppointmentArray getPendingAppointments(string token, string startDate)
        {
            return (TaggedAppointmentArray)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "getPendingAppointments", new object[] { startDate });
        }

        public PatientArray getPatientsByClinic(string token, string clinicId, string startDate, string stopDate)
        {
            return (PatientArray)RestSessionMgr.Instance().getSession(token).execute("SchedulingLib", "getPatientsByClinic", new object[] { clinicId, startDate, stopDate });
        }

        public RegionArray getVHA()
        {
            throw new NotImplementedException();
        }

        public DataSourceArray connect(string sitelist)
        {
            MySession newSession = RestSessionMgr.Instance().getSession("");
            DataSourceArray response = (DataSourceArray)newSession.execute("ConnectionLib", "connectToLoginSite", new object[] { sitelist });
            WebOperationContext.Current.OutgoingResponse.Headers.Add("token", newSession.Token);
            return response;
        }

        public TaggedTextArray disconnect(string token)
        {
            throw new NotImplementedException();
        }

        public UserTO login(string token, string username, string pwd, string context)
        {
            throw new NotImplementedException();
        }
    }

    // WCF
    [ServiceContract(Namespace = "http://mdws.medora.va.gov/SchedulingSvc", SessionMode = SessionMode.Allowed)]
    public interface IWcfSchedulingSvc
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getVHA")]
        RegionArray getVHA();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "connect/{sitelist}")]
        DataSourceArray connect(string sitelist);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "disconnect&token={token}")]
        TaggedTextArray disconnect(string token);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "login/username={username}&pwd={pwd}&context={context}&token={token}")]
        UserTO login(string token, string username, string pwd, string context);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "clinics/{target}&token={token}")]
        TaggedHospitalLocationArray getClinics(string token, string target);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getClinicAvailability/{clinicId}&token={token}")]
        TextTO getClinicAvailability(string token, string clinicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getClinicSchedulingDetails/{clinicId}&token={token}")]
        HospitalLocationTO getClinicSchedulingDetails(string token, string clinicId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAppointmentTypes/{target}&token={token}")]
        AppointmentTypeArray getAppointmentTypes(string token, string target);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "makeAppointment?clinicId={clinicId}&appointmentType={appointmentType}&appointmentTimestamp={appointmentTimestamp}&appointmentLength={appointmentLength}&token={token}")]
        AppointmentTO makeAppointment(string token, string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getPendingAppointments/{startDate}&token={token}")]
        TaggedAppointmentArray getPendingAppointments(string token, string startDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getPatientsByClinic?clinicId={clinicId}&startDate={startDate}&stopDate={stopDate}&token={token}")]
        PatientArray getPatientsByClinic(string token, string clinicId, string startDate, string stopDate);
    }
}