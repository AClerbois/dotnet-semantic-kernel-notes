using OpenAI;
using System.ClientModel;
using OpenAI.Chat;

#pragma warning disable CS8604 // Possible null reference argument.
OpenAIClient client = new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("AI:OpenAI:Key")));
#pragma warning restore CS8604 // Possible null reference argument.
ChatClient chatService = client.GetChatClient("gpt-4o-mini");

await foreach (var update in chatService.CompleteChatStreamingAsync("Quel est la couleur du ciel ?"))
{
    foreach (var item in update.ContentUpdate)
    {
        Console.Write(item);
    }
}
