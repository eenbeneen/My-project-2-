using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JokeUIScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public static event EventHandler<OnJokeSelectedEventArgs> OnJokeSelected;
    public static event EventHandler OnJokeUnselected;
    public class OnJokeSelectedEventArgs : EventArgs
    {
        public JokeSOScript jokeSO;
    }

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI funninessScoreText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Transform playedJokeUI;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private JokeUIAnimatorScript jokeUIAnimator;

    private JokeSOScript jokeSO;
    
    
    private bool buttonActive = true;

    

    private void Awake()
    {

        //If this object's parent is the PlayedJokeUI object, play the animation to play the joke
        if (transform.parent == playedJokeUI )
        {

            SetButtonActive(false);
            jokeUIAnimator.PlayJokePlayedAnimation();
            
        }
        else
        {
            SetButtonActive(true);
        }

        
    }

    private void Start()
    {
        
        buttonComponent.onClick.AddListener(delegate { PlayerDeckManagerScript.Instance.StartPlayingJoke(jokeSO);  PlayJokeUI(); });

        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;

        
    }

    private void GameManagerScript_OnEnemyTurnStart(object sender, System.EventArgs e)
    {
        SetButtonActive(false);
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, System.EventArgs e)
    {
        SetButtonActive(true);
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


    


    public void PlayJokeUI()
    {
        HandUIScript.Instance.jokeUIList.Remove(this);
        Instantiate(this, playedJokeUI);
        DestroySelf();
    }

    public void DestroySelf() 
    {
        HandUIScript.Instance.RemoveFromHand(this);
        Destroy(gameObject); 
    }


    public void SetButtonActive(bool value)
    {
        buttonActive = value;
        jokeUIAnimator.ActiveAnimationsEnabled(value);
    }

    public void MoveTo(Vector3 position)
    {
        jokeUIAnimator.AnimateMoveTo(position);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.SetAsLastSibling();
        if (buttonActive)
        {
            OnJokeSelected?.Invoke(this, new OnJokeSelectedEventArgs
            {
                jokeSO = jokeSO
            });
        }
        
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        OnJokeUnselected?.Invoke(this, EventArgs.Empty);
    }

}
