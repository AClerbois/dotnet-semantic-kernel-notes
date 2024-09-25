using OpenAI;
using System.ClientModel;
using OpenAI.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

IChatCompletionService chatService =
    // new OpenAIChatCompletionService("gpt-4o-mini", Environment.GetEnvironmentVariable("AI:OpenAI:Key"));
    // new GoogleAIGeminiChatCompletionService("gemini-mini", Environment.GetEnvironmentVariable("AI:GoogleGemini:Key"));
    // new MistralAIChatCompletionService("mistral-small-mini", Environment.GetEnvironmentVariable("AI:Mistral:Key"));
    IChatCompletionService chatService = new AzureOpenAIChatCompletionService("gpt-4o", "https://devday-semantickernel-test.openai.azure.com/", Environment.GetEnvironmentVariable("AI:AzureOpenAI:Key"));
Console.WriteLine( await chatService.GetChatMessageContentAsync("Quel est la couleur du ciel ?"));
