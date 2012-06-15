using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.dto
{
    public class TaggedNoteArrays : AbstractArrayTO
    {
        public TaggedNoteArray[] arrays;

        public TaggedNoteArrays() { }

        public TaggedNoteArrays(IndexedHashtable t)
        {
            if (t == null || t.Count == 0)
            {
                return;
            }
            arrays = new TaggedNoteArray[t.Count];
            for (int i = 0; i < t.Count; i++)
            {
                if (t.GetValue(i) == null)
                {
                    arrays[i] = new TaggedNoteArray((string)t.GetKey(i));
                }
                else if (MdwsUtils.isException(t.GetValue(i)))
                {
                    arrays[i] = new TaggedNoteArray((string)t.GetKey(i),(Exception)t.GetValue(i));
                }
                else if (t.GetValue(i).GetType().IsArray)
                {
                    arrays[i] = new TaggedNoteArray((string)t.GetKey(i), (Note[])t.GetValue(i));
                }
                else
                {
                    arrays[i] = new TaggedNoteArray((string)t.GetKey(i), (Note)t.GetValue(i));
                }
            }
            count = t.Count;
        }

        internal void add(string siteId, IList<Note> notes)
        {
            if (arrays == null || arrays.Length == 0)
            {
                arrays = new TaggedNoteArray[1];
            }
            else
            {
                Array.Resize<TaggedNoteArray>(ref arrays, arrays.Length + 1);
            }

            arrays[arrays.Length - 1] = new TaggedNoteArray(siteId, ((List<Note>)notes).ToArray());
        }
    }
}
