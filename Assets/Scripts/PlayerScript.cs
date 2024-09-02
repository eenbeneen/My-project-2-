using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript Instance { get; private set; }

    [SerializeField] private List<JokeSOScript> deck;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;
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
