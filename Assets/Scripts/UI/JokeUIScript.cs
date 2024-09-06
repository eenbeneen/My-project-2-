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
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI timeToTellText;
    [SerializeField] private Transform playedJokeUI;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private JokeUIAnimatorScript jokeUIAnimator;
    [SerializeField] private Image deselectedImage;
    [SerializeField] private Image background;
    [SerializeField] private List<Color> typeColorList;
    

    private JokeSOScript jokeSO;
    private bool jokeBeingPlayed;
    
    private bool isButtonActive = true;

    private bool isSelected;

    

    private void Awake()
    {
        

        //If this object's parent is the PlayedJokeUI object, play the animation to play the joke
        if (transform.parent == playedJokeUI )
        {
            jokeBeingPlayed = true;
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

        if(GameManagerScript.Instance != null)
        {
            GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
            GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;
        }
        

    }

    private void OnDisable()
    {
        if (GameManagerScript.Instance != null)
        {
            GameManagerScript.Instance.OnPlayerTurnStart -= GameManagerScript_OnPlayerTurnStart;
            GameManagerScript.Instance.OnEnemyTurnStart -= GameManagerScript_OnEnemyTurnStart;
        }
        
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

        UpdateVisual();

        if (isButtonActive)
        {
            if (jokeSO != null && GameManagerScript.Instance != null)
            {
                SetButtonActive(GameManagerScript.Instance.IsJokePlayable(jokeSO));
            }
            buttonComponent.interactable = true;
        }
        else
        {
            buttonComponent.interactable = false;
        }
        
        
    }


    public void SetJokeSO(JokeSOScript jokeSO)
    {
        this.jokeSO = jokeSO;
        
        
    }

    public void UpdateVisual()
    {
        if (jokeSO != null)
        {
            nameText.text = jokeSO.name;
            typeText.text = jokeSO.type.ToString() + " Joke";
            descriptionText.text = jokeSO.GetDescription();
            timeToTellText.text = jokeSO.secondsToTell.ToString() + "s";

            int colorIndex;
            switch(jokeSO.type)
            {
                default:
                case JokeSOScript.JokeType.Normal: 
                    colorIndex = 0; 
                    break;
                case JokeSOScript.JokeType.Dumb: 
                    colorIndex = 1; 
                    break;
                case JokeSOScript.JokeType.Nice: 
                    colorIndex = 2; 
                    break;
                case JokeSOScript.JokeType.Smart: 
                    colorIndex = 3; 
                    break;
                case JokeSOScript.JokeType.Rude: 
                    colorIndex = 4; 
                    break;
            }
            background.color = typeColorList[colorIndex];
        }
    }




    public void PlayJokeUI()
    {
        UpdateVisual();
        HandUIScript.Instance.jokeUIList.Remove(this);
        OnJokeUnselected?.Invoke(this, EventArgs.Empty);
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
        isButtonActive = value;

        if (!value && isSelected)
        {
            isSelected = false;
            OnJokeUnselected?.Invoke(this, EventArgs.Empty);
        }
        
        jokeUIAnimator.ActiveAnimationsEnabled(value);

        if (deselectedImage != null)
        {
            if (!jokeBeingPlayed)
            {

                deselectedImage.gameObject.SetActive(!value);
            }
            else
            {
                deselectedImage.gameObject.SetActive(false);
            }
        }


    }

    public void MoveTo(Vector3 position)
    {
        jokeUIAnimator.AnimateMoveTo(position);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {

        transform.SetAsLastSibling();
        
        if (isButtonActive)
        {
            isSelected = true;
            OnJokeSelected?.Invoke(this, new OnJokeSelectedEventArgs
            {
                jokeSO = jokeSO
            });
            
        }
        
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isSelected = false;
        OnJokeUnselected?.Invoke(this, EventArgs.Empty);
        
    }

}
