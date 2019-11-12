namespace EmailManager.Data.Contracts
{
    public interface IEmailBody
    {
        string Body { get; set; }
        Email Email { get; set; }
        string Id { get; set; }
    }
}