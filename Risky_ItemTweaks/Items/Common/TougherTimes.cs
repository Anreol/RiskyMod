﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using R2API;
namespace Risky_ItemTweaks.Items.Common
{
    public class TougherTimes
    {
        public static bool enabled = true;
        public static void Modify()
        {
            if (!enabled) return;

            //Change block chance
            IL.RoR2.HealthComponent.TakeDamage += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                     x => x.MatchLdcR4(15f),
                     x => x.MatchLdarg(0),
                     x => x.MatchLdflda(nameof(RoR2.HealthComponent), "itemCounts"),
                     x => x.MatchLdfld(nameof(RoR2.HealthComponent.itemCounts), "bear")
                    );
                c.Next.Operand = 7.5f;
            };

            LanguageAPI.Add("ITEM_BEAR_DESC", "<style=cIsHealing>7%</style> <style=cStack>(+7% per stack)</style> chance to <style=cIsHealing>block</style> incoming damage. <style=cIsUtility>Unaffected by luck</style>.");

            //Effect handled on SharedHooks.GetStatCoefficients and SharedHooks.RecalculateStats
        }
    }
}