using ConnectorLib.SimpleTCP;
using CrowdControl.Common;
using ConnectorType = CrowdControl.Common.ConnectorType;

namespace CrowdControl.Games.Packs.PlanetOfLana;

public class PlanetOfLana : SimpleTCPPack<SimpleTCPServerConnector>
{
    public PlanetOfLana(
        UserRecord player,
        Func<CrowdControlBlock, bool> responseHandler,
        Action<object> statusUpdateHandler
    ) : base(player, responseHandler, statusUpdateHandler)
    {
    }

    public override Game Game => new(
        "Planet of Lana",
        "PlanetOfLana",
        "PC",
        ConnectorType.SimpleTCPServerConnector
    );

    public override ushort Port => 28380;

    public override EffectList Effects => new Effect[]
    {
        new("Kill Lana", "kill_lana")
        {
            Price = 500,
            Description = "Instantly kills Lana",
        },
        new("Invert D-Pad", "invert_dpad")
        {
            Price = 200,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Inverts Lana's movement controls",
        },
        new("Kill Mui", "kill_mui")
        {
            Price = 500,
            Description = "Instantly kills Mui",
        },
        new("Lana Half Speed", "lana_half_speed")
        {
            Price = 200,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Halves Lana's movement speed",
        },
        new("Mui Ignores Follow Command", "mui_ignore_follow")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Mui refuses to follow when commanded",
        },
        new("Mui Ignores Stay Command", "mui_ignore_stay")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Mui refuses to stay when commanded",
        },
        new("Mui Ignores Go Command", "mui_ignore_go")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Mui refuses to go where directed",
        },
        new("Disable Lever Switch", "disable_box_trap_switch")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "The switch jams and won't budge",
        },
        new("Twig Branch Won't Grow", "twig_wont_grow")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "The twig ignores your touch and stays put",
        },
        new("Mui Can't Hypnotize", "mui_cant_hypnotize")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Mui's hypnosis beam fails to connect",
        },
        new("Disable Drone Control Station", "disable_control_station")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "The drone control panel stops responding",
        },
        new("Disable Small Power Buttons", "disable_platform")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Nearby power buttons stop responding",
        },
        new("Lana Invulnerable", "lana_invulnerable")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Lana can't be killed",
        },
        new("Mui Invulnerable", "mui_invulnerable")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Mui can't be killed",
        },
        new("Lana Can't Jump", "lana_disable_jump")
        {
            Price = 200,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Lana temporarily can't jump",
        },
        new("Lana Can't Crouch", "lana_disable_duck")
        {
            Price = 200,
            Duration = TimeSpan.FromSeconds(60),
            Description = "Lana temporarily can't crouch",
        },
        new("Magnet Disable", "magnet_wont_grab")
        {
            Price = 300,
            Duration = TimeSpan.FromSeconds(60),
            Description = "The crane's magnet fails to grab",
        },
    };
}
