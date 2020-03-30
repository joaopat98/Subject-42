using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElectricObject
{
    void Activate();

    Vector3 GetSelectionPosition();

    void Highlight(bool isActive);
}
