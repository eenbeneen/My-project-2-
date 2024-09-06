using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIScript : MonoBehaviour
{
    [SerializeField] private Button startButtonTemplate;

    private void Start()
    {
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            Button button = Instantiate(startButtonTemplate, transform);
            button.onClick.AddListener(delegate { DungeonManagerScript.Instance.StartCurrentEvent(); });
        }
        

    }

}
