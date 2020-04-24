using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : PlayerActionSet {

    public PlayerAction Fire;
    //public PlayerAction Jump;
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerTwoAxisAction Move;

    public PlayerActions()
    {
        Fire = CreatePlayerAction("Fire");
        //Jump = CreatePlayerAction("Jump");
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Up = CreatePlayerAction("Move Up");
        Down = CreatePlayerAction("Move Down");
        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
    }

    public static PlayerActions CreateWithDefaultBindings()
    {
        var playerActions = new PlayerActions();

        playerActions.Fire.AddDefaultBinding(Key.Space);
        playerActions.Fire.AddDefaultBinding(InputControlType.Action1);
        playerActions.Fire.AddDefaultBinding(Mouse.LeftButton);

        //playerActions.Up.AddDefaultBinding(Mouse.PositiveY);
        //playerActions.Down.AddDefaultBinding(Mouse.NegativeY);
        //playerActions.Left.AddDefaultBinding(Mouse.NegativeX);
        //playerActions.Right.AddDefaultBinding(Mouse.PositiveX);
        playerActions.Up.AddDefaultBinding(Key.W);
        playerActions.Down.AddDefaultBinding(Key.S);
        playerActions.Left.AddDefaultBinding(Key.A);
        playerActions.Right.AddDefaultBinding(Key.D);

        return playerActions;
    }
}
