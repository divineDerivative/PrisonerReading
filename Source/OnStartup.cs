using HarmonyLib;
using Verse;
using DivineFramework;

namespace PrisonerReading
{
    [StaticConstructorOnStartup]
    internal static class OnStartup
    {
        static OnStartup()
        {
            Harmony harmony = new("divineDerivative.PrionerReading");
            harmony.PatchAll();
            ModManagement.RegisterMod("PrisonerReading", typeof(OnStartup).Assembly.GetName().Name, new("0.8.1.0"), debugDelegate: () => false);
        }
    }
}
