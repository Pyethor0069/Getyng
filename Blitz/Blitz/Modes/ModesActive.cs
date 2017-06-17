using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using static Blitz.Shapion;
using static Blitz.SpellsManager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitz.Modes
{
    class ModesActive
    {
        internal static void On_Tick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combos();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harass();
            }
        }

        private static void Harass()
        {
            if (Shapion.H["Q"].Cast<CheckBox>().CurrentValue && SpellsManager._Q.IsReady())
            {
                var QTraget = TargetSelector.GetTarget(_Q.Range, DamageType.Magical, Player.Instance.Position);
                if (QTraget == null || BuffsShapion.ItenrTrageet(QTraget)) return;
                var prediction = _Q.GetPrediction(QTraget);
                if (QTraget.IsValidTarget(_Q.Range) && prediction.HitChance >= HitQ())
                {
                    _Q.Cast(QTraget);
                }
            }
            if (Shapion.H["E"].Cast<CheckBox>().CurrentValue && SpellsManager._E.IsReady())
            {
                _E.Cast();
            }
            if (Shapion.H["R"].Cast<CheckBox>().CurrentValue && SpellsManager._R.IsReady())
            {
                _R.Cast();
            }
        }

        private static void Combos()
        {
            //Combo1
            if (Shapion.C["W"].Cast<CheckBox>().CurrentValue && SpellsManager._W.IsReady())
            {
                _W.Cast();
            }

            if (Shapion.C["Q"].Cast<CheckBox>().CurrentValue && SpellsManager._Q.IsReady())
            {
                var QTraget = TargetSelector.GetTarget(_Q.Range, DamageType.Magical, Player.Instance.Position);
                if (QTraget == null || BuffsShapion.ItenrTrageet(QTraget)) return;
                var prediction = _Q.GetPrediction(QTraget);
                if (QTraget.IsValidTarget(_Q.Range) && prediction.HitChance >= HitQ())
                {
                    _Q.Cast(QTraget);
                }
            }

            if (Shapion.C["E"].Cast<CheckBox>().CurrentValue && SpellsManager._E.IsReady())
            {
                _E.Cast();
            }
            if (Shapion.C["R"].Cast<CheckBox>().CurrentValue && _R.IsReady())
            {
                _R.Cast();
            }
            //Combo2
            if (Shapion.C["Q"].Cast<CheckBox>().CurrentValue && _Q.IsReady())
            {
                var QTraget = TargetSelector.GetTarget(_Q.Range, DamageType.Magical, Player.Instance.Position);
                if (QTraget == null || BuffsShapion.ItenrTrageet(QTraget)) return;
                var prediction = _Q.GetPrediction(QTraget);
                if (QTraget.IsValidTarget(_Q.Range) && prediction.HitChance >= HitQ())
                {
                    _Q.Cast(QTraget);
                }
            }
            if (Shapion.C["E"].Cast<CheckBox>().CurrentValue && _E.IsReady())
            {
                _E.Cast();
            }
            if (Shapion.C["W"].Cast<CheckBox>().CurrentValue && _W.IsReady())
            {
                _W.Cast();
            }
            if (Shapion.C["R"].Cast<CheckBox>().CurrentValue && _R.IsReady())
            {
                _R.Cast();
            }
            //Combo3
            if (Shapion.C["R"].Cast<CheckBox>().CurrentValue && _R.IsReady())
            {
                _R.Cast();
            }
            if (Shapion.C["Q"].Cast<CheckBox>().CurrentValue && _Q.IsReady())
            {
                var QTraget = TargetSelector.GetTarget(_Q.Range, DamageType.Magical, Player.Instance.Position);
                if (QTraget == null || BuffsShapion.ItenrTrageet(QTraget)) return;
                var prediction = _Q.GetPrediction(QTraget);
                if (QTraget.IsValidTarget(_Q.Range) && prediction.HitChance >= HitQ())
                {
                    _Q.Cast(QTraget);
                }
            }
            if (Shapion.C["E"].Cast<CheckBox>().CurrentValue && _E.IsReady())
            {
                _E.Cast();
            }
        }
    }
}