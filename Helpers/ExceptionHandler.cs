namespace RustOptimizer.Helpers
{
    internal class ExceptionHandler
    {
        public static void LogError(Exception ex)
        {
            try
            {
                string logFilePath = Application.StartupPath + @"\log_file.txt";

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] - Unhandled Exception Type: {ex.GetType().FullName}");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine();
                }

                System.Diagnostics.Debug.WriteLine($"Error details logged to: {logFilePath}");
            }
            catch (Exception logEx)
            {
                System.Diagnostics.Debug.WriteLine("Error logging failed: " + logEx.Message);
            }
        }
        public static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;

            if (exception != null)
            {
                System.Diagnostics.Debug.WriteLine($"Unhandled Exception Type: {exception.GetType().FullName}");
                System.Diagnostics.Debug.WriteLine($"Message: {exception.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {exception.StackTrace}");

                LogError(exception);
            }
        }

    }
}
