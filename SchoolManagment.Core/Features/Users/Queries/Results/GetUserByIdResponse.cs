namespace SchoolManagment.Core.Features.Users.Queries.Results
{
    public class GetUserByIdResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
