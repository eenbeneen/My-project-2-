using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{

    private const string DUNGEON_SCENE = "Dungeon";

    

    public void StartDungeon()
    {
        SceneManager.LoadScene(DUNGEON_SCENE);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
