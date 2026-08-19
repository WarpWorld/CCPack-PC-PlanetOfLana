using HarmonyLib;

namespace CrowdControl.Delegates.Effects.Implementations;

public static class TwigState
{
    public static bool Disabled = false;
}

// OnTickle fires when the twig is stepped on/touched by the player;
// blocking it prevents the growth sequence from starting.
[HarmonyPatch(typeof(PoL.Creatures.TickleTwig.Twig), "OnTickle")]
public class Twig_OnTickle_Patch
{
    static bool Prefix()
    {
        return !TwigState.Disabled;
    }
}