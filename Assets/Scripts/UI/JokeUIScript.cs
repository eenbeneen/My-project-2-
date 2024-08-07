using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JokeUIScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private const string JOKE_PLAYED = "JokePlayed";
    private const string JOKE_SELECTED = "JokeSelected";


    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI funninessScoreText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Transform playedJokeUI;

    private JokeSOScript jokeSO;
    private Animator animator;
    private Button buttonComponent;
    private bool buttonActive = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        //If this object's parent is the PlayedJokeUI object, play the animation to play the joke
        if (transform.parent == playedJokeUI )
        {

            buttonActive = false;
            animator.SetTrigger(JOKE_PLAYED);
            
        }

        
    }

    private void Start()
    {
        buttonComponent = transform.GetComponent<Button>();
        buttonComponent.onClick.AddListener(delegate { PlayerDeckManagerScript.Instance.StartPlayingJoke(jokeSO);  PlayJokeUI(); });

        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;

        
    }

    private void GameManagerScript_OnEnemyTurnStart(object sender, System.EventArgs e)
    {
        buttonActive = false;
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, System.EventArgs e)
    {
        buttonActive = true;
    }

    private void Update()
    {
        if (buttonActive)
        {
            buttonComponent.enabled = true;
        }
        else
        {
            buttonComponent.enabled = false;
        }
    }


    public void SetJokeSO(JokeSOScript jokeSO)
    {
        this.jokeSO = jokeSO;
    }

    public void UpdateVisual()
    {
        nameText.text = jokeSO.name;
        typeText.text = jokeSO.type.ToString() + " Joke";
        funninessScoreText.text = jokeSO.baseLaughs.ToString();
        descriptionText.text = jokeSO.description;
    }


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (buttonActive)
        {
            animator.SetBool(JOKE_SELECTED, true);
        }
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        animator.SetBool(JOKE_SELECTED, false);
    }


    public void PlayJokeUI()
    {
        HandUIScript.Instance.jokeUIList.Remove(this);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Instantiate(this, playedJokeUI);
        DestroySelf();
    }

    public void DestroySelf() 
    {
        
        Destroy(gameObject); 
    }


    public void SetButtonActive(bool value)
    {
        buttonActive = value;
    }

}
