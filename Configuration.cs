namespace JwtBearer
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetPrivateKey()
        {
            return _configuration["Signature:Secret"];
        }
    }
}
