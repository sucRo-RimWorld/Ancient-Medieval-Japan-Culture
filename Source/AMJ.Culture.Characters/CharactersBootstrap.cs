using HarmonyLib;
using Verse;

namespace AMJ.Culture.Characters
{
    // Separate bootstrap from Culture's meal/entrance/Thought patches.
    // The only Harmony patch in this assembly is SolidChildBioGatePatch.
    [StaticConstructorOnStartup]
    public static class CharactersBootstrap
    {
        static CharactersBootstrap()
        {
            new Harmony("sucro.ancientmedievaljapan.culture.characters.solidchild")
                .PatchAll();
        }
    }
}
