using EmailManager.Data;

namespace EmailManager.Models.EmailViewModel
{
    public class AttachmentViewModel
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public double? AttachmentSize { get; set; }

        public Email Email { get; set; }
    }
}
