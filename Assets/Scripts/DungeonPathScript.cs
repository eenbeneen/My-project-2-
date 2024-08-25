using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPathScript : MonoBehaviour
{

    private const string ROUTINE = "Routine";

    public static DungeonPathScript Instance { get; private set; }

    public enum DungeonEvent {
        Routine,
        FreeReward,
        Shop,
    }

    [SerializeField] private List<DungeonEvent> path;
    private int pathProgress;

    private void Awake()
    {
        Instance = this;
    }

    public DungeonEvent GetDungeonEventOfPos(int num)
    {
        return path[pathProgress];
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
        StartEvent(path[pathProgress]);
    }

}
