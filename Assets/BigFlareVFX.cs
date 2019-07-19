﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFlareVFX : MonoBehaviour
{
    private static List<GameObject> _objectPool;

    public static void InitializePool()
    {
        _objectPool = new List<GameObject>();
        _objectPool.Add(Instantiate(Resources.Load("Prefabs/VFX/BigFlare", typeof(GameObject)) as GameObject));

        foreach (GameObject o in _objectPool)
        {
            o.SetActive(false);
        }
    }

    public static GameObject DeployFromPool(Vector3 pos)
    {
        if (_objectPool == null) InitializePool();
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if (_objectPool[i] == null) //should take care of reloading scenes - its hacky, should probably do on scene load or something
            {
                _objectPool[i] = Instantiate(Resources.Load("Prefabs/VFX/BigFlare", typeof(GameObject)) as GameObject);
                _objectPool[i].SetActive(false);
            }
            if (!_objectPool[i].activeSelf)
            {
                _objectPool[i].SetActive(true);
                _objectPool[i].transform.position = pos;
                return _objectPool[i];
            }
        }
        //if no inactive objects exist, make a new one and deploy it
        _objectPool.Add(Instantiate(Resources.Load("Prefabs/VFX/BigFlare", typeof(GameObject)) as GameObject));
        _objectPool[_objectPool.Count - 1].SetActive(false);
        var deployedObj = DeployFromPool(pos);
        deployedObj.transform.position = pos;
        return deployedObj;
    }

    private void OnEnable()
    {
        GetComponent<Animator>().Play("BigFlare");
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
}
