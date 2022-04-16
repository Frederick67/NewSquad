using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace NewSquad
{
    public class NewSquad : Exiled.API.Features.Plugin<Config>
    {
        public override string Author { get; } = "Exiled Team";
        public override string Name { get; } = "NewSquad";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 0, 0);

        public Handlers Handlers { get; private set; }


        public override void OnEnabled()
        {
            Log.Info("NewSquad has been enabled.");
            base.OnEnabled();
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            Log.Info("NewSquad has been disabled.");
            base.OnDisabled();
            UnRegisterEvents();
        }

        public void RegisterEvents()
        {
            Handlers = new Handlers(this);
            Exiled.Events.Handlers.Server.RespawningTeam += Handlers.TeamRespawn;
        }
        public void UnRegisterEvents()
        {
            Exiled.Events.Handlers.Server.RespawningTeam -= Handlers.TeamRespawn;
            Handlers = null;
        }
    }
}
