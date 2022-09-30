using Microsoft.AspNetCore.SignalR;

namespace BlogInfoService.WebAPI.Hubs
{
    public class BlogInfoHub:Hub
    {
        public async Task SendMessage(string ParamName,string ParamValue)
        {
            await Clients.All.SendAsync("ReceiveMessage", ParamName, ParamValue);
        }
    }
}
