using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoAxisAction : TwoAxisInputControl {

    PlayerAction negativeXAction;
    PlayerAction positiveXAction;
    PlayerAction negativeYAction;
    PlayerAction positiveYAction;

    public BindingSourceType LastInputType = BindingSourceType.None;

    public bool InvertYAxis { get; set; }

    public bool InvertXAxis { get; set; }

    internal PlayerTwoAxisAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
    {
        this.negativeXAction = negativeXAction;
        this.positiveXAction = positiveXAction;
        this.negativeYAction = negativeYAction;
        this.positiveYAction = positiveYAction;
    }

    void ProcessActionUpdate(PlayerAction action)
    {
        var lastInputType = LastInputType;

        if (action.UpdateTick > UpdateTick)
        {
            UpdateTick = action.UpdateTick;
            lastInputType = action.LastInputType;
        }

        if (LastInputType != lastInputType)
        {
            LastInputType = lastInputType;
        }
    }

    internal void Update(ulong updateTick, float deltaTime)
    {
        ProcessActionUpdate(negativeXAction);
        ProcessActionUpdate(positiveXAction);
        ProcessActionUpdate(negativeYAction);
        ProcessActionUpdate(positiveYAction);

        var x = Utility.ValueFromSides(negativeXAction, positiveXAction, InvertXAxis);
        var y = Utility.ValueFromSides(negativeYAction, positiveYAction, InvertYAxis);
        UpdateWithAxes(x, y, updateTick, deltaTime);
    }
}
