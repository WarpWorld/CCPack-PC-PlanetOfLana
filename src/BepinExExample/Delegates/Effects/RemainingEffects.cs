using ConnectorLib.JSON;

namespace CrowdControl.Delegates.Effects.Implementations;

[Effect(id: "kill_lana", selfConflict: true)]
public class KillLana(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        if (lanaInstance == null) return EffectResponse.Failure(request.ID, StandardErrors.ExceptionThrown);

        PoL.Gameplay.GameplayEvents.DeathMark(lanaInstance.Cast<PoL.Characters.ICharacter>());
        var audioBridge = lanaInstance.GetComponent<PoL.Characters.Lana.AudioBridge>();
        if (audioBridge != null) audioBridge.DeathStinger(PoL.Gameplay.DeathScenario.None);

        return EffectResponse.Success(request.ID);
    }

    public override EffectResponse? Stop(EffectRequest request) => EffectResponse.Finished(request.ID);
}

[Effect(id: "kill_mui", selfConflict: true)]
public class KillMui(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        var muiInstance = PoL.Characters.Mui.MuiMachine.instance;
        if (muiInstance == null) return EffectResponse.Failure(request.ID, StandardErrors.ExceptionThrown);

        PoL.Gameplay.GameplayEvents.DeathMark(muiInstance.Cast<PoL.Characters.ICharacter>());

        return EffectResponse.Success(request.ID);
    }

    public override EffectResponse? Stop(EffectRequest request) => EffectResponse.Finished(request.ID);
}

[Effect(id: "invert_dpad", defaultDuration: 60, selfConflict: true)]
public class InvertDpad(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (InvertState.InvertEnabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        InvertState.InvertEnabled = true;
        InputOverrideManager.Refresh();
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!InvertState.InvertEnabled) return EffectResponse.Finished(request.ID);
        InvertState.InvertEnabled = false;
        InputOverrideManager.Refresh();
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "lana_half_speed", defaultDuration: 30, selfConflict: true)]
public class LanaHalfSpeed(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (SpeedState.Multiplier != 1.0f) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        SpeedState.Multiplier = 0.5f;
        InputOverrideManager.Refresh();
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (SpeedState.Multiplier == 1.0f) return EffectResponse.Finished(request.ID);
        SpeedState.Multiplier = 1.0f;
        InputOverrideManager.Refresh();
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "mui_ignore_follow", defaultDuration: 15, selfConflict: true)]
public class MuiIgnoreFollow(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (MuiCommandState.IgnoreFollowCommand) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        MuiCommandState.IgnoreFollowCommand = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!MuiCommandState.IgnoreFollowCommand) return EffectResponse.Finished(request.ID);
        MuiCommandState.IgnoreFollowCommand = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "mui_ignore_stay", defaultDuration: 15, selfConflict: true)]
public class MuiIgnoreStay(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (MuiCommandState.IgnoreStayCommand) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        MuiCommandState.IgnoreStayCommand = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!MuiCommandState.IgnoreStayCommand) return EffectResponse.Finished(request.ID);
        MuiCommandState.IgnoreStayCommand = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "mui_ignore_go", defaultDuration: 15, selfConflict: true)]
public class MuiIgnoreGo(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (MuiCommandState.IgnoreGoCommand) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        MuiCommandState.IgnoreGoCommand = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!MuiCommandState.IgnoreGoCommand) return EffectResponse.Finished(request.ID);
        MuiCommandState.IgnoreGoCommand = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "disable_box_trap_switch", defaultDuration: 30, selfConflict: true)]
public class DisableLeverSwitch(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (PowerSwitchState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        PowerSwitchState.Disabled = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!PowerSwitchState.Disabled) return EffectResponse.Finished(request.ID);
        PowerSwitchState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "mui_cant_hypnotize", defaultDuration: 30, selfConflict: true)]
public class MuiCantHypnotize(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (HypnosisState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        HypnosisState.Disabled = true;

        // Blocking SetTarget/InteractHypnotize only prevents NEW attempts - if a hypnosis
        // beam is already active/mid-flight, actively stop and clear it too.
        var activeBeam = UnityEngine.Object.FindObjectOfType<HypnosisBeam>();
        if (activeBeam != null)
        {
            activeBeam.StopAndClear();
        }

        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!HypnosisState.Disabled) return EffectResponse.Finished(request.ID);
        HypnosisState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "disable_control_station", defaultDuration: 30, selfConflict: true)]
public class DisableControlStation(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (ControlStationState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        ControlStationState.Disabled = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!ControlStationState.Disabled) return EffectResponse.Finished(request.ID);
        ControlStationState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "disable_platform", defaultDuration: 30, selfConflict: true)]
public class DisablePlatform(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (PlatformToggleState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        PlatformToggleState.Disabled = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!PlatformToggleState.Disabled) return EffectResponse.Finished(request.ID);
        PlatformToggleState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "magnet_wont_grab", defaultDuration: 30, selfConflict: true)]
public class MagnetDisable(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (MagnetState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        MagnetState.Disabled = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!MagnetState.Disabled) return EffectResponse.Finished(request.ID);
        MagnetState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "lana_disable_duck", defaultDuration: 30, selfConflict: true)]
public class LanaCantCrouch(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (LanaDuckState.Disabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        LanaDuckState.Disabled = true;
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!LanaDuckState.Disabled) return EffectResponse.Finished(request.ID);
        LanaDuckState.Disabled = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "lana_invulnerable", defaultDuration: 45, selfConflict: true)]
public class LanaInvulnerable(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (LanaInvulnerableState.Enabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        LanaInvulnerableState.Enabled = true;
        FanSuspensionManager.Refresh();
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!LanaInvulnerableState.Enabled) return EffectResponse.Finished(request.ID);
        LanaInvulnerableState.Enabled = false;
        FanSuspensionManager.Refresh();
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        if (lanaInstance != null) lanaInstance.IsInvulnerable = false;
        return EffectResponse.Finished(request.ID);
    }
}

[Effect(id: "mui_invulnerable", defaultDuration: 45, selfConflict: true)]
public class MuiInvulnerable(CrowdControlMod mod, NetworkClient client) : Effect(mod, client)
{
    public override EffectResponse Start(EffectRequest request)
    {
        if (MuiInvulnerableState.Enabled) return EffectResponse.Failure(request.ID, StandardErrors.AlreadyInState);
        MuiInvulnerableState.Enabled = true;
        FanSuspensionManager.Refresh();
        return EffectResponse.Success(request.ID);
    }
    public override EffectResponse? Stop(EffectRequest request)
    {
        if (!MuiInvulnerableState.Enabled) return EffectResponse.Finished(request.ID);
        MuiInvulnerableState.Enabled = false;
        FanSuspensionManager.Refresh();
        var muiInstance = PoL.Characters.Mui.MuiMachine.instance;
        if (muiInstance != null) muiInstance.IsInvulnerable = false;
        return EffectResponse.Finished(request.ID);
    }
}