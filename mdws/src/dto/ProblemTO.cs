using System;
using System.Collections.Generic;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class ProblemTO : AbstractTO
    {
        public string id;
        public string status;
        public string providerNarrative;
        public string onsetDate;
        public string modifiedDate;
        public string exposures;
        public string noteNarrative;
        public AuthorTO observer;
        public TaggedText facility;
        public ObservationTypeTO type;
        public string comment;
        public TaggedTextArray organizationalProperties;

        public ProblemTO() { }

        public ProblemTO(Problem mdo)
        {
            this.id = mdo.Id;
            this.status = mdo.Status;
            this.providerNarrative = mdo.ProviderNarrative;
            this.onsetDate = mdo.OnsetDate;
            this.modifiedDate = mdo.ModifiedDate;
            this.exposures = mdo.Exposures;
            this.noteNarrative = mdo.NoteNarrative;
            if (mdo.Observer != null)
            {
                this.observer = new AuthorTO(mdo.Observer);
            }
            if (mdo.Facility != null)
            {
                this.facility = new TaggedText(mdo.Facility.Id, mdo.Facility.Name);
            }
            if (mdo.Type != null)
            {
                this.type = new ObservationTypeTO(mdo.Type);
            }
            this.comment = mdo.Comment;
            if (mdo.OrganizationProperties != null && mdo.OrganizationProperties.Count > 0)
            {
                this.organizationalProperties = new TaggedTextArray(mdo.OrganizationProperties);
            }
        }
    }
}
