﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithManager : MonoBehaviour
{
    public static Vector2 playerpos;
    public static float[,] array = new float[8,17];
    private float timer = 0;
    [SerializeField] private EntityPhysics _player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //playerpos
        playerpos = _player.GetComponent<Rigidbody2D>().position;

        //Ripple
        timer += Time.deltaTime;
		for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = Mathf.Sin(i + j +  timer) * 0.5f + 0.5f;
            }
        }
	}
}
