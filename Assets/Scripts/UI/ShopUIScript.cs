using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private GameObject buyButtonTemplate;
    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private GameObject layout;

    public void RefreshShopUI(int stock, List<JokeSOScript> jokesForSale)
    {
        foreach (Transform child in layout.transform)
        {
            Destroy(child);
        }

        for (int i = 0; i < stock; i++)
        {
            JokeUIScript jokeUI = Instantiate(jokeUITemplate, layout.transform);
            jokeUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            jokeUI.SetJokeSO(jokesForSale[i]);
            jokeUI.gameObject.SetActive(true);
            jokeUI.UpdateVisual();
        }

        layout.GetComponent<LayoutScript>().UpdateLayout();
    }
}
