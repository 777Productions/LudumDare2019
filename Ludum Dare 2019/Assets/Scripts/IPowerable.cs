using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerable
{
    bool IsPowered();

    bool IsActive();

    void SetState(bool state);
}
