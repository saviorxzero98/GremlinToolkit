namespace GQL.JanusGraphClients.Managements.Reports
{
    public class ManagementActionReport
    {
        /// <summary>
        /// Action Name
        /// </summary>
        public string ActionName { get; set; } = string.Empty;

        /// <summary>
        /// Is Success
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Error
        /// </summary>
        public Exception Error { get; set; }

        public static ManagementActionReport Success()
        {
            return new ManagementActionReport()
            {
                IsSuccess = true
            };
        }

        public static ManagementActionReport Fail(string message = "", Exception error = null)
        {
            return new ManagementActionReport()
            {
                IsSuccess = false,
                Message = message,
                Error = error
            };
        }

        public static ManagementActionReport Fail(Exception error)
        {
            return new ManagementActionReport()
            {
                IsSuccess = false,
                Message = (error != null) ? error.Message : string.Empty,
                Error = error
            };
        }
    }
}
