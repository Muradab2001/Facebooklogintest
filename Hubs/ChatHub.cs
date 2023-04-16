using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MultiShop.Hubs
{
    public class ChatHub : Hub
    {
        public  async Task Send(string message)
        {
            await Clients.All.SendAsync("recivemesage",message);
        }
    }
}
