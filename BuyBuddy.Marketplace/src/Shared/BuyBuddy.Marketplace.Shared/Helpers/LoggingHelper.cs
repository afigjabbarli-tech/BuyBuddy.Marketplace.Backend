using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace BuyBuddy.Marketplace.Shared.Helpers
{
    public static class LoggingHelper
    {
        public static void LogDetailedError(
            ILogger logger,
            Exception ex,
            [CallerMemberName] string callerMethod = "",
            [CallerFilePath] string callerFile = "",
            [CallerLineNumber] int callerLine = 0)
        {
            logger.LogError(ex,
                "Exception occurred in method physical location: {PhysicalStackTrace}. " +
                "Called from: {CallerMethod} at {CallerFile}:{CallerLine}. Exception message: {Message}",
                ex.StackTrace,
                callerMethod,
                callerFile,
                callerLine,
                ex.Message);
        }
    }
}
