using ConnectorLib.JSON;

namespace CrowdControl.Delegates.Effects.Implementations;

[Effect(
    id: "twig_wont_grow",
    defaultDuration: 15,
    selfConflict: true)
]
public class TwigWontGrow(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (TwigState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);

        TwigState.Disabled = true;

        return EffectResponse.Success(request.ID);
    }

    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!TwigState.Disabled) return EffectResponse.Finished(request.ID);

        TwigState.Disabled = false;

        return EffectResponse.Finished(request.ID);
    }
}