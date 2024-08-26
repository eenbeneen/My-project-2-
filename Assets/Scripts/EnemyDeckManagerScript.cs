using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeckManagerScript : MonoBehaviour
{
    


    public static EnemyDeckManagerScript Instance { get; private set; }

    public event EventHandler<OnEnemyJokePlayedEventArgs> OnEnemyJokePlayed;
    public class OnEnemyJokePlayedEventArgs : EventArgs
    {
        public JokeSOScript jokeSO;
    }


    [SerializeField] private List<JokeSOScript> startingDeck;
    [SerializeField] private int startingHandSize;
    [SerializeField] private EnemyUIScript enemyUI;
    [SerializeField] private float enemyJokePlayingTimerMax = 3f;
    private float enemyJokePlayingTimer;

    private bool jokePlayedThisTurn;

    private List<JokeSOScript> hand = new List<JokeSOScript>();
    private List<JokeSOScript> deck = new List<JokeSOScript>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManagerScript.Instance.OnMatchStart += GameManagerScript_OnMatchStart;
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;

        enemyJokePlayingTimer = enemyJokePlayingTimerMax;

        foreach (JokeSOScript jokeSO in startingDeck)
        {
            jokeSO.InitializeVariables(false);
        }
    }

    private void OnDisable()
    {
        GameManagerScript.Instance.OnMatchStart -= GameManagerScript_OnMatchStart;
        GameManagerScript.Instance.OnEnemyTurnStart -= GameManagerScript_OnEnemyTurnStart;
    }
    private void GameManagerScript_OnEnemyTurnStart(object sender, EventArgs e)
    {
        Draw(startingHandSize - hand.Count);
    }

    private void Update()
    {
        if (GameManagerScript.Instance.isEnemyTurn)
        {
            enemyJokePlayingTimer -= Time.deltaTime;
            if (enemyJokePlayingTimer < 0)
            {
                PlayRandomJoke();
                enemyJokePlayingTimer = enemyJokePlayingTimerMax;
            }
        }
        else
        {
            enemyJokePlayingTimer = enemyJokePlayingTimerMax / 2;
        }
    }

    private void GameManagerScript_OnMatchStart(object sender, System.EventArgs e)
    {
        ShuffleStartingDeck();
        Draw(startingHandSize);
        jokePlayedThisTurn = false;
    }

    public void ShuffleStartingDeck()
    {
        List<JokeSOScript> jokesToShuffle = new List<JokeSOScript>();
        jokesToShuffle.AddRange(startingDeck);
        for (int i = 0; i < startingDeck.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, jokesToShuffle.Count);
            deck.Add(jokesToShuffle[rand]);
            jokesToShuffle.RemoveAt(rand);
        }
    }

    public void Draw(int numJokes)
    {
        for (int i = 0; i < numJokes; i++)
        {
            if (deck.Count > 0)
            {
                JokeSOScript jokeSODrawn = deck[0];
                deck.RemoveAt(0);
                hand.Add(jokeSODrawn);

                jokeSODrawn.InitializeVariables(false);
            }
            else
            {
                return;
            }
            
        }
    }

    public void StartPlayingJoke(JokeSOScript jokeSO)
    {
        jokeSO.InitializeVariables(false);
        jokeSO.OnPlay();
        StartCoroutine(PlayJoke(jokeSO));
    }

    private IEnumerator PlayJoke(JokeSOScript jokeSO)
    {
        Debug.Log(jokeSO.laughs);

        EnemyUIScript.Instance.PlayJokeAnimation(jokeSO);

        float delayFromAnimation = 2.5f;

        yield return new WaitForSeconds(delayFromAnimation);
        
        AudienceScript.Instance.ChangeLaughsForPlayerWithJoke(jokeSO, false);

        OnEnemyJokePlayed?.Invoke(this, new OnEnemyJokePlayedEventArgs
        {
            jokeSO = jokeSO
        });

        hand.Remove(jokeSO);
        
    }

    public void PlayRandomJoke()
    {
        List<JokeSOScript> playableJokeSOList = new List<JokeSOScript>();
        foreach (JokeSOScript jokeSO in hand)
        {
            if (GameManagerScript.Instance.IsJokePlayable(jokeSO))
            {
                playableJokeSOList.Add(jokeSO);
            }
        }

        if (playableJokeSOList.Count > 0)
        {
            JokeSOScript randJokeSO = playableJokeSOList[UnityEngine.Random.Range(0, playableJokeSOList.Count)];
            
            StartPlayingJoke(randJokeSO);
        }
        else
        {
            Debug.Log("Out of cards!");

            if (deck.Count == 0)
            {
                Debug.Log("Enemy deck empty! Reshuffling...");
                ShuffleStartingDeck();
            }
            
            GameManagerScript.Instance.StartPlayerTurn();
        }
        
    }
}
