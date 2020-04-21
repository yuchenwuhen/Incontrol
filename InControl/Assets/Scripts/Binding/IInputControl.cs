using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputControl {

    bool HasChanged { get; }
    bool IsPressed { get; }
    bool WasPressed { get; }
    bool WasReleased { get; }
    void ClearInputState();
}
