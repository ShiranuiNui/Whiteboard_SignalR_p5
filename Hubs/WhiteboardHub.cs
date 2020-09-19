using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
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
            string initialCoordinates = JsonSerializer.Serialize(dbcontext.Coordinates.Select(x => x));
            await Clients.Client(Context.ConnectionId).SendAsync("init", initialCoordinates);
        }
        public async Task Draw(int prevX, int prevY, int currentX, int currentY)
        {
            dbcontext.Coordinates.Add(new Coordinate() { PreviousX = prevX, PreviousY = prevY, NewX = currentX, NewY = currentY });
            await Clients.AllExcept(new List<string> { Context.ConnectionId }).SendAsync("draw", prevX, prevY, currentX, currentY);
            await dbcontext.SaveChangesAsync();
        }
        public async Task AllDelete()
        {
            var alldata = dbcontext.Coordinates.Where(x => true).Select(x => x).ToList();
            dbcontext.Coordinates.RemoveRange(alldata);
            await dbcontext.SaveChangesAsync();
            await Clients.All.SendAsync("alldelete");
        }
    }
}