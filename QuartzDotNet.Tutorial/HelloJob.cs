using Quartz;

namespace QuartzDotNet.Tutorial;

public class HelloJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("Hello World!");
    }
}