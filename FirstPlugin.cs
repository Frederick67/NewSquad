using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstPlugin
{
    public class FirstPlugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Author { get; } = "Frederick67";
        public override string Name { get; } = "FirstPlugin";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        public Handlers Handlers { get; private set; }
        public static FirstPlugin Singleton;

        public override void OnEnabled()
        {
            Log.Info("FirstPlugin has been enabled!");
            Singleton = this;
            base.OnEnabled();
            registerEvents();

        }

        public override void OnDisabled()
        {
            Log.Info("FirstPlugin has been disabled!");
            base.OnDisabled();
            unregisterEvents();
        }
        
        private void registerEvents()
        {
            Handlers = new Handlers(this);
            Exiled.Events.Handlers.Server.RoundStarted += Handlers.RoundStarted;
            Exiled.Events.Handlers.Server.RespawningTeam += Handlers.mtfspawn;

        }

        private void unregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= Handlers.RoundStarted;
            Exiled.Events.Handlers.Server.RespawningTeam -= Handlers.mtfspawn;
            Handlers = null;

        }
    }
}
