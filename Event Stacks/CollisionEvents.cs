using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionEvents : MonoBehaviour
{
    [Flags]
    public enum CollisionType
    {
        OnEnter = 1,
        OnExit = 2,
        OnStay = 4
    }
    public CollisionType collisionType;

    [VisibleIf(nameof(collisionType), CollisionType.OnEnter)]
    public UnityEvent<Collision> onCollisionEnter;
    [VisibleIf(nameof(collisionType), CollisionType.OnExit)]
    public UnityEvent<Collision> onCollisionExit;
    [VisibleIf(nameof(collisionType), CollisionType.OnStay)]
    public UnityEvent<Collision> onCollisionStay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionType.HasFlag(CollisionType.OnEnter))
            onCollisionEnter?.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collisionType.HasFlag(CollisionType.OnExit))
            onCollisionExit?.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collisionType.HasFlag(CollisionType.OnStay))
            onCollisionStay?.Invoke(collision);
    }
}
