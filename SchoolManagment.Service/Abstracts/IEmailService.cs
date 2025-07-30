namespace SchoolManagment.Service.Abstracts
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string Message, string? subject);
    }
}
