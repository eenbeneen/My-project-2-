using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIScript : MonoBehaviour
{
    [SerializeField] private Button startButtonTemplate;
    [SerializeField] private Button shopButtonTemplate;
    [SerializeField] private GameObject shopUI;


    [SerializeField] private Button buyButtonTemplate;
    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private LayoutScript forSaleLayout;
    [SerializeField] private LayoutScript buyButtonLayout;
    [SerializeField] private ShopManagerScript shopManager;


    private void DungeonManagerScript_OnEnterShop(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }


    private void Start()
    {
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            Button startButton = Instantiate(startButtonTemplate, transform);
            startButton.onClick.AddListener(delegate { DungeonManagerScript.Instance.StartCurrentEvent(); });
        }

        Button shopButton = Instantiate(shopButtonTemplate, transform);
        shopButton.onClick.AddListener(delegate { EnterShopUI(); });
        

    }

    public void InitShopUI(List<JokeSOScript> jokesForSale)
    {

        if (forSaleLayout.transform.childCount > 0)
        {
            foreach (Transform child in forSaleLayout.transform)
            {
                Destroy(child.gameObject);
            }
        }

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

    }



    public void ExitShopUI()
    {
        gameObject.SetActive(true);
        shopUI.SetActive(false);
    }

    public void EnterShopUI()
    {
        gameObject.SetActive(false);
        shopUI.SetActive(true);
    }

}
