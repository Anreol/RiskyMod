﻿using RoR2;
using System.Collections.Generic;

namespace RiskyMod.Drones
{
    public class VagrantResistance
    {
        public static bool enabled = true;
        public static List<BodyIndex> AffectedBodies;
        public static BodyIndex VagrantIndex;

        public VagrantResistance()
        {
            if (!enabled) return;

            AffectedBodies = new List<BodyIndex>();
            On.RoR2.BodyCatalog.Init += (orig) =>
            {
                orig();
                AddBody("BackupDroneBody");

                AddBody("Drone1Body");
                AddBody("Drone2Body");
                AddBody("Turret1Body");

                AddBody("MissileDroneBody");
                AddBody("FlameDroneBody");
                AddBody("EquipmentDroneBody");
                AddBody("EmergencyDroneBody");

                VagrantIndex = BodyCatalog.FindBodyIndex("VagrantBody");
            };

            //Damage reduction handled in SharedHooks.TakeDamage
        }

        public static bool HasResist(BodyIndex index)
        {
            foreach (BodyIndex b in AffectedBodies)
            {
                if (b == index) return true;
            }
            return false;
        }

        public static void AddBody(string bodyname)
        {
            BodyIndex index = BodyCatalog.FindBodyIndex(bodyname);
            if (index != BodyIndex.None)
            {
                AffectedBodies.Add(index);
            }
        }
    }
}
