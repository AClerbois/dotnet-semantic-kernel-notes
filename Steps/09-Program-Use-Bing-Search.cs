using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;

ServiceCollection c = new();

#pragma warning disable CS8604 // Possible null reference argument.
c.AddAzureOpenAIChatCompletion("gpt-4o", "https://devday-semantickernel-test.openai.azure.com/", Environment.GetEnvironmentVariable("AI:AzureOpenAI:Key"));
#pragma warning restore CS8604 // Possible null reference argument.
c.AddKernel();
// c.AddLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.Debug));
IServiceProvider services = c.BuildServiceProvider();


Kernel kernel = services.GetRequiredService<Kernel>();
kernel.ImportPluginFromType<Speakers>();
#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
kernel.ImportPluginFromObject(new WebSearchEnginePlugin(new BingConnector(Environment.GetEnvironmentVariable("AI:Bing:Key"))));
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
IChatCompletionService chatService = services.GetRequiredService<IChatCompletionService>();

PromptExecutionSettings settings = new AzureOpenAIPromptExecutionSettings(){
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

ChatHistory history = new();
while (true)
{
    Console.Write("Question ? : ");
#pragma warning disable CS8604 // Possible null reference argument.
    history.AddUserMessage(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.

    var assistant = await chatService.GetChatMessageContentAsync(history, settings, kernel);
    history.Add(assistant);
    Console.WriteLine(assistant);
}

class Speakers
{
    IList<Speaker> Items = new List<Speaker>()
    {
        new Speaker("Adrien", "Azure OpenAI"),
        new Speaker("Adrien", "Azure Playwright"),
        new Speaker("Christophe", "Azure Playwright"),
        new Speaker("Christophe", ".NET Blazor - What's new"),
        new Speaker("Denis", ".NET Blazor - What's new"),
        new Speaker("Denis", "Microsoft Fluent UI Blazor"),
        new Speaker("Vincent", "Microsoft Fluent UI Blazor")
    };

    [KernelFunction("GetSpeakersByName")]
    public IList<Speaker> GetSpeakersByName(string name)
    {
        return Items.Where(s => s.Name == name).ToList();
    }
    
    [KernelFunction("GetSessionsBySpeaker")]
    public IList<Speaker> GetSessionsBySpeaker(string session)
    {
        return Items.Where(s => s.SessionTitle== session).ToList();
    }

    public record Speaker(string Name, string SessionTitle);
}