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
            }
            else
            {
                return;
            }
            
        }
    }

    public void StartPlayingJoke(JokeSOScript jokeSO)
    {
        StartCoroutine(PlayJoke(jokeSO));
    }

    private IEnumerator PlayJoke(JokeSOScript jokeSO)
    {
        EnemyUIScript.Instance.PlayJokeAnimation(jokeSO);

        float delayFromAnimation = 2f;

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
        if (hand.Count > 0)
        {
            JokeSOScript randJokeSO = hand[UnityEngine.Random.Range(0, hand.Count)];
            StartPlayingJoke(randJokeSO);
        }
        else
        {
            ShuffleStartingDeck();
            Draw(startingHandSize);
            PlayRandomJoke();
        }
        
    }
}
