using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

	public static bool IsSetup { get; private set; }

    public static bool CommandWasPressed { get; private set; }

    static float initialTime;
    static float currentTime;
    static float lastUpdateTime;
    static ulong currentTick;

    public static bool InvertYAxis { get; set; }

    static List<PlayerActionSet> playerActionSets = new List<PlayerActionSet>();

    internal static bool SetupInternal()
    {
        if (IsSetup)
        {
            return false;
        }

        initialTime = 0.0f;
        currentTime = 0.0f;
        lastUpdateTime = 0.0f;
        currentTick = 0;

        playerActionSets.Clear();

        IsSetup = true;

        return true;
    }

    internal static void UpdateInternal()
    {
        AssertIsSetup();

        currentTick++;
        UpdateCurrentTime();
        var deltaTime = currentTime - lastUpdateTime;

        CommandWasPressed = false;

        UpdatePlayerActionSets(deltaTime);

        lastUpdateTime = currentTime;
    }

    internal static void UpdatePlayerActionSets(float deltaTime)
    {
        var playerActionSetCount = playerActionSets.Count;
        for (var i = 0; i < playerActionSetCount; i++)
        {
            playerActionSets[i].Update(currentTick, deltaTime);
        }
    }

    static void UpdateCurrentTime()
    {
        // Have to do this hack since Time.realtimeSinceStartup is not set until AFTER Awake().
        if (initialTime < float.Epsilon)
        {
            initialTime = Time.realtimeSinceStartup;
        }

        currentTime = Mathf.Max(0.0f, Time.realtimeSinceStartup - initialTime);
    }

    internal static void OnLevelWasLoaded()
    {
        UpdateInternal();
    }

    static void AssertIsSetup()
    {
        if (!IsSetup)
        {
            throw new System.Exception("InputManager is not initialized. Call InputManager.Setup() first.");
        }
    }

    internal static void AttachPlayerActionSet(PlayerActionSet playerActionSet)
    {
        if (!playerActionSets.Contains(playerActionSet))
        {
            playerActionSets.Add(playerActionSet);
        }
    }


    internal static void DetachPlayerActionSet(PlayerActionSet playerActionSet)
    {
        playerActionSets.Remove(playerActionSet);
    }
}
