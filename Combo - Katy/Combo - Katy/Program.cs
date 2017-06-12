using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System.Collections.Generic;

namespace Combo___Katy
{
    class Program
    {
        private static Menu KMenu;
        private static AIHeroClient User = Player.Instance;

        private static Spell.Targeted Q;
        private static Spell.Active W;
        private static Spell.Active W2;
        private static Spell.Skillshot E;
        private static Spell.Active R;
        private static List<Adagar> daggers = new List<Adagar>();

        public class Adagar
        {
            public Vector3 Position { get; set; }
        }

        private static bool HasRBuff()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            return Player.Instance.Spellbook.IsChanneling;
        }

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loadingg;
        }

        private static void Loadingg(EventArgs args)
        {
            if (EloBuddy.Player.Instance.ChampionName != "Katarina") { return; }
            //Spells
            Q = new Spell.Targeted(SpellSlot.Q, 625, DamageType.Magical);
            W = new Spell.Active(SpellSlot.W);
            W2 = new Spell.Active(SpellSlot.W, 375, DamageType.Magical);
            E = new Spell.Skillshot(SpellSlot.E, 725, SkillShotType.Circular, 50, 100, 150, DamageType.Magical);
            R = new Spell.Active(SpellSlot.R, 550, DamageType.Magical);
            //Menu
            KMenu = MainMenu.AddMenu("Katarina", "Katarina");
            KMenu.AddLabel("Remember to disable the combo from the other katarina script");
            KMenu.AddLabel("(Combo Only)");
            KMenu.AddSeparator();
            KMenu.Add("ComboBox", new ComboBox("Use Combo", 1, "Q-E-W-E-R", "Q-E-W-R", "E-W-Q-E-R"));
            KMenu.Add("SliderR", new Slider("Use R Mini Enemy", 2, 1, 5));
            KMenu.AddSeparator();
            KMenu.Add("Reset", new CheckBox("Reset (AA)", false));
            KMenu.Add("Reset2", new CheckBox("Use (E) + AA"));

            Game.OnTick += Ontick;

        }
        private static void CastE(Vector3 target)
        {
            if (daggers.Count == 0 && !HasRBuff())
                E.Cast(target);
            foreach (Adagar dagger in daggers)
            {

                {

                }
                if (target.Distance(dagger.Position) <= 550)
                    User.Spellbook.CastSpell(E.Slot, dagger.Position.Extend(target, 150).To3D(), false, false);
            }
        }
            private static void Ontick(EventArgs args)
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            target = TargetSelector.GetTarget(E.Range, DamageType.Magical);

            //Reset AA
            if (HasRBuff())
            {
                Orbwalker.DisableMovement = true;
                Orbwalker.DisableAttacking = true;
            }
            else
            {
                Orbwalker.DisableMovement = false;
                Orbwalker.DisableAttacking = false;
            }
            if (Orbwalker.ActiveModesFlags.Equals(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }
        }

        private static void Combo()
        {
            if (KMenu["ComboBox"].Cast<ComboBox>().CurrentValue == 0)
            {
                var Traget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var Tragete = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var Tragetw = TargetSelector.GetTarget(W2.Range, DamageType.Magical);
                var Tragete2 = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var Tragetr = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                {
                    Q.Cast(Traget);
                    E.Cast(Tragete);
                    W2.Cast(Tragetw);
                    E.Cast(Tragete2);
                    R.Cast(Tragetr);
                }
            }
            if (KMenu["ComboBox"].Cast<ComboBox>().CurrentValue == 1)
            {
                var Traget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var Tragete = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var Tragetr = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                {
                    Q.Cast(Traget);
                    E.Cast(Tragete);
                    W.Cast();
                    R.Cast(Tragetr);
                }
            }
            if (KMenu["ComboBox"].Cast<ComboBox>().CurrentValue == 2)
            {
                var Traget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var Tragete = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var Traget2 = TargetSelector.GetTarget(W2.Range, DamageType.Magical);
                var Tragetr = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                {
                    E.Cast(Tragete);
                    W2.Cast(Traget2);
                    Q.Cast(Traget);
                    E.Cast(Tragete);
                    R.Cast(Tragetr);

                }
            }
        }
    }
}


        