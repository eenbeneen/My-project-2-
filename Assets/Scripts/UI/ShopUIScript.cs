using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private GameObject buyButtonTemplate;
    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private LayoutScript layout;


    public void RefreshShopUI(List<JokeSOScript> jokesForSale)
    {
        
        foreach (JokeSOScript joke in jokesForSale)
        {
            JokeUIScript jokeUI = Instantiate(jokeUITemplate, layout.transform);
            jokeUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            jokeUI.SetJokeSO(joke);
            jokeUI.UpdateVisual();
            jokeUI.gameObject.SetActive(true);
        }

        layout.UpdateLayout();
    }

    public void ExitShopUI()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in layout.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }



}
