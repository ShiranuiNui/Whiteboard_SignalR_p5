using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Whiteboard_SignalR_p5.Hubs
{
    public class WhiteboardHub : Hub
    {
        public Task Draw(int prevX, int prevY, int currentX, int currentY)
        {
            return Clients.AllExcept(new List<string> { Context.ConnectionId }).InvokeAsync("draw", prevX, prevY, currentX, currentY);
        }
    }
}