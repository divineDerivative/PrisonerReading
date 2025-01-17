using HarmonyLib;
using Verse;

namespace PrisonerReading
{
    [StaticConstructorOnStartup]
    internal static class OnStartup
    {
        static OnStartup()
        {
            Harmony harmony = new("divineDerivative.PrionerReading");
            harmony.PatchAll();
        }
    }
}
