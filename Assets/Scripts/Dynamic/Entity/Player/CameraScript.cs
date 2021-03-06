﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraScript : MonoBehaviour
{
    private const float OFFSET_MAGNITUDE_X = 16f * 0.75f;
    private const float OFFSET_MAGNITUDE_Y = 9f * 0.75f;
    [SerializeField] private EntityPhysics _playerPhysics;
    public Transform player;
    public float smoothTime;
    public float lerpAmount = 0.5f;
    //[SerializeField] private InputHandler input;
    private Player controller;
    private Vector3 velocity = Vector3.zero;
    private bool _isUsingCursor;
    private Vector2 _cursorWorldPos;

    private List<CameraAttractor> _currentAttractors;


    public bool IsUsingMouse
    {
        get { return _isUsingCursor; }
        set { _isUsingCursor = value; }
    }

    public void AddAttractor(CameraAttractor attractor)
    {
        if (!_currentAttractors.Contains(attractor))
        {
            _currentAttractors.Add(attractor);
        }
        else
        {
            Debug.LogWarning("AddAttractor() already in list!");
        }
    }

    public void RemoveAttractor(CameraAttractor attractor)
    {
        if (_currentAttractors.Contains(attractor))
        {
            _currentAttractors.Remove(attractor);
        }
        else
        {
            Debug.LogWarning("RemoveAttractor() attempt failed : CameraAttractor not in list!");
        }
    }


    private void Awake()
    {
        controller = ReInput.players.GetPlayer(0);
        _currentAttractors = new List<CameraAttractor>();
    }

    void Update()
    {
        Vector3 targetPosition = UpdateTargetPosition();
        foreach (CameraAttractor attractor in _currentAttractors)
        {
            targetPosition = (targetPosition + attractor.transform.position * attractor.PullMagnitude) / (attractor.PullMagnitude + 1f);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, lerpAmount);
    }

    /// <summary>
    /// Updates the position the camera follows.
    /// </summary>
    Vector3 UpdateTargetPosition()
    {
        Vector3 pos = player.TransformPoint(new Vector3(0f, 0f, -150f));
        Vector2 offset;
        if (_isUsingCursor)
        {
            offset = new Vector2(_cursorWorldPos.x, _cursorWorldPos.y - _playerPhysics.GetBottomHeight() - 1f) - (Vector2)_playerPhysics.GetComponent<Transform>().position;
            offset *= 0.05f;
            if (offset.sqrMagnitude > 1f)
            {
                offset.Normalize();
            }
        }
        else
        {
            offset = controller.GetAxis2DRaw("LookHorizontal", "LookVertical"); //moves camera in direction the stick is pointing
            if (offset.magnitude > 0.1f)
            {
                offset = controller.GetAxis2DRaw("LookHorizontal", "LookVertical");
            }
            else
            {
                offset = controller.GetAxis2DRaw("MoveHorizontal", "MoveVertical");
            }
        }
        pos.Set(pos.x + offset.x * OFFSET_MAGNITUDE_X, pos.y + offset.y * OFFSET_MAGNITUDE_Y, pos.z);
        return pos;
    }

    //------------| Camera Effects (wrapper methods for coroutines)
    
    /// <summary>
    /// Vibrates the camera.
    /// </summary>
    /// <param name="intensity">Max distance the camera is allowed to move</param>
    /// <param name="repetitions">How often the camera's position is moved</param>
    /// <param name="timeBetweenJolts">Time between position adjustments</param>
    public void Shake(float intensity, int repetitions, float timeBetweenJolts)
    {
        //Debug.Log("Camera shaking!");

        StartCoroutine(CameraShake(intensity, repetitions, timeBetweenJolts));
    }

    /// <summary>
    /// Jolt camera in a random direction
    /// </summary>
    /// <param name="intensity">Distance camera is moved</param>
    public void Jolt(float intensity)
    {
        System.Random rand = new System.Random();
        Jolt(intensity, new Vector2((float)(rand.NextDouble()*2.0-1.0), (float)(rand.NextDouble()*2.0-1.0)));
    }

    public void Jolt(float intensity, Vector2 direction)
    {
        intensity *= AccessibilityOptionsSingleton.GetInstance().ScreenshakeAmount;
        //Debug.Log("Camera Jolt!");
        if (direction.magnitude == 0)
        {
            Jolt(intensity);
            return;
        }
        Vector3 originalpos = gameObject.GetComponent<Transform>().position;
        gameObject.GetComponent<Transform>().position = new Vector3(originalpos.x + direction.normalized.x*intensity, originalpos.y + direction.normalized.y*intensity, originalpos.z);
    }


    //-----------| Coroutines 

    IEnumerator CameraShake(float intensity, int repetitions, float timeBetweenJolts)
    {
        //Debug.Log("Camera shaking!");
        intensity *= AccessibilityOptionsSingleton.GetInstance().ScreenshakeAmount;
        System.Random rand = new System.Random();
        Vector3 originalpos = gameObject.GetComponent<Transform>().position;
        Vector3 newpos = originalpos;
        for (float i = 0; i < repetitions; i++)
        {
            originalpos = gameObject.GetComponent<Transform>().position;
            gameObject.GetComponent<Transform>().position = new Vector3(originalpos.x + (float)(rand.NextDouble()*2-1)*intensity, originalpos.y + (float)(rand.NextDouble() * 2 - 1)*intensity, originalpos.z);
            yield return new WaitForSeconds(timeBetweenJolts);
        }
    }

    public void UpdateMousePosition(Vector2 position)
    {
        _isUsingCursor = true;
        _cursorWorldPos = position;

    }

}

