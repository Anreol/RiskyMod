﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;

namespace RiskyMod.Items.Uncommon
{
    public class Infusion
    {
        public static bool enabled = true;
        public Infusion()
        {
            if (!enabled) return;

            //Remove vanilla effect
            IL.RoR2.GlobalEventManager.OnCharacterDeath += (il) =>
            {
                ILCursor c = new ILCursor(il);
                c.GotoNext(
                     x => x.MatchLdsfld(typeof(RoR2Content.Items), "Infusion")
                    );
                c.Remove();
                c.Emit<RiskyMod>(OpCodes.Ldsfld, nameof(RiskyMod.emptyItemDef));
            };

            //Handled in AssistManager
        }
    }
}