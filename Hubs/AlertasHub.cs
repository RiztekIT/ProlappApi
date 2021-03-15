using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ProlappApi.Hubs
{
    
    public class AlertasHub : Hub
    {
        
        //private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<AlertasHub>();

        //[HubMethodName("alertasHub")]
        public void NuevaNotificacion(Object mensaje)
        {
            //Clients.Caller.alertasHub("hola");
            Clients.All.alertasHub("Hola "+ mensaje);
           
            //hubContext.Clients.All.SendAsync("Nuevo Mensaje", mensaje);
        }
    }
}