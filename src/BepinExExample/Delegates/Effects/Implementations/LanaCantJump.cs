using ConnectorLib.JSON;

namespace CrowdControl.Delegates.Effects.Implementations;

[Effect(
    id: "lana_disable_jump",
    defaultDuration: 15,
    selfConflict: true)
]
public class LanaCantJump(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (LanaJumpState.JumpDisabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);

        LanaJumpState.JumpDisabled = true;

        return EffectResponse.Success(request.ID);
    }

    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!LanaJumpState.JumpDisabled) return EffectResponse.Finished(request.ID);

        LanaJumpState.JumpDisabled = false;

        return EffectResponse.Finished(request.ID);
    }
}