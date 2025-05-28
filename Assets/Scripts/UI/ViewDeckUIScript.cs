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
        
        

    }


    public void Show()
    {

        foreach (JokeSOScript jokeSO in PlayerScript.Instance.playerProgress.deck)
        {
            JokeUIScript jokeUI = Instantiate(jokeUITemplate, layoutTransform);
            jokeUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            jokeUI.SetJokeSO(jokeSO);
            jokeUI.gameObject.SetActive(true);
            jokeUI.UpdateVisual();


        }
        GetComponentInChildren<DeckLayoutScript>().UpdateViewDeckLayout();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (Transform child in layoutTransform)
        {
            Destroy(child.gameObject);
        }

        
    }




}
