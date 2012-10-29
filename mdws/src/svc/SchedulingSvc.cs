using System.Web.Services;
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

        [WebMethod(EnableSession = true, Description = "Get all VHA sites")]
        public RegionArray getVHA()
        {
            return (RegionArray)MySession.execute("SitesLib", "getVHA", new object[] { });
        }

        [WebMethod(EnableSession = true, Description = "Connect to a single VistA system.")]
        public DataSourceArray connect(string sitelist)
        {
            return (DataSourceArray)MySession.execute("ConnectionLib", "connectToLoginSite", new object[] { sitelist });
        }

        [WebMethod(EnableSession = true, Description = "Log onto a single VistA system.")]
        public UserTO login(string username, string pwd, string context)
        {
            return (UserTO)MySession.execute("AccountLib", "login", new object[] { username, pwd, context });
        }

        [OperationContract]
        [WebMethod(EnableSession = true, Description = "Disconnect from single Vista system.")]
        public TaggedTextArray disconnect()
        {
            return (TaggedTextArray)MySession.execute("ConnectionLib", "disconnectAll", new object[] { });
        }

        [WebMethod(EnableSession = true, Description = "Select a patient at logged in site.")]
        public PatientTO select(string DFN)
        {
            return (PatientTO)MySession.execute("PatientLib", "select", new object[] { DFN });
        }

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