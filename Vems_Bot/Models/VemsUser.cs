namespace Vems_Bot.Models
{
    public class VemsUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string course { get; set; }
        public string documentLink { get; set; }
        public string description { get; set; }
        public string note { get; set; }

        public VemsUser()
        {
            name = null;
            course = null;
            documentLink = null;
            description = null;
        }
    }
}
