namespace FINANCE.TRACKER.Models
{
    public class ErrorValidation
    {
        public static string ErrorMsg(Exception ex)
        {
            string returnValue = string.Empty;

            if (ex.InnerException == null || ex.InnerException.Message == null)
            {
                returnValue = ex.Message;
            }
            else
            {
                if (ex.InnerException.Message.ToLower().Contains("unique key constraint"))
                {
                    returnValue = "Record already existed.";
                }
                else
                {
                    returnValue = ex.InnerException.Message;
                }
            }

                return returnValue;
        }
    }
}
