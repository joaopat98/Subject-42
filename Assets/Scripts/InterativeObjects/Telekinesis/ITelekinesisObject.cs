using UnityEngine;

public interface ITelekinesisObject
{
    void Grab(TelekinesisAbility ability);
    void Release();
    void Move(Vector3 offset);
    void SetPosition(Vector3 position);
    void Rotate(Vector3 direction, float degrees);
    Vector3 GetSelectionPosition();
    Vector3 GetPosition();
    void Highlight(bool IsActive);
    bool IsActive();
}