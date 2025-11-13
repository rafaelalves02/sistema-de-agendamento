namespace SistemaDeAgendamento.Web.Models
{
    public class BrandingOptions
    {
        public const string SectionName = "Branding";

        public string BusinessName { get; set; } = "Sistema De Agendamento";
        public string PrimaryColor { get; set; } = "#0A58CA";
        public string SecondaryColor { get; set; } = "#6C757D";
        public string LogoUrl { get; set; } = "";
        public string FaviconUrl { get; set; } = "/favicon.ico";
        public bool ShowLogoInNavbar { get; set; } = false;
    }
}

