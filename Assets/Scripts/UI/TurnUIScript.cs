using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUIScript : MonoBehaviour
{

    public static TurnUIScript Instance {  get; private set; }


    [SerializeField] private GameObject yourTurnUI;
    [SerializeField] private GameObject enemyTurnUI;

    [SerializeField] private TextMeshProUGUI yourJokesLeftText;
    [SerializeField] private TextMeshProUGUI enemyJokesLeftText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;

    }

    private void OnDisable()
    {
        GameManagerScript.Instance.OnPlayerTurnStart -= GameManagerScript_OnPlayerTurnStart;
        GameManagerScript.Instance.OnEnemyTurnStart -= GameManagerScript_OnEnemyTurnStart;
    }

    public void UpdateSecondsText(int seconds, bool isForPlayer)
    {
        if (isForPlayer)
        {
            yourJokesLeftText.text = seconds.ToString() + "s LEFT";
        }
        else
        {
            enemyJokesLeftText.text = seconds.ToString() + "s LEFT";
        }
        
    }

    private void GameManagerScript_OnEnemyTurnStart(object sender, System.EventArgs e)
    {
        enemyTurnUI.SetActive(true);
        yourTurnUI.SetActive(false);
        enemyJokesLeftText.text = GameManagerScript.Instance.secondsPerTurn.ToString() + "s LEFT";
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, System.EventArgs e)
    {
        enemyTurnUI.SetActive(false);
        yourTurnUI.SetActive(true);
        yourJokesLeftText.text = GameManagerScript.Instance.secondsPerTurn.ToString() + "s LEFT";
    }
}
