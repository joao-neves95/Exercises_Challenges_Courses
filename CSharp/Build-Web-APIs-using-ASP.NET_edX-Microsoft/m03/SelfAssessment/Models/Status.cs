namespace SelfAssessment.Models
{
    public class Status
    {
        private static object notFound = new { error = "404 Not Found" };
        public static object NotFound
        {
            get { return notFound; }
        }

        private static object ok = new { success = "200 Ok" };
        public static object Ok
        {
            get { return ok; }
        }
        
    }
}