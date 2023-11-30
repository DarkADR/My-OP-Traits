using HarmonyLib;
using RimWorld;
using Verse;

namespace My_OP_Traits
{
    // Class one, set up the mod
    public class MyOPTraits : Mod
    {
        public MyOPTraits(ModContentPack content) : base(content)
        {
            new Harmony("MyOPTraits.main").PatchAll();
        }
    }
    
    // Class two, make it easier to get at your special Defs, you could look it up by name and skip this step but this is probably nicer code
    [DefOf]
    public static class MyOPTraitsDefOf
    {
        public static TraitDef My_OP_Traits_GodTrait;

        static MyOPTraitsDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(MyOPTraitsDefOf));
    }

    // Class three, the actual patch
    [HarmonyPatch(typeof(HediffSet), nameof(HediffSet.AddDirect))]
    public static class HediffSet_AddDirectPatch
    {
        [HarmonyPrefix]
        public static bool AddDirect(HediffSet __instance, Hediff hediff)
        {
            return !hediff.def.isBad || !__instance.pawn.story.traits.HasTrait(MyOPTraitsDefOf.My_OP_Traits_GodTrait);
        }
    }
}
