using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonManagerScript : MonoBehaviour
{

    private const string ROUTINE = "Routine";

    public static DungeonManagerScript Instance { get; private set; }

    public event EventHandler OnEnterShop;

    public enum DungeonEvent {
        Routine,
        FreeReward,
        Shop,
    }

    [SerializeField] private List<DungeonEvent> possibleEvents;
    private int pathProgress;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        Instance = this;
    }

    

    private void StartEvent(DungeonEvent dungeonEvent)
    {
        switch(dungeonEvent)
        {
            case DungeonEvent.Routine:
                SceneManager.LoadScene(ROUTINE);
                break;
            case DungeonEvent.FreeReward:
                break;
            case DungeonEvent.Shop:
                StartShop();
                break;
        }
        pathProgress++;
    }

    public void StartCurrentEvent()
    {
        StartEvent(DungeonEvent.Routine);
    }

    public void StartShop()
    {
        OnEnterShop?.Invoke(this, EventArgs.Empty);
    }

}
