﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollideDirection
{
    None = 0,
    Top,
    Left,
    Right,
    Bottom
}

public class CollideChecker : MonoSingleton<CollideChecker>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float borderOffset;

    protected override void Awake()
    {
        base.Awake();
    }

    public bool IsWallCollided(Vector2 pos)
    {
        Vector2 screenPos = mainCamera.WorldToViewportPoint(pos);
        return (screenPos.x > 1 || screenPos.x < 0 || screenPos.y > 1 || screenPos.y < 0);
    }

    public CollideDirection GetWallCollidedDirection(Vector2 pos)
    {
        Vector2 screenPos = mainCamera.WorldToViewportPoint(pos);

        if (screenPos.x <= 0) return CollideDirection.Left;
        if (screenPos.x >= 1) return CollideDirection.Right;
        if (screenPos.y >= 1) return CollideDirection.Top;
        if (screenPos.y <= 0) return CollideDirection.Bottom;

        return CollideDirection.None;
    }

    public void ChangeDirection(Vector2 pos, ref Vector2 direction)
    {
        switch (GetWallCollidedDirection(pos))
        {
            case CollideDirection.Top:
            case CollideDirection.Bottom:
                {
                    direction.y *= -1;
                    break;
                }
            case CollideDirection.Left:
            case CollideDirection.Right:
                {
                    direction.x *= -1;
                    break;
                }
            default:
                break;
        }
    }
}