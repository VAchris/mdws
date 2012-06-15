using System;
using System.Web;
using System.Collections.Specialized;
using gov.va.medora.mdo;
using gov.va.medora.utils;
using gov.va.medora.mdo.api;
using gov.va.medora.mdws.dto;
using gov.va.medora.mdo.dao;
using System.Net.Mail;
using System.Net;

namespace gov.va.medora.mdws
{
    public class ToolsLib
    {
        MySession mySession;

        public ToolsLib(MySession mySession)
        {
            this.mySession = mySession;
        }

        public TextTO isRpcAvailable(string target, string context)
        {
            return isRpcAvailable(null,target,context);
        }

        public TextTO isRpcAvailable(string sitecode, string target, string context)
        {
            TextTO result = new TextTO();
            string msg = MdwsUtils.isAuthorizedConnection(mySession, sitecode);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            else if (target == "")
            {
                result.fault = new FaultTO("Missing target");
            }
            else if (context == "")
            {
                result.fault = new FaultTO("Missing context");
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
                ToolsApi api = new ToolsApi();
                string s = api.isRpcAvailable(cxn, target, context);
                result = new TextTO(s);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TextTO getVariableValue(string arg)
        {
            return getVariableValue(null,arg);
        }

        public TextTO getVariableValue(string sitecode, string arg)
        {
            TextTO result = new TextTO();
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
                ToolsApi api = new ToolsApi();
                string s = api.getVariableValue(cxn, arg);
                result = new TextTO(s);
            }
            catch (Exception e)
            {
                result.fault = new FaultTO(e.Message);
            }
            return result;
        }

        public TextTO sendEmail(string from, string to, string subject, string body, string isBodyHTML, string username, string password)
        {
            TextTO result = new TextTO();
            if (String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to) ||
                String.IsNullOrEmpty(subject) || String.IsNullOrEmpty(body))
            {
                result.fault = new FaultTO("Must supply all parameters", "Supply a value for each of the arguments");
            }
            if (result.fault != null)
            {
                return result;
            }

            string host = "smtp.va.gov";
            int port = 25;
            bool enableSsl = false;
            bool useDefaultCredentials = false;

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from);
                if (!String.IsNullOrEmpty(isBodyHTML))
                {
                    message.IsBodyHtml = isBodyHTML.ToUpper().Equals("TRUE") ? true : false;
                }
                message.Body = body;
                message.Subject = subject;
                //to contains comma seperated email addresses
                message.To.Add(to);

                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.UseDefaultCredentials = useDefaultCredentials;

                if (!useDefaultCredentials && !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
                {
                    smtpClient.Credentials = new NetworkCredential(username, password);
                }

                smtpClient.Send(message);
                result.text = "OK";
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc.Message);
            }
            return result;
        }

        public TextArray ddrGetsEntry(string file, string iens, string flds, string flags)
        {
            TextArray result = new TextArray();
            string msg = MdwsUtils.isAuthorizedConnection(mySession);
            if (msg != "OK")
            {
                result.fault = new FaultTO(msg);
            }
            if (result.fault != null)
            {
                return result;
            }

            try
            {
                ToolsApi api = new ToolsApi();
                string[] response = api.ddrGetsEntry(mySession.ConnectionSet.BaseConnection, file, iens, flds, flags);
                return new TextArray(response);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
                return result;
            }
        }

        public TaggedTextArray runRpc(string rpcName, string[] paramValues, int[] paramTypes, bool[] paramEncrypted)
        {
            TaggedTextArray result = new TaggedTextArray();

            try
            {
                ToolsApi api = new ToolsApi();
                IndexedHashtable s = api.runRpc(mySession.ConnectionSet, rpcName, paramValues, paramTypes, paramEncrypted);
                return new TaggedTextArray(s);
            }
            catch (Exception exc)
            {
                result.fault = new FaultTO(exc);
                return result;
            }
        }
    }
}
