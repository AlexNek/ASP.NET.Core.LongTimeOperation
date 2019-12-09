using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace ASP.NET.Core.LongTimeOperation.Hubs
{
    public class LongOperationHub : Hub
    {
        private readonly IHubContext<LongOperationHub> mHubContext;

        public LongOperationHub(IHubContext<LongOperationHub> hubContext)
        {
            mHubContext = hubContext;
        }

        public override Task OnConnectedAsync()
        {
            //Clients.All.SendAsync("ReceiveMessage", "system", $"{Context.ConnectionId} started the connection");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //Clients.All.SendAsync(
            //    "ReceiveMessage",
            //    "system",
            //    $"{Context.ConnectionId} disconnected " + exception.Message);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task Start(string connectionId)
        {
            await LongOperationTask(connectionId);
        }

        //simulate a long task
        private async Task LongOperationTask(string connectionId)
        {
            DateTime start = DateTime.UtcNow;
            int maxCount = 200;
            
            for (int i = 0; i < maxCount; i++)
            {
                //Do operation slowly
                Thread.Sleep(100);

                TimeSpan duration = DateTime.UtcNow - start;
                await mHubContext.Clients.Client(connectionId).SendAsync("ReportProgress", duration.ToString("g"), i * 100 / (maxCount - 1)).ConfigureAwait(false);
            }
            await mHubContext.Clients.Client(connectionId).SendAsync("ReportFinish").ConfigureAwait(false);
        }
    }
}