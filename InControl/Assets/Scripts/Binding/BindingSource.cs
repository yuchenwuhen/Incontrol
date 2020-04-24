using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BindingSource {

    public abstract BindingSourceType BindingSourceType { get; }

    public abstract float GetValue(InputDevice inputDevice);

    public abstract bool GetState(InputDevice inputDevice);

    public abstract bool Equals(BindingSource other);

    public abstract string Name { get; }

    public abstract string DeviceName { get; }

    internal PlayerAction BoundTo { get; set; }
}
