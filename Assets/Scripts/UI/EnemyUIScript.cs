using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIScript : MonoBehaviour
{

    public static EnemyUIScript Instance { get; private set; }


    [SerializeField] private Transform playedJokeUI;
    [SerializeField] private JokeUIScript jokeUITemplate;

    private void Awake()
    {
        Instance = this;
    }


    public void PlayJokeAnimation(JokeSOScript jokeSO)
    {
        JokeUIScript jokeUI = Instantiate(jokeUITemplate, playedJokeUI);
        jokeUI.gameObject.SetActive(true);
        jokeUI.SetJokeSO(jokeSO);
        jokeUI.UpdateVisual();
    }
}
