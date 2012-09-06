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
    public class OrderTO : AbstractTO
    {
        public string id;
        public string timestamp;
        public string orderingServiceName;
        public string treatingSpecialty;
        public string startDate;
        public string stopDate;
        public string status;
        public string sigStatus;
        public string dateSigned;
        public string verifyingNurse;
        public string dateVerified;
        public string verifyingClerk;
        public string chartReviewer;
        public string dateReviewed;
        public UserTO provider;
        public string text;
        public string detail;
        public string errMsg;
        public bool flag;
        public OrderTypeTO type;

        public OrderTO() { }

        public OrderTO(Order mdoOrder)
        {
            this.id = mdoOrder.Id;
            this.timestamp = mdoOrder.Timestamp.ToString("yyyyMMdd.HHmmss");
            this.orderingServiceName = mdoOrder.OrderingServiceName;
            this.treatingSpecialty = mdoOrder.TreatingSpecialty;
            this.startDate = mdoOrder.StartDate.ToString("yyyyMMdd.HHmmss");
            this.stopDate = mdoOrder.StopDate.ToString("yyyyMMdd.HHmmss");
            this.status = mdoOrder.Status;
            this.sigStatus = mdoOrder.SigStatus;
            this.dateSigned = mdoOrder.DateSigned.ToString("yyyyMMdd.HHmmss");
            this.verifyingNurse = mdoOrder.VerifyingNurse;
            this.dateVerified = mdoOrder.DateVerified.ToString("yyyyMMdd.HHmmss");
            this.verifyingClerk = mdoOrder.VerifyingClerk;
            this.chartReviewer = mdoOrder.ChartReviewer;
            this.dateReviewed = mdoOrder.DateReviewed.ToString("yyyyMMdd.HHmmss");
            this.provider = new UserTO(mdoOrder.Provider);
            this.text = mdoOrder.Text;
            this.detail = mdoOrder.Detail;
            this.errMsg = mdoOrder.ErrMsg;
            this.flag = mdoOrder.Flag;
            this.type = new OrderTypeTO(mdoOrder.Type);
        }

        public OrderTO(Exception e)
        {
            this.fault = new FaultTO(e);
        }
    }
}
