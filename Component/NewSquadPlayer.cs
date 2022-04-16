using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Exiled.Events;
using Exiled.API.Features.Items;

namespace NewSquad.Component
{
    internal class NewSquadPlayer : MonoBehaviour
    {
        private Player player;
        List<ItemType> inventory = new List<ItemType>();
        Vector3 pos = new Vector3(190.2f, 993.8f, -90.1f);
        NewSquad plugin;
        private bool anotherlife = false;
        private List<Item> deadinventory = new List<Item>();
        private bool canrespawn;
        public Vector3 deadpos;
        private int respawntime;


        private void Awake()
        {
            RegisterEvents();
            player = Exiled.API.Features.Player.Get(gameObject);
            Log.Debug("Component aggiunto");
            this.plugin = plugin;
        }

        private void Start()
        {
            
            InventoryInit();
            PlayerInit();


        }
        

        private void PlayerInit()
        {
            player.SetRole(RoleType.Tutorial);

            Timing.CallDelayed(0.5f, () =>
            {
                player.Teleport(pos);
                player.ClearInventory();
                player.AddItem(inventory);
            });
            player.Broadcast(10, "You are in the new squad");
            player.RankName = "New Squad";
            player.Health = player.MaxHealth = plugin.Config.Health;

        }
        private void InventoryInit()
        {
            inventory.Clear();
            inventory.Add(ItemType.GunCOM18);
            inventory.Add(ItemType.KeycardChaosInsurgency);
            inventory.Add(ItemType.Medkit);
            inventory.Add(ItemType.Adrenaline);
            inventory.Add(ItemType.Painkillers);
        }

        //events
        
        private void RoundEnd(EndingRoundEventArgs ev)
        {
            if (ev.LeadingTeam == Exiled.API.Enums.LeadingTeam.ChaosInsurgency)
                if (plugin.Config.WinWithChaos!=true)
                {
                    ev.IsAllowed = false;
                }
            if (ev.LeadingTeam == Exiled.API.Enums.LeadingTeam.FacilityForces)
                if (plugin.Config.WinWithMTF != true)
                {
                    ev.IsAllowed = false;
                }

        }
        private void OnDied(DiedEventArgs ev)
        {
            if (anotherlife)
            {
                
                Destroy(this);
            }

            //Let's save all player things
            Log.Debug("Player died");
            deadinventory.AddRange(player.Items);
            deadpos = player.Position;

            AnotherLife();
            
        }

        private void AnotherLife()
        {
            Log.Debug("Another life");
            //player.Broadcast(plugin.Config.DeadBrodcast);
            
            //Timing.CallDelayed(20f, () => sas = false);
            //Timing.RunCoroutine(Respawn());
            canrespawn = true;
            respawntime = 20;
            Timing.CallDelayed(20f , () =>
            {
                canrespawn = false;
                Log.Debug("Can respawn= false");
            });
            Timing.RunCoroutine(Respawn());
            
            



            Log.Debug("Respawn");
            player.SetRole(RoleType.Tutorial);
            Timing.CallDelayed(0.5f, () => player.Teleport(deadpos));
            //player.ClearInventory();
            //player.AddItem(deadinventory);
            //player.Health = player.MaxHealth = plugin.Config.Health;
            anotherlife = true;

            
        }

        IEnumerator<float> Respawn()
        {
            Log.Debug("Respawn coroutine");
            while (true)
            {
                Log.Debug("Respawn coroutine while");
                player.ShowHint($"{plugin.Config.DeadHint} {respawntime} {plugin.Config.Secondi}" , 1.1f);
                respawntime--;
                yield return Timing.WaitForSeconds(1.5f);
            }
        }
        
        
        


        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Server.EndingRound += RoundEnd;
        }

        private void UnRegisterEvents()
        {
            Exiled.Events.Handlers.Server.EndingRound -= RoundEnd;
        }
    }
}
