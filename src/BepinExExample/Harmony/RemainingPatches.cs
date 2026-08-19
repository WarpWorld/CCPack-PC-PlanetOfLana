using HarmonyLib;
using UnityEngine.InputSystem;

namespace CrowdControl.Delegates.Effects.Implementations;

public static class InvertState { public static bool InvertEnabled = false; }
public static class SpeedState { public static float Multiplier = 1.0f; }
public static class MuiCommandState
{
    public static bool IgnoreFollowCommand = false;
    public static bool IgnoreStayCommand = false;
    public static bool IgnoreGoCommand = false;
}
public static class PowerSwitchState { public static bool Disabled = false; }
public static class HypnosisState { public static bool Disabled = false; }
public static class ControlStationState { public static bool Disabled = false; }
public static class PlatformToggleState { public static bool Disabled = false; }
public static class MagnetState { public static bool Disabled = false; }
public static class LanaDuckState { public static bool Disabled = false; }
public static class LanaInvulnerableState { public static bool Enabled = false; }
public static class MuiInvulnerableState { public static bool Enabled = false; }

// Refresh() must run on the main thread - the framework already guarantees
// Start()/Stop() run on the main thread, so no queue/enqueue wrapper is needed here.
public static class InputOverrideManager
{
    public static void Refresh()
    {
        try
        {
            var layer = FindInputLayer();
            if (layer == null) return;

            var direction = layer.inputActions.Lana.Direction;
            InputActionRebindingExtensions.RemoveAllBindingOverrides(direction);

            string processors = null;
            if (InvertState.InvertEnabled)
            {
                processors = "invertVector2";
            }
            if (SpeedState.Multiplier != 1.0f)
            {
                string scale = "scaleVector2(x=" + SpeedState.Multiplier + ",y=" + SpeedState.Multiplier + ")";
                processors = processors == null ? scale : processors + "," + scale;
            }

            if (processors != null)
            {
                var bindingOverride = new InputBinding();
                bindingOverride.overrideProcessors = processors;
                InputActionRebindingExtensions.ApplyBindingOverride(direction, bindingOverride);
            }
        }
        catch { /* swallow, matches original behavior */ }
    }

    private static PoL.Characters.Lana.InputLayer FindInputLayer()
    {
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        if (lanaInstance != null)
        {
            var layer = lanaInstance.GetComponentInChildren<PoL.Characters.Lana.InputLayer>();
            if (layer != null) return layer;
        }
        return UnityEngine.Object.FindObjectOfType<PoL.Characters.Lana.InputLayer>();
    }
}

[HarmonyPatch(typeof(PoL.Gameplay.PowerSwitch), "SwitchOn")]
public class PowerSwitch_SwitchOn_Patch
{
    static bool Prefix(PoL.Gameplay.PowerSwitch __instance)
    {
        if (!PlatformToggleState.Disabled) return true;
        return __instance.name != "Direction Toggler";
    }
}

[HarmonyPatch(typeof(PoL.Gameplay.Cranes.CraneMagnet), "UpdateUse")]
public class CraneMagnet_UpdateUse_Patch
{
    static bool Prefix() => !MagnetState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Markup.ControlStation), "SetActivated")]
public class ControlStation_SetActivated_Patch
{
    static bool Prefix() => !ControlStationState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Markup.ControlStation), "Activate")]
public class ControlStation_Activate_Patch
{
    static bool Prefix() => !ControlStationState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.TriggerActionDeathArea), "Execute")]
public class TriggerActionDeathArea_Execute_Patch
{
    private static bool wasLanaInvulnerable;
    private static bool wasMuiInvulnerable;

    static void Prefix()
    {
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        var muiInstance = PoL.Characters.Mui.MuiMachine.instance;

        wasLanaInvulnerable = lanaInstance != null && lanaInstance.IsInvulnerable;
        wasMuiInvulnerable = muiInstance != null && muiInstance.IsInvulnerable;

        if (lanaInstance != null) lanaInstance.IsInvulnerable = false;
        if (muiInstance != null) muiInstance.IsInvulnerable = false;
    }

