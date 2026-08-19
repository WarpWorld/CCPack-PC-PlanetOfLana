using HarmonyLib;

namespace CrowdControl.Delegates.Effects.Implementations;

public static class LanaJumpState
{
    public static bool JumpDisabled = false;
    public static PoL.Actors.RigidbodyActor CachedLanaRigidbodyActor = null;
    public static PoL.Actors.RigidbodyActorEffector CachedLanaRigidbodyActorEffector = null;
}

// GroundImpulse is the shared physics method that applies a jump's velocity
// kick while grounded; blocking it covers stationary/running/sliding jumps
// all at once. Gated to Lana's own instance only.
[HarmonyPatch(typeof(PoL.Actors.RigidbodyActor), "GroundImpulse")]
public class RigidbodyActor_GroundImpulse_Patch
{
    static bool Prefix(PoL.Actors.RigidbodyActor __instance)
    {
        if (!LanaJumpState.JumpDisabled)
        {
            return true;
        }

        if (LanaJumpState.CachedLanaRigidbodyActor == null)
        {
            var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
            if (lanaInstance != null)
            {
                LanaJumpState.CachedLanaRigidbodyActor = lanaInstance.GetComponentInChildren<PoL.Actors.RigidbodyActor>();
            }
        }

        return LanaJumpState.CachedLanaRigidbodyActor != __instance;
    }
}

// RigidbodyActorEffector.Jump is a companion wrapper around RigidbodyActor,
// reached via LanaContext.rigidbodyActorEffector. Kept alongside the
// GroundImpulse patch above for full coverage.
[HarmonyPatch(typeof(PoL.Actors.RigidbodyActorEffector), "Jump")]
public class RigidbodyActorEffector_Jump_Patch
{
    static bool Prefix(PoL.Actors.RigidbodyActorEffector __instance)
    {
        if (!LanaJumpState.JumpDisabled)
        {
            return true;
        }

        if (LanaJumpState.CachedLanaRigidbodyActorEffector == null)
        {
            var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
            if (lanaInstance != null)
            {
                LanaJumpState.CachedLanaRigidbodyActorEffector = lanaInstance.GetComponentInChildren<PoL.Actors.RigidbodyActorEffector>();
            }
        }

        return LanaJumpState.CachedLanaRigidbodyActorEffector != __instance;
    }
}