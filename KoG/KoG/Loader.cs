using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System.Linq;

namespace KoG
{
    class Loader
    {
        private static Menu KMenu, CM, LM, HM, DM;
        private static Spell.Skillshot Q;
        private static Spell.Active W;
        private static Spell.Skillshot E;
        private static Spell.Skillshot R;
        private static AIHeroClient Buff = Player.Instance;

        //hits

        public static HitChance HitQ()
        {
            switch (CM["HitQ"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
                default:
                    return HitChance.Unknown;
            }
        }
        public static HitChance HitE()
        {
            switch (CM["HitE"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
                default:
                    return HitChance.Unknown;
            }
        }
        public static HitChance HitR()
        {
            switch (CM["HitR"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
                default:
                    return HitChance.Unknown;
            }
        }
        public static float QDamage(Obj_AI_Base target)
        {
            if (Q.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[]
                { 0f, 75f, 105f, 135f, 165f, 195f }[Q.Level] + 0.3f *
                Player.Instance.TotalMagicalDamage);
            else
            { return 0f; }
        }
        public static float WDamege(Obj_AI_Base target)
        {
            if (W.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 10f, 20f, 30f }[W.Level] - 0.3f * Player.Instance.TotalMagicalDamage);
            else
            { return 0f; }
        }
        public static float EDamage(Obj_AI_Base target)
        {
            if (E.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                    new[] { 0f, 25f, 40f, 55f, 70f, 85f }[E.Level] + 0.25f * Player.Instance.TotalMagicalDamage + 0.5f *
                    Buff.TotalAttackDamage);
            else
            { return 0f; }
        }
        public static float RDamage(Obj_AI_Base target)
        {
            if (!R.IsReady())
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                    (new[] { 250f, 375f, 400f, 550f }[R.Level] + 2.85f * Player.Instance.TotalMagicalDamage + 3.3f *
                    (Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage)));
            else
            { return 0f; }
        }
        public static float KDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                ((Buff.Level / 1.75f) + 3f) * Buff.Level + 71.5f + 1.25f *
                 (Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage)
                + Buff.TotalMagicalDamage *
                         new[] { .55f, .70f, .80f, 1.00f }[R.Level]);

        }
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_Complete;
        }

        private static void Loading_Complete(EventArgs args)
        {
            //Campeão
            if (EloBuddy.Player.Instance.ChampionName != "KogMaw") { return; }
            Chat.Print("[<font color='#00FF7F'>Version 1.3.56</font> loaded. <font color='#00FF7F'>Beta</font>]");
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Circular, spellSpeed: int.MaxValue, spellWidth: 160 * 2, castDelay: 750);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1200, SkillShotType.Circular, 50, null, 150, DamageType.Magical);
            R = new Spell.Skillshot(SpellSlot.R, 1200, SkillShotType.Circular, 1550, null, 1750, DamageType.Magical);
            //Menu
            KMenu = MainMenu.AddMenu("Kog`Maw", "Kog`Maw");
            //CM
            CM = KMenu.AddSubMenu("Combo");
            CM.Add("Qc", new CheckBox("(Q) In Combo"));
            CM.Add("Qw", new CheckBox("(W) In Combo"));
            CM.Add("Qe", new CheckBox("(E) In Combo"));
            CM.Add("Qr", new CheckBox("(R) In Combo"));
            CM.AddLabel("Hit Chance (Spells)");
            CM.Add("HitQ", new ComboBox("Hit (Q)", 1, "Low", "Medium", "High"));
            CM.Add("HitE", new ComboBox("Hit (E)", 1, "Low", "Medium", "High"));
            CM.Add("HitR", new ComboBox("Hit (R)", 2, "Low", "Medium", "High"));
            CM.AddLabel("Modo Spell (R)");
            CM.Add("QQE", new CheckBox("Use (R) + AA", false));
            CM.Add("QEWE", new CheckBox("Use (R) + Damager", false));
            //HM
            HM = KMenu.AddSubMenu("Harass");
            HM.Add("HQ", new CheckBox("Use (Q)"));
            HM.Add("HR", new CheckBox("Use (R)", false));
            HM.AddLabel("Mana Manager");
            HM.Add("HQM", new Slider("Mana (Q)", 65, 1, 100));
            HM.Add("HRM", new Slider("Mana (R)", 75, 1, 100));
            //LM
            LM = KMenu.AddSubMenu("Lane");
            LM.Add("LQ", new CheckBox("Use (Q)"));
            LM.Add("LQM", new Slider("Mana", 45, 1, 100));
            LM.Add("LW", new CheckBox("Use (W)"));
            LM.Add("LWM", new Slider("Mana", 45, 1, 100));
            LM.Add("LR", new CheckBox("Use (R)"));
            LM.Add("LRM", new Slider("Mana", 45, 1, 100));
            LM.Add("mINI", new Slider("Lane Spell(R)", 2, 1, 6));
            //DM
            DM = KMenu.AddSubMenu("Drawing");
            DM.Add("DQ", new CheckBox("Drawing (Q)"));
            DM.Add("DER", new CheckBox("Drawing (E)/(R)"));
            DM.Add("DrawDamager", new CheckBox("Draw Damager"));

            Drawing.OnDraw += On_Draw;
            Drawing.OnEndScene += Life_Indiquetion;
            Game.OnTick += On_Tick;

        }

        private static void On_Tick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                LaneClear();
            }

        }

        private static void LaneClear()
        {
            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(Q.Range)).OrderBy(target => !(target.Health <= KDamage(target, SpellSlot.E))).ThenBy(m => !m.HasBuffOfType(BuffType.Poison)).ThenBy(m => m.Health).FirstOrDefault();

            var minione = EntityManager.MinionsAndMonsters.GetLaneMinions().FirstOrDefault(m => m.IsValidTarget(W.Range) && m.HasBuffOfType(BuffType.Poison));
            var minions =
                 EntityManager.MinionsAndMonsters.GetLaneMinions()
                 .Where(m => m.IsValidTarget(W.Range))
                    .ToArray();
            if (minions.Length == 0) return;
            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, Q.Range, Q.Width,
                Q.CastDelay, Q.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();


            //Cast Q
            if (LM["LQ"].Cast<CheckBox>().CurrentValue && Q.IsReady())
            {
                var ppMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (ppMinion.Length >= LM["mINI"].Cast<Slider>().CurrentValue)
                {
                    Q.Cast(farmLocation.CastPosition);
                }
            }


            if (LM["LQ"].Cast<CheckBox>().CurrentValue && Q.IsReady() && Player.Instance.ManaPercent >= LM["LQM"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= LM["mINI"].Cast<Slider>().CurrentValue)
                {
                    Q.Cast(farmLocation.CastPosition);
                }
            }

            if (LM["LR"].Cast<CheckBox>().CurrentValue && W.IsReady() && Player.Instance.ManaPercent >= LM["LWM"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= LM["mINI"].Cast<Slider>().CurrentValue)
                {
                    R.Cast(farmLocation.CastPosition);

                }
            }
            if (LM["LW"].Cast<CheckBox>().CurrentValue && W.IsReady() && Player.Instance.ManaPercent >= LM["LWM"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                var minionS = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(W.Range)).OrderBy(target => !(target.Health <= KDamage(target, SpellSlot.E))).ThenBy(m => !m.HasBuffOfType(BuffType.Poison)).ThenBy(m => m.Health).FirstOrDefault();
                {
                    W.Cast(minionS);

                }
            }
        }
        private static float KDamage(Obj_AI_Minion target, SpellSlot e)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
               ((Buff.Level / 1.75f) + 3f) * Buff.Level + 71.5f + 1.25f *
                (Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage)
               + Buff.TotalMagicalDamage *
                        new[] { .55f, .70f, .80f, 1.00f }[R.Level]);
        }

        private static void Harass()
        {
            if (HM["HQ"].Cast<CheckBox>().CurrentValue && Q.IsReady() && Player.Instance.ManaPercent >= LM["HQM"].Cast<Slider>().CurrentValue)

            {
                var tradiçãodoinimgio = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var prediction = Q.GetPrediction(tradiçãodoinimgio);
                if (tradiçãodoinimgio.IsValidTarget(Q.Range) && prediction.HitChance >= HitQ())
                {
                    Q.Cast(tradiçãodoinimgio);
                }
            }
            if (HM["HR"].Cast<CheckBox>().CurrentValue && R.IsReady() && Player.Instance.ManaPercent >= LM["HRM"].Cast<Slider>().CurrentValue)

            {
                var tradiçãodoinimgio = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                var prediction = R.GetPrediction(tradiçãodoinimgio);
                if (tradiçãodoinimgio.IsValidTarget(R.Range) && prediction.HitChance >= HitR())
                {
                    R.Cast(tradiçãodoinimgio);
                }
            }
        }

        private static void Combo()
        {
            if (CM["Qc"].Cast<CheckBox>().CurrentValue && Q.IsReady())
            {
                var tradiçãodoinimgio = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var prediction = Q.GetPrediction(tradiçãodoinimgio);
                if (tradiçãodoinimgio.IsValidTarget(Q.Range) && prediction.HitChance >= HitQ())
                {
                    Q.Cast(tradiçãodoinimgio);
                }
            }

            if (CM["Qe"].Cast<CheckBox>().CurrentValue && E.IsReady())
            {
                var tradiçãodoinimgio = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var prediction = E.GetPrediction(tradiçãodoinimgio);
                if (tradiçãodoinimgio.IsValidTarget(E.Range) && prediction.HitChance >= HitE())
                {
                    E.Cast(tradiçãodoinimgio);
                }
            }

            if (CM["Qw"].Cast<CheckBox>().CurrentValue && W.IsReady())
            {
                W.Cast();
            }
        
            if (CM["Qr"].Cast<CheckBox>().CurrentValue && R.IsReady())
            {
                var tradiçãodoinimgio = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                var prediction = R.GetPrediction(tradiçãodoinimgio);
                if (tradiçãodoinimgio.IsValidTarget(R.Range) && prediction.HitChance >= HitR())
                {
                    R.Cast(tradiçãodoinimgio);
                }
            }
        }
    

      private static void Life_Indiquetion(EventArgs args)
        {
            foreach (var Inimigosreais in EntityManager.Heroes.Enemies.Where(x => x.VisibleOnScreen && x.IsValidTarget() && x.IsHPBarRendered))
            {
                var damage = 0f;
                if (Q.IsReady() && W.IsReady() && E.IsReady())
                    damage = QDamage(Inimigosreais) + WDamege(Inimigosreais) + EDamage(Inimigosreais) + (2f * KDamage(Inimigosreais));
                else if (Q.IsReady() && W.IsReady())
                    damage = QDamage(Inimigosreais) + WDamege(Inimigosreais) + KDamage(Inimigosreais);
                else if (Q.IsReady() && E.IsReady())
                    damage = QDamage(Inimigosreais) + EDamage(Inimigosreais) + KDamage(Inimigosreais);
                else if (W.IsReady() && E.IsReady())
                    damage = EDamage(Inimigosreais) + WDamege(Inimigosreais) + KDamage(Inimigosreais);
                else if (Q.IsReady())
                    damage = QDamage(Inimigosreais);
                else if (E.IsReady())
                    damage = EDamage(Inimigosreais);
                if (!R.IsReady())
                    damage += RDamage(Inimigosreais);

                var PrecentirDamager = ((Inimigosreais.TotalShieldHealth() - damage) > 0
                    ? (Inimigosreais.TotalShieldHealth() - damage)
                    : 0) / (Inimigosreais.MaxHealth + Inimigosreais.AllShield + Inimigosreais.AttackShield + Inimigosreais.MagicShield);
                var Percent = Inimigosreais.TotalShieldHealth() / (Inimigosreais.MaxHealth + Inimigosreais.AllShield + Inimigosreais.AttackShield + Inimigosreais.MagicShield);

                if (DM["DrawDamager"].Cast<CheckBox>().CurrentValue)
                {
                    var IniciarPossison = new Vector2((int)(Inimigosreais.HPBarPosition.X + PrecentirDamager * 107), (int)Inimigosreais.HPBarPosition.Y - 4 + 14);
                    var FianlPoscisão = new Vector2((int)(Inimigosreais.HPBarPosition.X + Percent * 107) + 1, (int)Inimigosreais.HPBarPosition.Y - 4 + 14);

                    Drawing.DrawLine(IniciarPossison, FianlPoscisão, 9.80f, System.Drawing.Color.LightBlue);
                }
            }
        }

        private static void On_Draw(EventArgs args)
        {
            if (DM["DQ"].Cast<CheckBox>().CurrentValue)
            {
                new Circle { Color = System.Drawing.Color.LightCyan, Radius = Q.Range, BorderWidth = 2f }.Draw(Player.Instance.Position);
            }
            if (DM["DER"].Cast<CheckBox>().CurrentValue)
            {
                new Circle { Color = System.Drawing.Color.LightGreen, Radius = E.Range, BorderWidth = 4f }.Draw(Player.Instance.Position);
            }
            if (DM["DER"].Cast<CheckBox>().CurrentValue)
            {
                new Circle { Color = System.Drawing.Color.LightSkyBlue, Radius = R.Range, BorderWidth = 5f }.Draw(Player.Instance.Position);
            }
        }
    }
}
