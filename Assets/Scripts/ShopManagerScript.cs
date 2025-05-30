using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour
{

    [SerializeField] private JokeSOScript[] jokePool;
    [SerializeField] private ShopUIScript shopUI;

    public List<JokeSOScript> jokesForSale;

    [SerializeField] private int stock;

    void Start()
    {
        DungeonManagerScript.Instance.OnEnterShop += DungeonManagerScript_OnEnterShop;
    }

    private void DungeonManagerScript_OnEnterShop(object sender, System.EventArgs e)
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
        shopUI.RefreshShopUI(stock, jokesForSale);
    }

    public void SetStock(int newStock)
    {
        stock = newStock;
    }

    public int GetStock()
    {
        return stock;
    }
}
