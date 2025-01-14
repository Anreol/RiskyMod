﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;

namespace RiskyMod.Fixes
{
    class FixDamageTypeOverwrite
    {
        public static bool enabled = true;
        public FixDamageTypeOverwrite()
        {
            if (!enabled) return;

            //Remove Vanilla Effect
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                     x => x.MatchLdsfld(typeof(RoR2Content.Buffs), "AffixRed")
                    );
                c.Index += 7;
                c.EmitDelegate<Func<bool, bool>>((cond) =>
                {
                    return false;
                });
            };

            //Effect is handled in SharedHooks.OnHitEnemy
        }
    }
}
