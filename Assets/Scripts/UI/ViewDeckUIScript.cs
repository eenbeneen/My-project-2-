using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewDeckUIScript : MonoBehaviour
{
    [SerializeField] private Transform layoutTransform;
    [SerializeField] private JokeUIScript jokeUITemplate;

    public void Start()
    {
        GetComponentInChildren<GridLayoutGroup>().enabled = true;

        foreach (JokeSOScript jokeSO in PlayerScript.Instance.playerProgress.deck)
        {
            JokeUIScript jokeUI = Instantiate(jokeUITemplate, layoutTransform);
            jokeUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            jokeUI.SetJokeSO(jokeSO);
            jokeUI.gameObject.SetActive(true);
            jokeUI.UpdateVisual();


        }
        

    }


    public void Show()
    {
        
        gameObject.SetActive(true);
        GetComponentInChildren<GridLayoutGroup>().enabled = false;
    }

    


}
