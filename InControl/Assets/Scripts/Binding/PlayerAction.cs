using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 具体的输入指令类
/// </summary>
public class PlayerAction : OneAxisInputControl {

    public string Name { get; private set; }

    List<BindingSource> defaultBindings = new List<BindingSource>();
    List<BindingSource> regularBindings = new List<BindingSource>();
    List<BindingSource> visibleBindings = new List<BindingSource>();

    public PlayerActionSet Owner;

    public PlayerAction(string name, PlayerActionSet owner)
    {
        Name = name;
        Owner = owner;
        owner.AddPlayerAction(this);
    }

    /// <summary>
    /// 添加按钮绑定
    /// </summary>
    /// <param name="keys"></param>
    public void AddDefaultBinding(params Key[] keys)
    {
        AddDefaultBinding(new KeyBindingSource(keys));
    }

    /// <summary>
    /// 添加鼠标绑定
    /// </summary>
    /// <param name="control"></param>
    public void AddDefaultBinding(Mouse control)
    {
        AddDefaultBinding(new MouseBindingSource(control));
    }

    public void AddDefaultBinding(BindingSource binding)
    {
        if (binding == null)
        {
            return;
        }

        if (binding.BoundTo != null)
        {
            throw new System.Exception("Binding source is already bound to action " + binding.BoundTo.Name);
        }

        if (!defaultBindings.Contains(binding))
        {
            defaultBindings.Add(binding);
            binding.BoundTo = this;
        }

        if (!regularBindings.Contains(binding))
        {
            regularBindings.Add(binding);
            binding.BoundTo = this;
        }
    }

    internal void Update(ulong updateTick, float deltaTime)
    {
        UpdateBindings(updateTick, deltaTime);
        DetectBindings();
    }

    public BindingSourceType LastInputType = BindingSourceType.None;
    public ulong LastInputTypeChangedTick;


    void UpdateBindings(ulong updateTick, float deltaTime)
    {
        var lastInputType = LastInputType;
        var lastInputTypeChangedTick = LastInputTypeChangedTick;
        var lastUpdateTick = UpdateTick;

        var bindingCount = regularBindings.Count;
        for (var i = bindingCount - 1; i >= 0; i--)
        {
            var binding = regularBindings[i];

            if (binding.BoundTo != this)
            {
                regularBindings.RemoveAt(i);
                visibleBindings.Remove(binding);
            }
            else
            {
                var value = binding.GetValue();
                if (UpdateWithValue(value, updateTick, deltaTime))
                {
                    lastInputType = binding.BindingSourceType;
                    lastInputTypeChangedTick = updateTick;
                }
            }
        }

        if (bindingCount == 0)
        {
            UpdateWithValue(0.0f, updateTick, deltaTime);
        }

        Commit();

        if (lastInputTypeChangedTick > LastInputTypeChangedTick)
        {
            if (lastInputType != BindingSourceType.MouseBindingSource ||
                Utility.Abs(LastValue - Value) >= MouseBindingSource.JitterThreshold)
            {
                var triggerEvent = lastInputType != LastInputType;

                LastInputType = lastInputType;
                LastInputTypeChangedTick = lastInputTypeChangedTick;
            }
        }
    }

    void DetectBindings()
    {
        
    }

}
