namespace AspnetCoreReactSPA.Services;

// This class is used by the application to send email for account confirmation and password reset.
// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
public class EmailSender : IEmailSender
{
    public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
    private readonly BackgroundWorkerQueue _workerQueue;
    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, BackgroundWorkerQueue workerQueue)
    {
        Options = optionsAccessor.Value;
        _workerQueue = workerQueue;
    }
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Execute(Options.SendGridKey, subject, message, email);
    }
    public Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            //sacco@rwefra.org Replace in the config the bhangyiluke@gmail.com
            //SendGridUser has tobe a verified sender by SendGrid at the site
            From = new EmailAddress(Options.EmailAddress, Options.SendGridUser),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));
        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        _workerQueue.QueueBackgroundWorkItem(async token => {
            await client.SendEmailAsync(msg,token);
        });

        return Task.CompletedTask;
    }
}
