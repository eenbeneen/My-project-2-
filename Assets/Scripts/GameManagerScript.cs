using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private const string DUNGEON = "Dungeon";

    public static GameManagerScript Instance { get; private set; }

    public event EventHandler OnPlayerTurnStart;
    public event EventHandler OnEnemyTurnStart;
    public event EventHandler OnMatchStart;

    public int secondsPerTurn;

    private int secondsLeftThisTurn;

    public bool isPlayerTurn;
    public bool isEnemyTurn;

    private float startTimer = 1f;
    private bool gameStarted;

    [SerializeField] private GameObject winScreenUI;
    [SerializeField] private GameObject lossScreenUI;
    [SerializeField] private Button endTurnButton;

    public List<JokeSOScript> jokesPlayed = new List<JokeSOScript>();


    private void Awake()
    {
        Instance = this;

        winScreenUI.SetActive(false);
        lossScreenUI.SetActive(false);

        endTurnButton.onClick.AddListener(delegate { StartEnemyTurn(); });
        
    }

    private void Start()
    {
        PlayerDeckManagerScript.Instance.OnJokePlayed += PlayerDeckManagerScript_OnJokePlayed;
        EnemyDeckManagerScript.Instance.OnEnemyJokePlayed += EnemyDeckManagerScript_OnEnemyJokePlayed;

        AudienceScript.Instance.OnMeterFull += AudienceScript_OnMeterFull;
        AudienceScript.Instance.OnMeterEmpty += AudienceScript_OnMeterEmpty;
    }

    private void OnDisable()
    {
        PlayerDeckManagerScript.Instance.OnJokePlayed -= PlayerDeckManagerScript_OnJokePlayed;
        EnemyDeckManagerScript.Instance.OnEnemyJokePlayed -= EnemyDeckManagerScript_OnEnemyJokePlayed;

        AudienceScript.Instance.OnMeterFull -= AudienceScript_OnMeterFull;
        AudienceScript.Instance.OnMeterEmpty -= AudienceScript_OnMeterEmpty;
    }

    private void AudienceScript_OnMeterEmpty(object sender, EventArgs e)
    {
        PlayerLoss();
    }

    private void AudienceScript_OnMeterFull(object sender, EventArgs e)
    {
        PlayerWin();
    }

    private void EnemyDeckManagerScript_OnEnemyJokePlayed(object sender, EnemyDeckManagerScript.OnEnemyJokePlayedEventArgs e)
    {
        if (isEnemyTurn)
        {
            int secondsUsed = e.jokeSO.baseSecondsToTell;
            secondsLeftThisTurn -= secondsUsed;
            //jokesPlayedThisMatch.Add(e.jokeSO);

            if (secondsLeftThisTurn < 0)
            {
                StartPlayerTurn();
            }
            else
            {
                TurnUIScript.Instance.UpdateSecondsText(secondsLeftThisTurn, false);
            }

            jokesPlayed.Add(e.jokeSO);
        }
    }

    private void PlayerDeckManagerScript_OnJokePlayed(object sender, PlayerDeckManagerScript.OnJokePlayedEventArgs e)
    {
        if (isPlayerTurn)
        {
            int secondsUsed = e.jokeSO.baseSecondsToTell;
            secondsLeftThisTurn -= secondsUsed;
            //jokesPlayedThisMatch.Add(e.jokeSO);

            if (secondsLeftThisTurn < 0)
            {
                StartEnemyTurn();
            }
            else
            {
                TurnUIScript.Instance.UpdateSecondsText(secondsLeftThisTurn , true);
            }

            jokesPlayed.Add(e.jokeSO);
        }
    }

    private void Update()
    {
        if (!gameStarted && startTimer > 0)
        {
            startTimer -= Time.deltaTime;

        }
        else if (!gameStarted)
        {
            gameStarted = true;
            StartMatch();
        }
        
        
    }

    private void StartMatch()
    {
        OnMatchStart?.Invoke(this, EventArgs.Empty);
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
        isEnemyTurn = false;
        secondsLeftThisTurn = secondsPerTurn;
        OnPlayerTurnStart?.Invoke(this, EventArgs.Empty);
    }

    public void StartEnemyTurn()
    {
        isPlayerTurn = false;
        isEnemyTurn = true;
        secondsLeftThisTurn = secondsPerTurn;
        OnEnemyTurnStart?.Invoke(this, EventArgs.Empty);
    }

    public int GetSecondsLeft()
    {
        return secondsLeftThisTurn ;
    }


    private void PlayerWin()
    {
        winScreenUI.SetActive(true);
        isPlayerTurn = false;
        isEnemyTurn = false;
    }

    private void PlayerLoss()
    {
        lossScreenUI.SetActive(true);
        isPlayerTurn = false;
        isEnemyTurn = false;
    }

    public bool IsJokePlayable(JokeSOScript jokeSO)
    {
        if (jokeSO.secondsToTell > secondsLeftThisTurn && jokeSO.PlayCondition())
        {
            return false;
        }
        return true;
    }

    public void ReturnToDungeon()
    {
        SceneManager.LoadScene(DUNGEON);
    }

}
