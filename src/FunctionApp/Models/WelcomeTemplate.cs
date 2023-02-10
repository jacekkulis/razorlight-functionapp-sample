namespace RazorLight.Samples.Models
{
    public class WelcomeTemplate : ITemplate
    {
        public string TemplateName => "Welcome";
        public string Name { get; set; }
        public string Username { get; set; }
        public string LoginUrl { get; set; }
        public string LiveChatUrl { get; set; }
        public string SupportEmail { get; set; }
    }
}
