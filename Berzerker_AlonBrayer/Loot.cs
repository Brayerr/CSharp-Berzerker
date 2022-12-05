using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzerker_AlonBrayer
{
    public class Loot
    {
        public string Stone { get; protected set; } = "Stone";
        public string Wood { get; protected set; } = "Wood";
        public string Gem { get; protected set; } = "Gem";
        public string Gold { get; protected set; } = "Gold";
        public string Potion { get; protected set; } = "Potion";

        public void LootTransferer(Player defeatedPlayer, Player victoriousPlayer)
        {
            for (int i = 0; i < defeatedPlayer.inventory.Count(); i++)
            {
                victoriousPlayer.inventory.Add(defeatedPlayer.inventory.ElementAt(i));
            }
            defeatedPlayer.inventory.Clear();
        }

    }
}