    static void Postfix()
    {
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        var muiInstance = PoL.Characters.Mui.MuiMachine.instance;

        if (lanaInstance != null && wasLanaInvulnerable) lanaInstance.IsInvulnerable = true;
        if (muiInstance != null && wasMuiInvulnerable) muiInstance.IsInvulnerable = true;
    }
}

[HarmonyPatch(typeof(HypnosisBeam), "SetTarget")]
public class HypnosisBeam_SetTarget_Patch
{
    static bool Prefix() => !HypnosisState.Disabled;
}

[HarmonyPatch(typeof(PoL.Characters.Mui.MuiAnimator), "InteractHypnotize")]
public class MuiAnimator_InteractHypnotize_Patch
{
    static bool Prefix() => !HypnosisState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Interaction.BoxTrapSwitch), "SetTargetDirection")]
public class BoxTrapSwitch_SetTargetDirection_Patch
{
    static bool Prefix() => !PowerSwitchState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Interaction.BoxTrapSwitch), "SetTargetDirectionLeft")]
public class BoxTrapSwitch_SetTargetDirectionLeft_Patch
{
    static bool Prefix() => !PowerSwitchState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Interaction.BoxTrapSwitch), "SetTargetDirectionRight")]
public class BoxTrapSwitch_SetTargetDirectionRight_Patch
{
    static bool Prefix() => !PowerSwitchState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.Interaction.BoxTrapSwitch), "TargetDirection", MethodType.Setter)]
public class BoxTrapSwitch_TargetDirection_Setter_Patch
{
    static bool Prefix() => !PowerSwitchState.Disabled;
}

[HarmonyPatch(typeof(PoL.Characters.Mui.MuiFollowBehaviour), "OnUpdate")]
public class MuiFollowBehaviour_OnUpdate_Patch
{
    static bool Prefix() => !MuiCommandState.IgnoreFollowCommand;
}

[HarmonyPatch(typeof(PoL.Characters.Mui.MuiGoToBehaviour), "OnUpdate")]
public class MuiGoToBehaviour_OnUpdate_Patch
{
    static bool Prefix() => !MuiCommandState.IgnoreFollowCommand;
}

[HarmonyPatch(typeof(PoL.Characters.Mui.Communication), "OnCommand")]
public class MuiCommunication_OnCommand_Patch
{
    static bool Prefix(PoL.Gameplay.CommunicationCommand __0)
    {
        if (__0 == PoL.Gameplay.CommunicationCommand.Stay && MuiCommandState.IgnoreStayCommand) return false;
        if (__0 == PoL.Gameplay.CommunicationCommand.Go && MuiCommandState.IgnoreGoCommand) return false;
        if (__0 == PoL.Gameplay.CommunicationCommand.Interact && HypnosisState.Disabled) return false;
        return true;
    }
}

[HarmonyPatch(typeof(PoL.Characters.Lana.LanaAnimator), "Crouch")]
public class LanaAnimator_Crouch_Patch
{
    static bool Prefix() => !LanaDuckState.Disabled;
}

[HarmonyPatch(typeof(PoL.Gameplay.GameplayEvents), "DeathMark")]
public class GameplayEvents_DeathMark_Patch
{
    static bool Prefix(PoL.Characters.ICharacter __0)
    {
        var lanaInstance = PoL.Characters.Lana.LanaMachine.instance;
        if (LanaInvulnerableState.Enabled && lanaInstance != null && __0.TryCast<PoL.Characters.Lana.LanaMachine>() != null)
        {
            return false;
        }
        return true;
    }
}

public static class FanSuspensionManager
{
    public static void Refresh()
    {
        bool shouldSuspend = LanaInvulnerableState.Enabled || MuiInvulnerableState.Enabled;
        foreach (var fan in UnityEngine.Object.FindObjectsOfType<PoL.Gameplay.Fan.Fan>())
        {
            fan.Suspend(shouldSuspend);
        }
    }
}