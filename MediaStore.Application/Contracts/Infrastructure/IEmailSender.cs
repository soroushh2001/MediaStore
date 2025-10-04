namespace MediaStore.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}
