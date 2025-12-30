using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour
{


    [SerializeField] private JokeSOScript[] jokePool;
    [SerializeField] private DungeonUIScript dungeonUI;

    public List<JokeSOScript> jokesForSale;

    [SerializeField] private int stock;

    private void Start()
    {
        RefreshShop();
    }

    void RefreshShop()
    {
        jokesForSale.Clear();
        for (int i = 0; i < stock; i++)
        {
            jokesForSale.Insert(i, jokePool[Random.Range(0, jokePool.Length)]);
        }
        dungeonUI.InitShopUI(jokesForSale);
    }

    public void BuyJoke(JokeSOScript joke)
    {
        PlayerScript.Instance.AddJokeToDeck(joke);
    }


}
