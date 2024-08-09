using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class PlayerDeckManagerScript : MonoBehaviour
{
    public static PlayerDeckManagerScript Instance { get; private set; }

    public event EventHandler<OnJokeDrawnEventArgs> OnJokeDrawn;
    
    public class OnJokeDrawnEventArgs : EventArgs
    {
        public JokeSOScript jokeSO;
    }

    public event EventHandler<OnJokePlayedEventArgs> OnJokePlayed;
    public class OnJokePlayedEventArgs : EventArgs
    {
        public JokeSOScript jokeSO;
    }


    [SerializeField] private List<JokeSOScript> startingDeck;
    [SerializeField] private GameObject handGameObject;
    [SerializeField] private int startingHandSize;

    private List<JokeSOScript> hand = new List<JokeSOScript>();
    private List<JokeSOScript> deck = new List<JokeSOScript>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManagerScript.Instance.OnMatchStart += GameManagerScript_OnMatchStart;
        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, EventArgs e)
    {
        Draw(startingHandSize - hand.Count);
    }

    private void GameManagerScript_OnMatchStart(object sender, System.EventArgs e)
    {
        ShuffleStartingDeck();
        Draw(startingHandSize);
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

                OnJokeDrawn?.Invoke(this, new OnJokeDrawnEventArgs
                {
                    jokeSO = jokeSODrawn
                });
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
        jokeSO.OnPlay();

        float delayFromAnimation = 2f;

        HandUIScript.Instance.SetAllButtonsActive(false);

        yield return new WaitForSeconds(delayFromAnimation);

        HandUIScript.Instance.SetAllButtonsActive(true);

        AudienceScript.Instance.ChangeLaughsForPlayerWithJoke(jokeSO, true);

        hand.Remove(jokeSO);

        OnJokePlayed?.Invoke(this, new OnJokePlayedEventArgs
        {
            jokeSO = jokeSO
        });
    }
}
