namespace CS436CVC3PROJECT.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string Sender { get; set; }  // เพิ่ม Sender
        public string Recipient { get; set; }  // เพิ่ม Recipient
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }  // ใช้ bool สำหรับ IsRead
    }
}
