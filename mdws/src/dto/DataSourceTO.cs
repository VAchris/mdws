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
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class DataSourceTO : AbstractTO
    {
        public String protocol = "";
        public String modality = "";
        public int timeout;
        public int port;
        public String provider = "";
        public String status = "";
        public String description = "";
        public String context = "";
        public bool testSource = false;
        public String vendor = "";
        public String version;
        public TaggedText siteId;
        public String welcomeMessage = "";

        public DataSourceTO() { }

        public DataSourceTO(DataSource mdoSrc)
        {
            this.protocol = mdoSrc.Protocol == null ? "" : mdoSrc.Protocol;
            this.modality = mdoSrc.Modality == null ? "" : mdoSrc.Modality;
            //this.timeout = mdoSrc.Timeout;
            this.port = mdoSrc.Port;
            this.provider = mdoSrc.Provider == null ? "" : mdoSrc.Provider;
            this.status = mdoSrc.Status == null ? "" : mdoSrc.Status;
            this.description = mdoSrc.Description == null ? "" : mdoSrc.Description;
            this.context = mdoSrc.Context == null ? "" : mdoSrc.Context;
            this.testSource = mdoSrc.IsTestSource;
            this.vendor = mdoSrc.Vendor == null ? "" : mdoSrc.Vendor;
            this.version = mdoSrc.Version == null ? "" : mdoSrc.Version;
            this.siteId = new TaggedText(mdoSrc.SiteId.Id, mdoSrc.SiteId.Name);
        }
    }
}
