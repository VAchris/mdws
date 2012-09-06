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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gov.va.medora.mdws.Web
{
    /// <summary>
    /// This page displays summary information about the current MDWS sessions. Session information
    /// is added to this page as new sessions are requested by client applications. You must be a member
    /// of the local admin group to view the contents of this page.
    /// </summary>
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!HttpContext.Current.User.IsInRole("Builtin\\Administrators"))
                //{
                //    labelMessage.Text = "You must be a member of the server's Administrators group to view this page. " +
                //        "(logged on as: " + HttpContext.Current.User.Identity.Name + ")";
                //    panelDashboard.Visible = false;
                //    return;
                //}
                //else
                //{
                    panelDashboard.Visible = true;
                    Application.Lock();
                    ApplicationSessions sessions = Application["APPLICATION_SESSIONS"] as ApplicationSessions;

                    Application.UnLock();

                    TimeSpan upTime = DateTime.Now.Subtract(sessions.Start);
                    labelUpTime.Text = upTime.Days + " Days, " + upTime.Hours + " Hours &amp; " + upTime.Minutes + " Minutes";
                    repeaterSession.DataSource = sessions.Sessions.Values;
                    repeaterSession.DataBind();
                    labelSessionCount.Text = sessions.Sessions.Count.ToString();
                //}
            }
            catch (Exception exc)
            {
                labelMessage.Text = "Oops! An unexpected error occurred. <br />" + exc.Message;
            }
        }

        protected string getDuration(object start)
        {
            return (DateTime.Now.Subtract((DateTime)start)).ToString();
        }
    }
}
