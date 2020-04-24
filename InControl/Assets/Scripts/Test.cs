using System.Collections;
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
		if (m_playerActions.Fire.WasPressed)
        {
            Debug.Log("开火");
        }

        if (m_playerActions.Move.X != 0)
        {
            Debug.Log("rotate");
        }
        transform.Translate(Vector3.right  * Time.deltaTime * m_playerActions.Move.X, Space.World);
        transform.Translate(Vector3.up  * Time.deltaTime * m_playerActions.Move.Y, Space.World);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 30), "last Input Type : " + m_playerActions.LastInputType);
    }
}
