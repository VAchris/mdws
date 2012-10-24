using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using gov.va.medora.mdws.dto;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace gov.va.medora.mdws
{
    // WSE
    [ServiceContract(Namespace = "http://mdws.medora.va.gov/SchedulingSvc")]
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
            return (TextTO)MySession.execute("EncounterLib", "getClinicAvailability", new object[] { clinicId });
        }

        [WebMethod(EnableSession = true, Description = "Get clinic scheduling information. e.g. availability, default appointment length, etc.")]
        public HospitalLocationTO getClinicSchedulingDetails(string clinicId)
        {
            //return new HospitalLocationTO() { fault = new FaultTO(new NotImplementedException()) };
            return (HospitalLocationTO)MySession.execute("EncounterLib", "getClinicSchedulingDetails", new object[] { clinicId });
        }

        [WebMethod(EnableSession = true, Description = "Get list of appointment types")]
        public AppointmentTypeArray getAppointmentTypes(string target)
        {
            return (AppointmentTypeArray)MySession.execute("EncounterLib", "getAppointmentTypes", new object[] { target });
        }

        [WebMethod(EnableSession = true, Description = "Schedule an appointment")]
        public AppointmentTO makeAppointment(string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength)
        {
            return (AppointmentTO)MySession.execute("EncounterLib", "makeAppointment", new object[] { clinicId, appointmentType, appointmentTimestamp, appointmentLength });
        }
    }

    // WCF
    [ServiceContract(Namespace = "http://mdws.medora.va.gov/SchedulingSvc", SessionMode = SessionMode.Allowed)]
    public interface ISchedulingSvc
    {
        [OperationContract]
        TaggedHospitalLocationArray getClinics(string target);

        [OperationContract]
        TextTO getClinicAvailability(string clinicId);

        [OperationContract]
        AppointmentTO makeAppointment(string clinicId, string appointmentType, string appointmentTimestamp, string appointmentLength);

        [OperationContract]
        AppointmentTypeArray getAppointmentTypes(string target);
    }
}