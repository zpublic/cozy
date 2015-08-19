using System;

namespace CozyNote.WinformClient.Models {

    public class NoteModel {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
