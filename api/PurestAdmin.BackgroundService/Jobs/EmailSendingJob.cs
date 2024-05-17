// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace PurestAdmin.BackgroundService.Jobs;
public class EmailSendingJob : BackgroundJob<EmailSendingArgs>, ITransientDependency
{
    //private readonly IEmailSender _emailSender;

    public EmailSendingJob(/*IEmailSender emailSender*/)
    {
        //_emailSender = emailSender;
    }

    public override void Execute(EmailSendingArgs args)
    {
        //_emailSender.Send(
        //    args.EmailAddress,
        //    args.Subject,
        //    args.Body
        //);
        throw new NotImplementedException();
    }

}
public class EmailSendingArgs
{
    public string EmailAddress { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
