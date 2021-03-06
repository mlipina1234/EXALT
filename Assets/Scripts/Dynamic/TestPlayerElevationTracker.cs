﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerElevationTracker : MonoBehaviour
{
    [SerializeField] private EntityPhysics _playerPhysics;
    [SerializeField] private EnvironmentPhysics _environmentPhysics;
    [SerializeField] private bool _trueIfPlatformFalseIfWall;
    [SerializeField] private bool _canChangeElevation = false;
    private float _lerpedPlayerHeight;

	// Use this for initialization
	void Start ()
    {
        _lerpedPlayerHeight = _playerPhysics.GetObjectElevation();
        if (_trueIfPlatformFalseIfWall)
        {
            //platform

            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());

            //changes field for all instances
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());
        }
        else
        {
            //wall
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());

            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());
        }
    }

    public void UpdateShader()
    {
        if (_trueIfPlatformFalseIfWall)
        {
            //platform

            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());

            //changes field for all instances
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());
        }
        else
        {
            //wall
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());

            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());
        }
    }

    private void Update()
    {
        if (_canChangeElevation) UpdateShader();
    }


    /*
    // Update is called once per frame
    void Update ()
    {
        
        
        _lerpedPlayerHeight = Mathf.Lerp( _lerpedPlayerHeight, _playerPhysics.GetObjectElevation(), 0.1f);
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_PlayerElevation", _lerpedPlayerHeight);
       
        //Shader.SetGlobalFloat("_PlayerElevation", _lerpedPlayerHeight);
        //Shader.SetGlobalFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());
        //Shader.SetGlobalFloat("_TopElevation", _environmentPhysics.GetTopHeight());
        //Shader.SetGlobalFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());
        if (_trueIfPlatformFalseIfWall)
        {
            //platform

            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());

            //changes field for all instances
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_PlatformElevation", _environmentPhysics.GetTopHeight());
        }
        else
        {
            //wall
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());

            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_TopElevation", _environmentPhysics.GetTopHeight());
            //gameObject.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_BottomElevation", _environmentPhysics.GetBottomHeight());
        }
    }
    */
}
