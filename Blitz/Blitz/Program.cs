using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Blitz
{
    class Program
    {

       private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingHuus;
        }

        private static void LoadingHuus(EventArgs args)
        {
            if(Player.Instance.ChampionName != "Blitzcrank") { return; }
            new Shapion();
            new SpellsManager();
            new BuffsShapion();
            
        }
    }
}
