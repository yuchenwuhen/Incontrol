using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InControlManager : SingletonMonoBehavior<InControlManager, MonoBehaviour>
{
    public bool useFixedUpdate = false;

    bool dontDestroyOnLoad = true;

    private void OnEnable()
    {
        if (EnforceSingleton() == false)
        {
            return;
        }

        if (InputManager.SetupInternal())
        {

        }

        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        SceneManager.sceneLoaded += OnSceneWasLoaded;

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        InputManager.OnLevelWasLoaded();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }

    void Update()
    {
        if (!useFixedUpdate || Utility.IsZero(Time.timeScale))
        {
            InputManager.UpdateInternal();
        }
    }


    void FixedUpdate()
    {
        if (useFixedUpdate)
        {
            InputManager.UpdateInternal();
        }
    }

}
