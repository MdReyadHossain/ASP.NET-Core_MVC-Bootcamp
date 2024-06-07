namespace CrudApp.Constant
{
    public static class AuthConstant
    {
        public const string IDENTITY_AUTH_TYPE = "_userAuth";
        public const string DEFAULT_CONNECTION_STRING = "Server=.; Database=UserPortal; Trusted_Connection=True; TrustServerCertificate=True;";

        public static string GET_CONNECTION_STRING(string databaseName)
        {
            return $"Server=.; Database={databaseName}; Trusted_Connection=True; TrustServerCertificate=True;";
        }
    }
}
