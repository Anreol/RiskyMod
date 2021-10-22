﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using R2API;
using RoR2.Projectile;
using UnityEngine;
using System;

namespace Risky_ItemTweaks.Items.Common
{
    public class StickyBomb
    {
        public static bool enabled = true;
        public static GameObject stickybombPrefab;

        public static void Modify()
        {
            if (!enabled) return;

            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                     x => x.MatchLdsfld(typeof(RoR2Content.Items), "StickyBomb")
                    );

                //New Damage
                c.GotoNext(
                    x => x.MatchLdcR4(1.8f)
                    );
                c.Next.Operand = 2.4f;

                //Replace projectile
                c.GotoNext(
                    x => x.MatchLdstr("Prefabs/Projectiles/StickyBomb")
                    );
                c.Index += 2;
                c.EmitDelegate<Func<GameObject, GameObject>>((oldPrefab) =>
                {
                    return StickyBomb.stickybombPrefab;
                });
            };

            //Modify detonation delay
            stickybombPrefab = Resources.Load<GameObject>("prefabs/projectiles/stickybomb").InstantiateClone("Risky_ItemTweaks", true);
            ProjectileImpactExplosion pie = stickybombPrefab.GetComponent<ProjectileImpactExplosion>();
            pie.lifetime = 1.2f;
            ProjectileAPI.Add(stickybombPrefab);

            LanguageAPI.Add("ITEM_STICKYBOMB_DESC", "<style=cIsDamage>5%</style> <style=cStack>(+5% per stack)</style> chance on hit to attach a <style=cIsDamage>bomb</style> to an enemy, detonating for <style=cIsDamage>240%</style> TOTAL damage.");
        }
    }
}