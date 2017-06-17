using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Blitz
{
    internal class Shapion
    {
        public static Menu BMenu, C, H, L, J, D, M;
        public Shapion()
        {
            BMenu = MainMenu.AddMenu("Blitz", "Blitz");
            C = BMenu.AddSubMenu("Combo");
            {
                C.AddLabel("Combo");
                C.Add("Q", new CheckBox("Use Q"));
                C.Add("QHit", new ComboBox("HitChance", 1, "Low", "Medium", "High"));
                C.Add("W", new CheckBox("Use W", false));
                C.Add("E", new CheckBox("Use E"));
                C.Add("R", new CheckBox("Use R"));
                C.AddLabel("Spell (R)");
                C.Add("RE", new Slider("Mini Use R Enemys", 2, 1, 5));
            }
            H = BMenu.AddSubMenu("Harass");
            {
                H.AddLabel("Harass");
                H.Add("Q", new CheckBox("Use Q"));
                H.Add("E", new CheckBox("Use E"));
                H.Add("R", new CheckBox("Use R"));
            }
            L = BMenu.AddSubMenu("Lane");
            {
                L.AddLabel("LaneClear");
                L.Add("ModeSupport", new CheckBox("Support Mode Active"));
            }
            J = BMenu.AddSubMenu("Jungle");
            {
                J.AddLabel("JungleClear");
                J.Add("ModeSupport", new CheckBox("Support Mode Active"));
                
            }
            M = BMenu.AddSubMenu("Misc");
            {
                M.AddLabel("Misc");
                M.Add("IQ", new CheckBox("Inter (Q)"));
                M.Add("IE", new CheckBox("Inter (E)"));
                M.AddLabel("Combo Spell (R)");
                M.Add("F+Q", new KeyBind("Flash + Q", false, KeyBind.BindTypes.HoldActive, 'K'));
                M.Add("F+E", new KeyBind("Flash + E", false, KeyBind.BindTypes.HoldActive, 'M'));
            }
            D = BMenu.AddSubMenu("Draw");
            {
                D.AddLabel("Drawing");
                D.Add("Q", new CheckBox("Draw Q"));
                D.Add("R", new CheckBox("Draw R"));
                D.Add("F", new CheckBox("Draw Flash"));
            }
        }
    }
}