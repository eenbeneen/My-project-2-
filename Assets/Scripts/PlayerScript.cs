using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript Instance { get; private set; }

    public struct PlayerProgress
    {
        public List<JokeSOScript> deck;

    }
    
    public PlayerProgress playerProgress = new PlayerProgress();

    [SerializeField] private List<JokeSOScript> deck;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        Instance = this;

        playerProgress.deck = deck;
        
    }

    public void AddJokeToDeck(JokeSOScript jokeSO)
    {
        deck.Add(jokeSO);
    }

    public List<JokeSOScript> GetDeck()
    {
        return deck;
    }

    
}
