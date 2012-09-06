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
    public class TaggedNoteArray : AbstractTaggedArrayTO
    {
        public NoteTO[] notes;

        public TaggedNoteArray() { }

        public TaggedNoteArray(string tag)
        {
            this.tag = tag;
            this.count = 0;
        }

        public TaggedNoteArray(string tag, Note[] notes)
        {
            this.tag = tag;
            if (notes == null)
            {
                this.count = 0;
                return;
            }
            this.notes = new NoteTO[notes.Length];
            for (int i = 0; i < notes.Length; i++)
            {
                this.notes[i] = new NoteTO(notes[i]);
            }
            this.count = notes.Length;
        }

        public TaggedNoteArray(string tag, Note note)
        {
            this.tag = tag;
            if (note == null)
            {
                this.count = 0;
                return;
            }
            this.notes = new NoteTO[1];
            this.notes[0] = new NoteTO(note);
            this.count = 1;
        }

        public TaggedNoteArray(string tag, Exception e)
        {
            this.tag = tag;
            this.fault = new FaultTO(e);
        }
    }
}
