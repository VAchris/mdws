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

namespace gov.va.medora.mdws.dto
{
    public class BoolTO : AbstractTO
    {
        public bool trueOrFalse;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BoolTO() { }

        /// <summary>
        /// Constructor assignes tf argument to local trueOrFalse boolean value
        /// </summary>
        /// <param name="tf">True or False</param>
        public BoolTO(bool tf)
        {
            trueOrFalse = tf;
        }

        /// <summary>
        /// Runs Boolean.TryParse on string argument
        /// </summary>
        /// <param name="tf">string 'true' or 'false'</param>
        public BoolTO(string tf)
        {
            bool success = Boolean.TryParse(tf, out trueOrFalse);
            // TBD - check for !success and throw exception??
        }
    }
}