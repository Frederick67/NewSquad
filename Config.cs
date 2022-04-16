using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events;

namespace NewSquad
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public int Change { get; set; } = 100;

        public int Health { get; set; } = 200;

        public bool WinWithChaos { get; set; } = true;

        public bool WinWithMTF { get; set; } = true;
        
        public Exiled.API.Features.Broadcast DeadBrodcast { get; set; } = new Exiled.API.Features.Broadcast("Stai per usare il defribrillatore aspetta", 10);
        
        public int RespawnTime { get; set; } = 20;
        
        public String DeadHint { get; set; } = "Stai per usare il defribrillatore aspetta:";
        public String Secondi { get; set; } = "secondi";



    }
}
