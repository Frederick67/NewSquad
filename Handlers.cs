using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using UnityEngine;
using MEC;

namespace NewSquad
{
    public class Handlers
    {
        private NewSquad plugin;
        List<Player> players = new List<Player>();
        System.Random random = new System.Random();
       
        public Handlers(NewSquad plugin)
        {
            this.plugin = plugin;
        }

        public async void TeamRespawn(RespawningTeamEventArgs ev)
        {
            if(ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
            {
                if (random.Next(0, 100) < plugin.Config.Change)
                {
                    Log.Debug("New Team Respawn");
                    players = ev.Players;
                    ev.IsAllowed = false;

                    foreach (Player player in players)
                    {
                        Log.Debug("Player: " + player.Nickname);
                    }
                    
                    foreach(Player player in players)
                    {   
                        player.GameObject.AddComponent<Component.NewSquadPlayer>();
                    }
                    
                }
                
                


            }
        }

        

    }
}
