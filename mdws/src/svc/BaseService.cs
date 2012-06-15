using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.ComponentModel;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    /// <summary>
    /// Summary description for BaseService
    /// </summary>
    [WebService(Namespace = "http://mdws.medora.va.gov")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public partial class BaseService : System.Web.Services.WebService
    {
        public const string VERSION = "1.1.0";

        public BaseService()
        {
            // If not Http request has been made yet Session is null
            // This happens before the Startup page is displayed
            if (HttpContext.Current.Session == null)
            {
                return;
            }

            // At this point a request has been made to a web service page
            if (HttpContext.Current.Session["MySession"] == null)
            {
                MySession = new MySession(this.GetType().Name);
                ApplicationSessions sessions = (ApplicationSessions)Application["APPLICATION_SESSIONS"];
                Application.Lock();
                sessions.ConfigurationSettings = MySession.MdwsConfiguration;
                Application.UnLock();
            }
        }

        protected MySession MySession
        {
            get { return (MySession)HttpContext.Current.Session["MySession"]; }
            set { HttpContext.Current.Session["MySession"] = value; }
        }

        [WebMethod(Description = "Get MDWS Version")]
        public string getVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().FullName;
        }

        [WebMethod(EnableSession = true, Description = "Add a data source for this session")]
        public SiteTO addDataSource(string id, string name, string datasource, string port, string modality, string protocol, string region)
        {
            SitesLib lib = new SitesLib(MySession);
            return lib.addSite(id, name, datasource, port, modality, protocol, region);
        }

        [WebMethod(Description = "Get current facade's version")]
        public TextTO getFacadeVersion()
        {
            TextTO result = new TextTO();
            try
            {
                System.Reflection.FieldInfo fi = this.GetType().GetField("VERSION");
                result.text = ((string)fi.GetValue(this));
            }
            catch (Exception)
            {
                result.fault = new FaultTO("This facade does not contain any version information"); 
            }
            return result;
        }

        [WebMethod(EnableSession = true, Description = "Set the current session's sites file")]
        public SiteArray setVha(string sitesFileName)
        {
            return MySession.setSites(sitesFileName);
        }

        [WebMethod(EnableSession = true, Description = "Get the executed RPCs from the base Vista connection")]
        public TextArray getRpcs()
        {
            return (TextArray)MySession.execute("ConnectionLib", "getRpcs", new object[] { });
        }
    }
}
