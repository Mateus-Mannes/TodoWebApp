namespace TodoApp.Options
{
    public class JwtOptions
    {
        public const string Section = "Jwt";
        public string JwtKey { get; set; } = string.Empty;
    }
}