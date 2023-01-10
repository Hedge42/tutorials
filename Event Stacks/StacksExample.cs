using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StacksExample : MonoBehaviour
{
    [Flags]
    public enum Method
    {
        Awake = 1,
        OnEnable = 2,
        OnDisable = 4,
        Start = 8,
        Update = 16
    }
    public Method method;

    [EventDrawer, VisibleIf(nameof(method), Method.Awake)]
    public UnityEvent onAwake;
    [EventDrawer, VisibleIf(nameof(method), Method.OnEnable)]
    public UnityEvent onEnable;
    [EventDrawer, VisibleIf(nameof(method), Method.OnDisable)]
    public UnityEvent onDisable;
    [EventDrawer, VisibleIf(nameof(method), Method.Start)]
    public UnityEvent onStart;
    [EventDrawer, VisibleIf(nameof(method), Method.Update)]
    public UnityEvent onUpdate;
}
