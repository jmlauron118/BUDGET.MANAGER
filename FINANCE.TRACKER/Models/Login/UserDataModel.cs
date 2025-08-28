namespace FINANCE.TRACKER.Models.Login
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public List<UserModuleModel>? Modules { get; set; }
    }
}
