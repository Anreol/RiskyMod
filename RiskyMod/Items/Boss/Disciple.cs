﻿using RoR2;
using RoR2.Orbs;
namespace RiskyMod.Items.Boss
{
    public class Disciple
    {
        public static bool enabled = true;
        public Disciple()
        {
            if (!enabled) return;
            if (!RiskyMod.disableProcChains) return;

            On.RoR2.CharacterBody.SprintWispBehavior.Fire += (orig, self) =>
            {
				DevilOrb devilOrb = new DevilOrb
				{
					origin = self.body.corePosition,
					damageValue = self.body.damage * CharacterBody.SprintWispBehavior.damageCoefficient * (float)self.stack,
					teamIndex = self.body.teamComponent.teamIndex,
					attacker = self.gameObject,
					damageColorIndex = DamageColorIndex.Item,
					scale = 1f,
					effectType = DevilOrb.EffectType.Wisp,
					procCoefficient = 0f
				};
				if (devilOrb.target = devilOrb.PickNextTarget(devilOrb.origin, CharacterBody.SprintWispBehavior.searchRadius))
				{
					devilOrb.isCrit = Util.CheckRoll(self.body.crit, self.body.master);
					OrbManager.instance.AddOrb(devilOrb);
				}
			};
        }
    }
}
