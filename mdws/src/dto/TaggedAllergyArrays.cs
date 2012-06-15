using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedAllergyArrays : AbstractArrayTO
    {
        public TaggedAllergyArray[] arrays;

        public TaggedAllergyArrays() { }

        public TaggedAllergyArrays(IndexedHashtable t)
        {
            if (t.Count == 0)
            {
                return;
            }
            arrays = new TaggedAllergyArray[t.Count];
            for (int i = 0; i < t.Count; i++)
            {
                if (t.GetValue(i) == null)
                {
                    arrays[i] = new TaggedAllergyArray((string)t.GetKey(i));
                }
                else if (MdwsUtils.isException(t.GetValue(i)))
                {
                    arrays[i] = new TaggedAllergyArray((string)t.GetKey(i), (Exception)t.GetValue(i));
                }
                else if (t.GetValue(i).GetType().IsArray)
                {
                    arrays[i] = new TaggedAllergyArray((string)t.GetKey(i), (Allergy[])t.GetValue(i));
                }
                else
                {
                    arrays[i] = new TaggedAllergyArray((string)t.GetKey(i), (Allergy)t.GetValue(i));
                }
            }
            count = t.Count;
        }

        internal void add(string siteId, IList<Allergy> allergies)
        {
            if (arrays == null || arrays.Length == 0)
            {
                arrays = new TaggedAllergyArray[1];
            }
            else
            {
                Array.Resize<TaggedAllergyArray>(ref arrays, arrays.Length + 1);
            }

            arrays[arrays.Length - 1] = new TaggedAllergyArray(siteId, ((List<Allergy>)allergies).ToArray());
        }
    }
}
