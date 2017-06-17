using EloBuddy;
using EloBuddy.SDK.Events;
using SharpDX;
using System.Linq;
using System;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Menu.Values;

namespace Blitz
{
    internal class BuffsShapion
    {
        private static Vector3 MousePos
        {
            get { return Game.CursorPos; }
        }
        public static bool HasSpell(string teltys)
        {
            return Player.Spells.FirstOrDefault(o => o.SData.Name.Contains(teltys)) != null;
        }
        public static bool ItenrTrageet(AIHeroClient target)
        {
            if (target.Buffs.Any(Bronze => Bronze.IsValid() && Bronze.DisplayName == "UndyingRage"))
                return true;
            if (target.Buffs.Any(Prata => Prata.IsValid() && Prata.DisplayName == "ChronoShift"))
                return true;
            if (target.Buffs.Any(Ouro => Ouro.IsValid() && Ouro.DisplayName == "JudicatorIntervention"))
                return true;
            if (target.Buffs.Any(Platinum => Platinum.IsValid() && Platinum.DisplayName == "kindredrnodeathbuff"))
                return true;
            if (target.Buffs.Any(Mester => Mester.IsValid() && Mester.DisplayName == "Flash"))
                return true;
            if (target.HasBuffOfType(BuffType.Invulnerability))
                return true;

            return target.IsInvulnerable;
        }
        public BuffsShapion()
        {
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Orbwalker.OnPostAttack += Pre_AtackBasic;
            Orbwalker.OnPreAttack += Orbwalke;
            Drawing.OnDraw += Drawing_Spells;
            Game.OnUpdate += Modes.ModesActive.On_Tick;
        }

        private void Orbwalke(AttackableUnit m, Orbwalker.PreAttackArgs a)
        {
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) &&
                !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                return;
            }
            var t = m as Obj_AI_Minion;
            if (t == null) return;
            {
                if (Shapion.L["ModeSupport"].Cast<CheckBox>().CurrentValue)
                   a.Process = false;
            }
        }


    private void Drawing_Spells(EventArgs args)
        {
            if (Shapion.D["Q"].Cast<CheckBox>().CurrentValue && SpellsManager._Q.IsLearned)
            {
                Circle.Draw(Color.LightYellow, SpellsManager._Q.Range, Player.Instance.Position);
            }
            if (Shapion.D["R"].Cast<CheckBox>().CurrentValue && SpellsManager._R.IsLearned)
            {
                Circle.Draw(Color.Red, SpellsManager._R.Range, Player.Instance.Position);
            }
            if (Shapion.D["F"].Cast<CheckBox>().CurrentValue && SpellsManager.Flash.IsLearned)
            {
                Circle.Draw(Color.LightBlue, SpellsManager.Flash.Range, Player.Instance.Position);
            }
        }

        private void Pre_AtackBasic(AttackableUnit target, EventArgs args)
        {
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                return;
            }
            var Etracent = target as AIHeroClient;
            if (!Shapion.C["E"].Cast<CheckBox>().CurrentValue || !SpellsManager._E.IsReady() || !Etracent.IsEnemy) return;
            if (target == null) return;
            if (Etracent.IsValidTarget() && SpellsManager._E.IsReady())
            {
                SpellsManager._E.Cast();
            }
        }
        private void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var InteronperQ = TargetSelector.GetTarget(SpellsManager._Q.Range, DamageType.Magical);
            {
                if (SpellsManager._Q.IsReady() && sender.IsValidTarget(SpellsManager._Q.Range) && Shapion.M["IQ"].Cast<CheckBox>().CurrentValue)
                    SpellsManager._Q.Cast(InteronperQ.ServerPosition);
                if (SpellsManager._E.IsReady() && sender.IsValidTarget(SpellsManager._Q.Range) && Shapion.M["IE"].Cast<CheckBox>().CurrentValue)
                    SpellsManager._E.Cast();
            }
            if (Shapion.M["F+Q"].Cast<KeyBind>().CurrentValue)
                if (Shapion.M["F+E"].Cast<KeyBind>().CurrentValue)
                {
                    FlashQ();
                    FlashE();
                }
        }

        private void FlashE()
        {
            Player.IssueOrder(GameObjectOrder.MoveTo, MousePos);
            var eTraget = TargetSelector.GetTarget(325, DamageType.Magical);
            if (eTraget == null)
            {
                return;
            }
            var psoicionsX = eTraget.Position.Extend(eTraget, 350);
            if (!Shapion.M["F+E"].Cast<KeyBind>().CurrentValue)
            {
                return;
            }
            if (!SpellsManager._E2.IsReady() || !SpellsManager.Flash.IsReady())
            {
                return;
            }
            if (!eTraget.IsValidTarget(350))
            {
                return;
            }
            SpellsManager.Flash.Cast((Vector3)psoicionsX);
            SpellsManager._E2.Cast();
        }

        private void FlashQ()
        {
            Player.IssueOrder(GameObjectOrder.MoveTo, MousePos);
            var qTraget = TargetSelector.GetTarget(925, DamageType.Magical);
            if (qTraget == null)
            {
                return;
            }
            var psoicionsX = qTraget.Position.Extend(qTraget, 750);
            var predictionpositionQ = SpellsManager._Q.GetPrediction(qTraget).CastPosition;
            if (!Shapion.M["F+Q"].Cast<KeyBind>().CurrentValue)
            {
                return;
            }
            if (!SpellsManager._Q.IsReady() || !SpellsManager.Flash.IsReady())
            {
                return;
            }
            if (!qTraget.IsValidTarget(925))
            {
                return;
            }
            SpellsManager.Flash.Cast((Vector3)psoicionsX);
            SpellsManager._Q.Cast(predictionpositionQ);
        }

    }
}