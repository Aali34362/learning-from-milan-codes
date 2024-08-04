namespace SignalRIntro.Api;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
