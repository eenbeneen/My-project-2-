using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonPathScript : MonoBehaviour
{

    private const string ROUTINE = "Routine";

    public static DungeonPathScript Instance { get; private set; }

    public enum DungeonEvent {
        Routine,
        FreeReward,
        Shop,
    }

    [SerializeField] private List<DungeonEvent> possibleEvents;
    private int pathProgress;

    private void Awake()
    {
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
                break;
        }
        pathProgress++;
    }

    public void StartCurrentEvent()
    {
        StartEvent(DungeonEvent.Routine);
    }

}
