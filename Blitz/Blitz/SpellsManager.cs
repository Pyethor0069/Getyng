using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;

namespace Blitz
{
    internal class SpellsManager
    {
        public static Spell.Skillshot _Q;
        public static Spell.Active _W;
        public static Spell.Active _E;
        public static Spell.Targeted _E2;
        public static Spell.Active _R;
        public static Spell.Skillshot Flash;
        public static HitChance HitQ()
        {
            switch (Shapion.C["QHit"].Cast<ComboBox>().CurrentValue)
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
        public SpellsManager()
        {
            _Q = new Spell.Skillshot(SpellSlot.Q, 925, SkillShotType.Linear, 250, 1800, 70);
            _W = new Spell.Active(SpellSlot.W);
            _E = new Spell.Active(SpellSlot.E);
            _E2 = new Spell.Targeted(SpellSlot.E, 325);
            _R = new Spell.Active(SpellSlot.R, 450);
            var Flush = Player.Instance.GetSpellSlotFromName("summonerflash");
            Flash = new Spell.Skillshot(Flush, 450, SkillShotType.Linear);
        }
    }
}