using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NoteMoveInput
    {
        public int FromId { get; set; }

        public string FromPass { get; set; }

        public int ToId { get; set; }

        public string ToPass { get; set; }

        public int NoteId { get; set; }
    }
}
