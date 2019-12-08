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

        public void Start(string connectionId)
        {
            Task.Run(() => { LongOperationTask(connectionId); });
        }

        //simulate a long task
        private void LongOperationTask(string connectionId)
        {
            DateTime start = DateTime.UtcNow;
            int maxCount = 200;
            
            for (int i = 0; i < maxCount; i++)
            {
                //Do operation slowly
                Thread.Sleep(100);

                TimeSpan duration = DateTime.UtcNow - start;
                string strDuration = String.Format("{0}", duration);
                mHubContext.Clients.Client(connectionId).SendAsync("ReportProgress", duration.ToString("g"), i * 100 / (maxCount - 1));
            }
            mHubContext.Clients.Client(connectionId).SendAsync("ReportFinish");
        }
    }
}