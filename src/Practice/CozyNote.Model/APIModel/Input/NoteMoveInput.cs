using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NoteMoveInput
    {
        public string FromName { get; set; }

        public string FromPass { get; set; }

        public string ToName { get; set; }

        public string ToPass { get; set; }

        public int NoteId { get; set; }
    }
}
