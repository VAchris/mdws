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
    public class TaggedProstheticClaimArray : AbstractTaggedArrayTO
    {
        public ProstheticClaimTO[] claims;

        public TaggedProstheticClaimArray() { }

        public TaggedProstheticClaimArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedProstheticClaimArray(string tag, List<ProstheticClaim> mdos)
        {
            this.tag = tag;
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.claims = new ProstheticClaimTO[mdos.Count];
            for (int i = 0; i < mdos.Count; i++)
            {
                this.claims[i] = new ProstheticClaimTO(mdos[i]);
            }
            this.count = claims.Length;
        }

        public TaggedProstheticClaimArray(string tag, ProstheticClaim[] mdos)
        {
            this.tag = tag;
            if (mdos == null)
            {
                this.count = 0;
                return;
            }
            this.claims = new ProstheticClaimTO[mdos.Length];
            for (int i = 0; i < mdos.Length; i++)
            {
                this.claims[i] = new ProstheticClaimTO(mdos[i]);
            }
            this.count = claims.Length;
        }

        public TaggedProstheticClaimArray(string tag, ProstheticClaim mdo)
        {
            this.tag = tag;
            if (mdo == null)
            {
                this.count = 0;
                return;
            }
            this.claims = new ProstheticClaimTO[1];
            this.claims[0] = new ProstheticClaimTO(mdo);
            this.count = 1;
        }

        public TaggedProstheticClaimArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
