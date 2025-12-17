using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private Button buyButtonTemplate;
    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private LayoutScript forSaleLayout;
    [SerializeField] private LayoutScript buyButtonLayout;
    [SerializeField] private ShopManagerScript shopManager;


    public void RefreshShopUI(List<JokeSOScript> jokesForSale)
    {
        
        for (int i = 0; i < jokesForSale.Count; i++)
        {
            JokeSOScript joke = jokesForSale[i];

            JokeUIScript jokeUI = Instantiate(jokeUITemplate, forSaleLayout.transform);
            jokeUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            jokeUI.SetJokeSO(joke);
            jokeUI.UpdateVisual();
            jokeUI.gameObject.SetActive(true);

            Button b = Instantiate(buyButtonTemplate, buyButtonLayout.transform);
            TMP_Text label = b.GetComponentInChildren<TMP_Text>();
            b.onClick.AddListener(() =>
                {
                    shopManager.BuyJoke(joke);
                    label.text = "Bought!";
                    b.interactable = false;
                }
            );

        }

        forSaleLayout.UpdateLayout();
        buyButtonLayout.UpdateLayout();

        gameObject.SetActive(true);

    }

    public void ExitShopUI()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in forSaleLayout.transform)
            {
                Destroy(child.gameObject);
            }
        }

        gameObject.SetActive(false);
    }



}
