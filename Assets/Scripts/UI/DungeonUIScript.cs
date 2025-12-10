using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIScript : MonoBehaviour
{
    [SerializeField] private Button startButtonTemplate;
    [SerializeField] private Button shopButtonTemplate;


    private void DungeonManagerScript_OnEnterShop(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }


    private void Start()
    {
        DungeonManagerScript.Instance.OnEnterShop += DungeonManagerScript_OnEnterShop;
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            Button startButton = Instantiate(startButtonTemplate, transform);
            startButton.onClick.AddListener(delegate { DungeonManagerScript.Instance.StartCurrentEvent(); });
        }

        Button shopButton = Instantiate(shopButtonTemplate, transform);
        shopButton.onClick.AddListener(delegate { DungeonManagerScript.Instance.StartShop(); });
        

    }

}
