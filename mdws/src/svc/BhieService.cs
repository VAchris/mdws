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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using gov.va.medora.mdws.dto;

namespace gov.va.medora.mdws
{
    /// <summary>
    /// Summary description for BhieService
    /// </summary>
    [WebService(Namespace = "http://mdws.medora.va.gov/BhieService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BhieService : BaseService
    {
        [WebMethod(EnableSession = true, Description = "Get notes with text from all VHA sites.")]
        public TaggedNoteArrays getNotes(
            string pwd,
            string userSitecode,
            string userName,
            string DUZ,
            string ICN,
            string fromDate,
            string toDate,
            string nNotes)
        {
            return (TaggedNoteArrays)MySession.execute("NoteLib", "getNotesForBhie", new object[] { pwd, ICN, fromDate, toDate, nNotes });
        }
    }
}
