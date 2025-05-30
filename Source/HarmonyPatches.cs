using DivineFramework;
using HarmonyLib;
using RimWorld;
using System;
using Verse;
using Verse.AI;

namespace PrisonerReading
{
    [HarmonyPatch]
    public static class HarmonyPatches
    {
        internal static bool putItBack;
        internal static JobDriver_Reading driver;
        internal static Job putBackJob;

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
                driver = (JobDriver_Reading)__instance;
                LogUtil.Message($"Starting get finalizer job for {__instance.pawn}", true);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(JobDriver), nameof(JobDriver.GetFinalizerJob))]
        public static void GetFinalizerJobPostfix(JobDriver __instance, Job __result)
        {
            if (__instance == driver)
            {
                putItBack = false;
                driver = null;
                putBackJob = __result;
                LogUtil.Message($"Ending get finalizer job for {__instance.pawn}", true);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.TryFindBestBetterStorageFor))]
        public static void TryFindBestBetterStorageForPrefix(Pawn carrier, ref Faction faction, Thing t)
        {
            //Trying real hard to make sure I only do this for prisoners reading books.
            if (driver != null && t is Book book)
            {
                if (putItBack && carrier.IsPrisonerOfColony)
                {
                    LogUtil.Message($"Changing faction from {faction} to {carrier.HostFaction}", true);
                    faction = carrier.HostFaction;
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Pawn_CarryTracker), nameof(Pawn_CarryTracker.TryDropCarriedThing), [typeof(IntVec3), typeof(ThingPlaceMode), typeof(Thing), typeof(Action<Thing, int>)], [ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out, ArgumentType.Normal])]
        public static void TryDropCarriedThingPostfix(IntVec3 dropLoc, ThingPlaceMode mode, Thing resultingThing, Action<Thing, int> placedAction, Pawn ___pawn, ref bool __result)
        {
            if (putBackJob != null && ___pawn.CurJob == putBackJob)
            {
                LogUtil.Message($"Unforbidding book", true);
                resultingThing.SetForbidden(false, false);
                putBackJob = null;
            }
        }
    }
}
