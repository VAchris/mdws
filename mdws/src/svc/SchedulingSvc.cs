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

using System.Web.Services;
using System.ComponentModel;
using gov.va.medora.mdws.dto;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace gov.va.medora.mdws
{
    // WSE
    //[ServiceContract(Namespace = "http://mdws.medora.va.gov/SchedulingSvc")]
    [WebService(Namespace = "http://mdws.medora.va.gov/SchedulingSvc")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // WCF
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class SchedulingSvc : BaseService, ISchedulingSvc
    {
        public SchedulingSvc() : base() { }

        [WebMethod(EnableSession = true, Description = "Get list of clinics")]
        public TaggedHospitalLocationArray getClinics(string target)
        {
            return (TaggedHospitalLocationArray)MySession.execute("EncounterLib", "getClinics", new object[] { target });
        }

        [WebMethod(EnableSession = true, Description = "Get clinic availability information")]
        public TextTO getClinicAvailability(string clinicId)
        {
            return (TextTO)MySession.execute("SchedulingLib", "getClinicAvailability", new object[] { clinicId });
        }

        [WebMethod(EnableSession = true, Description = "Get clinic scheduling information. e.g. availability, default appointment length, etc.")]
        public HospitalLocationTO getClinicSchedulingDetails(string clinicId)
        {
            return (HospitalLocationTO)MySession.execute("SchedulingLib", "getClinicSchedulingDetails", new object[] { clinicId });
        }

        [WebMethod(EnableSession = true, Description = "Get list of appointment types")]
        public AppointmentTypeArray getAppointmentTypes(string target)
        {
            return (AppointmentTypeArray)MySession.execute("SchedulingLib", "getAppointmentTypes", new object[] { target });
        }

        [WebMethod(EnableSession = true, Description = "Schedule an appointment")]
        public AppointmentTO makeAppointment(string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength)
        {
            return (AppointmentTO)MySession.execute("SchedulingLib", "makeAppointment", new object[] { clinicId, appointmentType, appointmentTimestamp, appointmentLength });
        }

        [WebMethod(EnableSession = true, Description = "Get a patient's pending appointments")]
        public TaggedAppointmentArray getPendingAppointments(string startDate)
        {
            return (TaggedAppointmentArray)MySession.execute("SchedulingLib", "getPendingAppointments", new object[] { startDate });
        }

        [WebMethod(EnableSession = true, Description = "Get a list of patients by clinic")]
        public PatientArray getPatientsByClinic(string clinicId, string startDate, string stopDate)
        {
            return (PatientArray)MySession.execute("SchedulingLib", "getPatientsByClinic", new object[] { clinicId, startDate, stopDate });
        }
    }

    // WCF
    [ServiceContract(Namespace = "http://mdws.medora.va.gov/SchedulingSvc", SessionMode = SessionMode.Allowed)]
    public interface ISchedulingSvc
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "clinics/{id}")]
        TaggedHospitalLocationArray getClinics(string target);

        [OperationContract]
        TextTO getClinicAvailability(string clinicId);

        [OperationContract]
        HospitalLocationTO getClinicSchedulingDetails(string clinicId);

        [OperationContract]
        AppointmentTypeArray getAppointmentTypes(string target);

        [OperationContract]
        AppointmentTO makeAppointment(string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength);

        [OperationContract]
        TaggedAppointmentArray getPendingAppointments(string startDate);

        [OperationContract]
        PatientArray getPatientsByClinic(string clinicId, string startDate, string stopDate);
    }
}