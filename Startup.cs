using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(ProlappApi.Startup))]
namespace ProlappApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.MapSignalR();
           
        }

        
    }
}