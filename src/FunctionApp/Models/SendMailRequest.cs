namespace RazorLight.Samples.Models
{
    public class SendMailRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public object TemplateModel { get; set; }
    }
}
