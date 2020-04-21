using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingSource : BindingSource
{
    public KeyCombo Control { get; protected set; }


    internal KeyBindingSource()
    {
    }


    public KeyBindingSource(KeyCombo keyCombo)
    {
        Control = keyCombo;
    }


    public KeyBindingSource(params Key[] keys)
    {
        Control = new KeyCombo(keys);
    }

    public override BindingSourceType BindingSourceType
    {
        get
        {
            return BindingSourceType.KeyBindingSource;
        }
    }

    public override string Name
    {
        get
        {
            return Control.ToString();
        }
    }

    public override string DeviceName
    {
        get
        {
            return "Keyboard";
        }
    }

    public override bool Equals(BindingSource other)
    {
        if (other == null)
        {
            return false;
        }

        var bindingSource = other as KeyBindingSource;
        if (bindingSource != null)
        {
            return Control == bindingSource.Control;
        }

        return false;
    }

    public override bool GetState()
    {
        return Control.IsPressed;
    }

    public override float GetValue()
    {
        return GetState() ? 1.0f : 0.0f;
    }
}
