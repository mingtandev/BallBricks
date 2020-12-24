﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoSingleton<BallLauncher>
{
    const string BALL_TAG = "Ball";

    [Header("Balls")]
    [SerializeField] List<GameObject> Balls;
    [SerializeField] List<BallScript> BallScripts = new List<BallScript>();

    [Header("Moving control")]
    [SerializeField] float _speed;
    public float Speed
    {
        get { return _speed; }
        set { 
            _speed = value;
            e_OnSpeedChange?.Invoke(value);
        }
    }
    [SerializeField] float intervalTime;
    [SerializeField] WaitForSeconds fireInterval;

    [Header("Flags")]
    public bool isMoving;

    [Header("Events")]
    public Action<float> e_OnSpeedChange;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        isMoving = false;

        for (int i = 0; i < Balls.Count; i++)
        {
            Balls[i] = ObjectPool.Instance.Spawn(BALL_TAG);
            Balls[i].SetActive(true);
            BallScripts.Add(Balls[i].GetComponent<BallScript>());
        }
    }

    private void OnValidate()
    {
        fireInterval = new WaitForSeconds(intervalTime);
        e_OnSpeedChange?.Invoke(Speed);
    }

    public void StartFire(Vector2 direction)
    {
        isMoving = true;
        StartCoroutine(CR_Fire(direction));
    }

    IEnumerator CR_Fire(Vector2 direction)
    {
        for (int i = 0; i < BallScripts.Count; i++)
        {
            BallScripts[i].Fire(direction);
            yield return fireInterval;
        }
    }
}
