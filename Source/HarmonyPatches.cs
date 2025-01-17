using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace PrisonerReading
{
    [HarmonyPatch]
    public static class HarmonyPatches
    {
        internal static bool putItBack;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookUtility), "IsValidBook")]
        public static void IsValidBookPostfix(Thing thing, Pawn pawn, ref bool __result)
        {
            if (pawn.IsPrisonerOfColony)
            {
                if (thing is Book && !thing.IsForbiddenHeld(pawn) && pawn.CanReserveAndReach(thing, PathEndMode.Touch, Danger.None))
                {
                    __result = thing.IsPoliticallyProper(pawn);
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(JobDriver), nameof(JobDriver.GetFinalizerJob))]
        public static void GetFinalizerJobPrefix(JobDriver __instance)
        {
            if (__instance is JobDriver_Reading && __instance.pawn.IsPrisonerOfColony)
            {
                putItBack = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(JobDriver), nameof(JobDriver.GetFinalizerJob))]
        public static void GetFinalizerJobPostfix()
        {
            putItBack = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.TryFindBestBetterStorageFor))]
        public static void TryFindBestBetterStorageForPrefix(Pawn carrier, ref Faction faction)
        {
            if (putItBack && carrier.IsPrisonerOfColony)
            {
                faction = carrier.HostFaction;
            }
        }
    }
}
