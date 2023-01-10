using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotator : MonoBehaviour
{
    public InputActionReference lookRef;
    public Transform pitchTarget;
    public Transform yawTarget;

    public float sens;

    public float conversion;

    private Vector2 rot;

    private void OnEnable()
    {
        lookRef.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        lookRef.action.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        rot += lookRef.action.ReadValue<Vector2>() * sens * conversion;

        float epsilon = .01f;
        rot.x %= 360f;
        rot.y = Mathf.Clamp(rot.y, -90f + epsilon, 90f - epsilon);

        pitchTarget.localRotation = Quaternion.Euler(-rot.y, pitchTarget.localEulerAngles.y, pitchTarget.localEulerAngles.z);
        yawTarget.localRotation = Quaternion.Euler(yawTarget.localEulerAngles.x, rot.x, yawTarget.localEulerAngles.z);
    }
}
