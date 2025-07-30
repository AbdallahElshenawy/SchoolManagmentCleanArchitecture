namespace SchoolManagment.Data.Entities.Identity
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpireAt { get; set; }

        public virtual User user { get; set; }
    }
}
