namespace Core.Helpers
{
    public class ApplicationSettings
    {
        /// <summary>
        /// API Url Endpoint from Application Settings
        /// </summary>
        /// <value></value>
        public string ApiEndPointUrl { get; set; }
        /// <summary>
        /// General email where birthday messages are sent.If the employee has the email it will use it else use one in appsettings
        /// </summary>
        public string Email { get; set; }
    }
}