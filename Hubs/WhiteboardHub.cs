using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Whiteboard_SignalR_p5.Contexts;
using Whiteboard_SignalR_p5.Models;

namespace Whiteboard_SignalR_p5.Hubs
{
    public class WhiteboardHub : Hub
    {
        CoordinatesContext dbcontext;
        public WhiteboardHub(CoordinatesContext _context)
        {
            this.dbcontext = _context;
        }
        public override async Task OnConnectedAsync()
        {
            string initialCoordinates = JsonConvert.SerializeObject(dbcontext.Coordinates.Select(x => x));
            await Clients.Client(Context.ConnectionId).InvokeAsync("init", initialCoordinates);
        }
        public Task Draw(int prevX, int prevY, int currentX, int currentY)
        {
            dbcontext.Coordinates.Add(new Coordinate() { PreviousX = prevX, PreviousY = prevY, NewX = currentX, NewY = currentY });
            dbcontext.SaveChanges();
            return Clients.AllExcept(new List<string> { Context.ConnectionId }).InvokeAsync("draw", prevX, prevY, currentX, currentY);
        }
    }
}