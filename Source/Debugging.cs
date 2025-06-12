using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using HarmonyLib;
using DivineFramework;
using Verse.AI;

namespace PrisonerReading
{
    [HarmonyPatch]
    public static class DebuggingPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.TryFindBestBetterStorageFor))]
        public static void TryFindBestBetterStorageForPostfix(Pawn carrier, ref Faction faction, Thing t, ref bool __result)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {
                LogUtil.Message($"Result of TryFindBestBetterStorageFor: {__result}");
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.TryFindBestBetterStoreCellFor))]
        public static void TryFindBestBetterStoreCellForPostfix(Thing t, Pawn carrier, Map map, StoragePriority currentPriority, Faction faction, IntVec3 foundCell, bool needAccurateResult, ref bool __result)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {
                LogUtil.Message($"Result of TryFindBestBetterStoreCellFor: {__result}");
                //List<SlotGroup> allGroupsListInPriorityOrder = map.haulDestinationManager.AllGroupsListInPriorityOrder;
                //if (allGroupsListInPriorityOrder.Count == 0)
                //{
                //    foundCell = IntVec3.Invalid;
                //    __result = false;
                //    return;
                //}
                //StoragePriority foundPriority = currentPriority;
                //float closestDistSquared = 2.1474836E+09f;
                //IntVec3 closestSlot = IntVec3.Invalid;
                //int count = allGroupsListInPriorityOrder.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    SlotGroup slotGroup = allGroupsListInPriorityOrder[i];
                //    StoragePriority priority = slotGroup.Settings.Priority;
                //    if ((int)priority < (int)foundPriority || (int)priority <= (int)currentPriority)
                //    {
                //        break;
                //    }
                //    object[] args = [t, carrier, map, faction, slotGroup, needAccurateResult, closestSlot, closestDistSquared, foundPriority];
                //    AccessTools.Method(typeof(StoreUtility), "TryFindBestBetterStoreCellForWorker").Invoke(null, args);
                //    closestSlot = (IntVec3)args[6];
                //    closestDistSquared = (float)args[7];
                //    foundPriority = (StoragePriority)args[8];
                //}
                //if (!closestSlot.IsValid)
                //{
                //    foundCell = IntVec3.Invalid;
                //    __result = false;
                //    return;
                //}
                //foundCell = closestSlot;
                //__result = true;
            }
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.TryFindBestBetterNonSlotGroupStorageFor))]
        public static void TryFindBestBetterNonSlotGroupStorageForPostfix(Pawn carrier, ref Faction faction, Thing t, ref bool __result)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {
                LogUtil.Message($"Result of TryFindBestBetterNonSlotGroupStorageFor: {__result}");
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), "TryFindBestBetterStoreCellForWorker")]
        public static void WorkerPostfix(Thing t, Pawn carrier, Map map, Faction faction, ISlotGroup slotGroup, bool needAccurateResult, ref IntVec3 closestSlot, ref float closestDistSquared, ref StoragePriority foundPriority)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {
                LogUtil.Message($"Result of TryFindBestBetterStoreCellForWorker for slotGroup {slotGroup}: {closestSlot}");
                //if (slotGroup == null || !slotGroup.Settings.AllowedToAccept(t))
                //{
                //    return;
                //}
                //IntVec3 intVec = (t.SpawnedOrAnyParentSpawned ? t.PositionHeld : carrier.PositionHeld);
                //List<IntVec3> cellsList = slotGroup.CellsList;
                //int count = cellsList.Count;
                //int num = (needAccurateResult ? Mathf.FloorToInt((float)count * Rand.Range(0.005f, 0.018f)) : 0);
                //for (int i = 0; i < count; i++)
                //{
                //    IntVec3 intVec2 = cellsList[i];
                //    float num2 = (intVec - intVec2).LengthHorizontalSquared;
                //    if (!(num2 > closestDistSquared) && StoreUtility.IsGoodStoreCell(intVec2, map, t, carrier, faction))
                //    {
                //        closestSlot = intVec2;
                //        closestDistSquared = num2;
                //        foundPriority = slotGroup.Settings.Priority;
                //        if (i >= num)
                //        {
                //            break;
                //        }
                //    }
                //}
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), nameof(StoreUtility.IsGoodStoreCell))]
        public static void IsGoodStoreCellPostfix(IntVec3 c, Map map, Thing t, Pawn carrier, Faction faction, ref bool __result)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {
                LogUtil.Message($"Result of IsGoodStoreCell for {c}: {__result}");
                //    if (carrier != null && c.IsForbidden(carrier))
                //    {
                //        __result = false;
                //        return;
                //    }
                //    if (!(bool)AccessTools.Method(typeof(StoreUtility), "NoStorageBlockersIn").Invoke(null, [c, map, t]))
                //    {
                //        __result = false;
                //        return;
                //    }
                //    if (carrier != null)
                //    {
                //        if (!carrier.CanReserveNew(c))
                //        {
                //            __result = false;
                //            return;
                //        }
                //    }
                //    else if (faction != null && map.reservationManager.IsReservedByAnyoneOf(c, faction))
                //    {
                //        __result = false;
                //        return;
                //    }
                //    if (c.ContainsStaticFire(map))
                //    {
                //        __result = false;
                //        return;
                //    }
                //    List<Thing> thingList = c.GetThingList(map);
                //    for (int i = 0; i < thingList.Count; i++)
                //    {
                //        if (thingList[i] is IConstructible && GenConstruct.BlocksConstruction(thingList[i], t))
                //        {
                //            __result = false;
                //            return;
                //        }
                //    }
                //    if (carrier != null)
                //    {
                //        Thing spawnedParentOrMe;
                //        IntVec3 start = (((spawnedParentOrMe = t.SpawnedParentOrMe) == null) ? carrier.PositionHeld : ((spawnedParentOrMe == t || !spawnedParentOrMe.def.hasInteractionCell) ? spawnedParentOrMe.Position : spawnedParentOrMe.InteractionCell));
                //        if (!carrier.Map.reachability.CanReach(start, c, PathEndMode.ClosestTouch, TraverseParms.For(carrier)))
                //        {
                //            __result = false;
                //            return;
                //        }
                //    }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StoreUtility), "NoStorageBlockersIn")]
        public static void NoStorageBlockersInPostfix(IntVec3 c, Map map, Thing thing, ref bool __result)
        {
            if (HarmonyPatches.driver != null && thing is Book book)
            {
                LogUtil.Message($"Result of NoStorageBlockersIn for {c}: {__result}");
                //List<Thing> list = map.thingGrid.ThingsListAt(c);
                //bool flag = false;
                //for (int i = 0; i < list.Count; i++)
                //{
                //    Thing thing2 = list[i];
                //    if (!flag && thing2.def.EverStorable(willMinifyIfPossible: false) && thing2.CanStackWith(thing) && thing2.stackCount < thing2.def.stackLimit)
                //    {
                //        flag = true;
                //    }
                //    if (thing2.def.entityDefToBuild != null && thing2.def.entityDefToBuild.passability != Traversability.Standable)
                //    {
                //        __result = false;
                //        return;
                //    }
                //    if (thing2.def.surfaceType == SurfaceType.None && thing2.def.passability != Traversability.Standable && (c.GetMaxItemsAllowedInCell(map) <= 1 || thing2.def.category != ThingCategory.Item))
                //    {
                //        __result = false;
                //        return;
                //    }
                //}
                //if (!flag && c.GetItemCount(map) >= c.GetMaxItemsAllowedInCell(map))
                //{
                //    __result = false;
                //    return;
                //}
            }
        }

        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(HaulAIUtility), nameof(HaulAIUtility.HaulToStorageJob))]
        public static void HaulToStorageJobPostfix(Pawn p, Thing t, ref Job __result)
        {
            if (HarmonyPatches.driver != null && t is Book book)
            {

            }
        }

        //Now I need to see what deep storage is doing and what I need to change
    }
}
