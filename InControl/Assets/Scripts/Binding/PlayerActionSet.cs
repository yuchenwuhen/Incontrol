using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum BindingSourceType : int
{
    None = 0,
    DeviceBindingSource,
    KeyBindingSource,
    MouseBindingSource,
    UnknownDeviceBindingSource
}

public enum InputControlType : int
{
    None = 0,

    // Standardized controls.
    //
    LeftStickUp = 1,
    LeftStickDown,
    LeftStickLeft,
    LeftStickRight,
    LeftStickButton,

    RightStickUp,
    RightStickDown,
    RightStickLeft,
    RightStickRight,
    RightStickButton,

    DPadUp,
    DPadDown,
    DPadLeft,
    DPadRight,

    LeftTrigger,
    RightTrigger,

    LeftBumper,
    RightBumper,

    Action1,
    Action2,
    Action3,
    Action4,
    Action5,
    Action6,
    Action7,
    Action8,
    Action9,
    Action10,
    Action11,
    Action12,

    // Command buttons.
    // When adding to this list, update InputDevice.AnyCommandControlIsPressed() accordingly.
    Back = 100,
    Start,
    Select,
    System,
    Options,
    Pause,
    Menu,
    Share,
    Home,
    View,
    Power,
    Capture,
    Plus,
    Minus,

    // Steering controls.
    PedalLeft = 150,
    PedalRight,
    PedalMiddle,
    GearUp,
    GearDown,

    // Flight Stick controls.
    Pitch = 200,
    Roll,
    Yaw,
    ThrottleUp,
    ThrottleDown,
    ThrottleLeft,
    ThrottleRight,
    POVUp,
    POVDown,
    POVLeft,
    POVRight,

    // Unusual controls.
    //
    TiltX = 250,
    TiltY,
    TiltZ,
    ScrollWheel,

    [Obsolete("Use InputControlType.TouchPadButton instead.", true)]
    TouchPadTap,

    TouchPadButton,

    TouchPadXAxis,
    TouchPadYAxis,

    LeftSL,
    LeftSR,
    RightSL,
    RightSR,

    // Alias controls; can't be explicitly mapped in a profile.
    //
    Command = 300,
    LeftStickX,
    LeftStickY,
    RightStickX,
    RightStickY,
    DPadX,
    DPadY,

    // Generic controls (usually assigned to unknown devices).
    //
    Analog0 = 400,
    Analog1,
    Analog2,
    Analog3,
    Analog4,
    Analog5,
    Analog6,
    Analog7,
    Analog8,
    Analog9,
    Analog10,
    Analog11,
    Analog12,
    Analog13,
    Analog14,
    Analog15,
    Analog16,
    Analog17,
    Analog18,
    Analog19,

    Button0 = 500,
    Button1,
    Button2,
    Button3,
    Button4,
    Button5,
    Button6,
    Button7,
    Button8,
    Button9,
    Button10,
    Button11,
    Button12,
    Button13,
    Button14,
    Button15,
    Button16,
    Button17,
    Button18,
    Button19,

    // Internal. Must be last.
    //
    Count
}

public enum Key : int
{
    None,

    // Modifiers
    Shift,
    Alt,
    Command,
    Control,

    // Modifier keys as first class keys
    LeftShift,
    LeftAlt,
    LeftCommand,
    LeftControl,
    RightShift,
    RightAlt,
    RightCommand,
    RightControl,

    Escape,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,

    Key0,
    Key1,
    Key2,
    Key3,
    Key4,
    Key5,
    Key6,
    Key7,
    Key8,
    Key9,

    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,

    Backquote,
    Minus,
    Equals,
    Backspace,

    Tab,
    LeftBracket,
    RightBracket,
    Backslash,

    Semicolon,
    Quote,
    Return,

    Comma,
    Period,
    Slash,

    Space,

    Insert,
    Delete,
    Home,
    End,
    PageUp,
    PageDown,

    LeftArrow,
    RightArrow,
    UpArrow,
    DownArrow,

    Pad0,
    Pad1,
    Pad2,
    Pad3,
    Pad4,
    Pad5,
    Pad6,
    Pad7,
    Pad8,
    Pad9,

    Numlock,
    PadDivide,
    PadMultiply,
    PadMinus,
    PadPlus,
    PadEnter,
    PadPeriod,

    // Mac only?
    Clear,
    PadEquals,
    F13,
    F14,
    F15,

    // Other
    AltGr,
    CapsLock

    // Unity doesn't define/support these. :(
    // F16,
    // F17,
    // F18,
    // F19,
}

public enum Mouse : int
{
    None,
    LeftButton,
    RightButton,
    MiddleButton,
    NegativeX,
    PositiveX,
    NegativeY,
    PositiveY,
    PositiveScrollWheel,
    NegativeScrollWheel,
    Button4,
    Button5,
    Button6,
    Button7,
    Button8,
    Button9
}

/// <summary>
/// 输入指令集合类
/// </summary>
public abstract class PlayerActionSet {

    public ReadOnlyCollection<PlayerAction> Actions { get; private set; }

    //List<PlayerOneAxisAction> oneAxisActions = new List<PlayerOneAxisAction>();
    List<PlayerTwoAxisAction> twoAxisActions = new List<PlayerTwoAxisAction>();

    public ulong UpdateTick { get; protected set; }

    public BindingSourceType LastInputType = BindingSourceType.None;

    public ulong LastInputTypeChangedTick;

    List<PlayerAction> actions = new List<PlayerAction>();

    public InputDevice Device { get; set; }

    protected PlayerActionSet()
    {
        Actions = new ReadOnlyCollection<PlayerAction>(actions);
        InputManager.AttachPlayerActionSet(this);
    }

    public void Destroy()
    {
        InputManager.DetachPlayerActionSet(this);
    }

    protected PlayerAction CreatePlayerAction(string name)
    {
        return new PlayerAction(name, this);
    }

    protected PlayerTwoAxisAction CreateTwoAxisPlayerAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
    {
        var action = new PlayerTwoAxisAction(negativeXAction, positiveXAction, negativeYAction, positiveYAction);
        twoAxisActions.Add(action);
        return action;
    }

    internal void Update(ulong updateTick, float deltaTime)
    {

        var lastInputType = LastInputType;
        var lastInputTypeChangedTick = LastInputTypeChangedTick;

        var actionsCount = actions.Count;
        for (var i = 0; i < actionsCount; i++)
        {
            var action = actions[i];

            action.Update(updateTick, deltaTime);

            if (action.UpdateTick > UpdateTick)
            {
                UpdateTick = action.UpdateTick;
            }

            if (action.LastInputTypeChangedTick > lastInputTypeChangedTick)
            {
                lastInputType = action.LastInputType;
                lastInputTypeChangedTick = action.LastInputTypeChangedTick;
            }
        }

        var twoAxisActionsCount = twoAxisActions.Count;
        for (var i = 0; i < twoAxisActionsCount; i++)
        {
            twoAxisActions[i].Update(updateTick, deltaTime);
        }

        if (lastInputTypeChangedTick > LastInputTypeChangedTick)
        {
            var triggerEvent = lastInputType != LastInputType;

            LastInputType = lastInputType;
            LastInputTypeChangedTick = lastInputTypeChangedTick;
        }
    }

    internal void AddPlayerAction(PlayerAction action)
    {
        actions.Add(action);
    }
}
