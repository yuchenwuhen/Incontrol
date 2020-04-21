﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private PlayerActions m_playerActions;

	// Use this for initialization
	void Start () {
        m_playerActions = PlayerActions.CreateWithDefaultBindings();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_playerActions.Fire.WasReleased)
        {
            Debug.Log("开火");
        }
	}
}