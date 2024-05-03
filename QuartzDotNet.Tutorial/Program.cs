

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzDotNet.Tutorial;

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddQuartz();
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    })
    .Build();


var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

var helloJob = JobBuilder.Create<HelloJob>()
    .WithIdentity("hello-job", "sample-jobs-group")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("hello-trigger", "sample-triggers-group")
    .StartNow()
    .WithSimpleSchedule(opt => opt
        .WithIntervalInSeconds(40)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(helloJob, trigger);

await builder.RunAsync();
