using OpenAI;
using System.ClientModel;  
using OpenAI.Chat;

OpenAIClient client = new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("AI:AzureOpenAI:Key")));
ChatClient chatService = client.GetChatClient("gpt-4o-mini");

var result = await chatService.CompleteChatAsync("Quel est la couleur du ciel ?"); 
Console.WriteLine(result.Value);