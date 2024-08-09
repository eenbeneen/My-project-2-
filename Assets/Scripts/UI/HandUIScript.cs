using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIScript : MonoBehaviour
{

    public static HandUIScript Instance { get; private set; }

    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private Transform deckTransform;

    public List<JokeUIScript> jokeUIList;

    private RectTransform rectTransform;


    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }


    void Start()
    {
        jokeUIList = new List<JokeUIScript>();
        PlayerDeckManagerScript.Instance.OnJokeDrawn += PlayerDeckManagerScript_OnJokeDrawn;
        
        
    }

    private void PlayerDeckManagerScript_OnJokeDrawn(object sender, PlayerDeckManagerScript.OnJokeDrawnEventArgs e)
    {
        JokeUIScript jokeUI = Instantiate(jokeUITemplate, transform);
        jokeUI.SetJokeSO(e.jokeSO, true);
        jokeUI.transform.position = deckTransform.position;
        jokeUI.gameObject.SetActive(true);
        jokeUI.UpdateVisual();
        jokeUIList.Add(jokeUI);

        UpdateVisual();

    }

    public void SetAllButtonsActive(bool value)
    {
        foreach (JokeUIScript jokeUI in jokeUIList)
        {
            jokeUI.SetButtonActive(value);
        }
    }

    private void UpdateVisual()
    {
        int jokesInHand = jokeUIList.Count;
        float height = rectTransform.sizeDelta.y;
        float spacing = height / jokesInHand;

        for (int i = 0; i < jokesInHand; i++)
        {
            float startPoint = -(height / 2) + (i * spacing);
            float newYPos = (2 * startPoint + spacing) / 2;
            jokeUIList[i].MoveTo(new Vector3(0f, newYPos, 0f));

        }

        


    }

    public void RemoveFromHand(JokeUIScript jokeUI)
    {
        jokeUIList.Remove(jokeUI);
        UpdateVisual();
    }



    

}
