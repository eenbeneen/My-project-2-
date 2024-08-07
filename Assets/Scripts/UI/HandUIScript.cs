using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIScript : MonoBehaviour
{

    public static HandUIScript Instance { get; private set; }

    [SerializeField] private JokeUIScript jokeUITemplate;

    public List<JokeUIScript> jokeUIList;


    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        jokeUIList = new List<JokeUIScript>();
        PlayerDeckManagerScript.Instance.OnJokeDrawn += PlayerDeckManagerScript_OnJokeDrawn;
    }

    private void PlayerDeckManagerScript_OnJokeDrawn(object sender, PlayerDeckManagerScript.OnJokeDrawnEventArgs e)
    {
        JokeUIScript jokeUI = Instantiate(jokeUITemplate, transform);
        jokeUI.gameObject.SetActive(true);
        jokeUI.SetJokeSO(e.jokeSO);
        jokeUI.UpdateVisual();
        jokeUIList.Add(jokeUI);
        
    }

    public void SetAllButtonsActive(bool value)
    {
        foreach (JokeUIScript jokeUI in jokeUIList)
        {
            jokeUI.SetButtonActive(value);
        }
    }
}
