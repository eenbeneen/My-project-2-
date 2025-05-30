using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokeViewerUIScript : MonoBehaviour
{
    public static JokeViewerUIScript Instance { get; private set; }
    [SerializeField] private JokeUIScript jokeUITemplate;
    [SerializeField] private GameObject background;

    private JokeUIScript shownJokeUI;

    private void Awake()
    {
        Instance = this;
    }

    public void ViewJoke(JokeSOScript jokeSO)
    {
        background.SetActive(true);
        shownJokeUI = Instantiate(jokeUITemplate, transform);
        shownJokeUI.SetJokeSO(jokeSO);
        shownJokeUI.SetButtonActive(false, false);
        shownJokeUI.gameObject.transform.localScale = new Vector3(4, 4, 4);
    }

    public void Hide()
    {
        background.SetActive(false);
        Destroy(shownJokeUI.gameObject); 
        shownJokeUI = null;
    }
}
