using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class JokeUIAnimatorScript : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject jokeAnimated;

    private JokeUIScript jokeUIScript;
    
    private bool jokeBeingPlayed = false;
    private bool enableActiveAnimations = true; //Active animations are animations that make the object feel active and interactable

    private void Awake()
    {
        jokeUIScript = jokeAnimated.GetComponent<JokeUIScript>();
    }

    private void Start()
    {
        JokeUIScript.OnJokeSelected += JokeUIScript_OnJokeSelected;
        JokeUIScript.OnJokeUnselected += JokeUIScript_OnJokeUnselected;
    }

    private void JokeUIScript_OnJokeUnselected(object sender, EventArgs e)
    {
        
        if (sender as JokeUIScript == jokeUIScript && !jokeBeingPlayed && enableActiveAnimations)
        {
            LeanTween.scale(jokeAnimated, new Vector3(1f, 1f, 1f), 0.2f).setEase(LeanTweenType.easeOutQuad);
        }
    }


    private void JokeUIScript_OnJokeSelected(object sender, JokeUIScript.OnJokeSelectedEventArgs e)
    {
        if (sender as JokeUIScript == jokeUIScript && !jokeBeingPlayed && enableActiveAnimations)
        {
            LeanTween.scale(jokeAnimated, new Vector3(1.5f, 1.5f, 1.5f), 0.2f).setEase(LeanTweenType.easeOutQuad);
        }
    }

    //public void OnPointerEnter(PointerEventData pointerEventData)
    //{
    //    if (!jokeBeingPlayed && enableActiveAnimations)
    //    {
    //        LeanTween.scale(jokeAnimated, new Vector3(1.5f, 1.5f, 1.5f), 0.2f).setEase(LeanTweenType.easeOutQuad);
    //    }
        
    //}

    //public void OnPointerExit(PointerEventData pointerEventData)
    //{
    //    if (!jokeBeingPlayed && enableActiveAnimations)
    //    {
    //        LeanTween.scale(jokeAnimated, new Vector3(1f, 1f, 1f), 0.2f).setEase(LeanTweenType.easeOutQuad);
    //    }
    //}



    public void PlayJokePlayedAnimation()
    {
        jokeBeingPlayed = true;
        LeanTween.scale(jokeAnimated, new Vector3(3.5f, 3.5f, 3.5f), 1.5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalY(jokeAnimated, 640f, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalY(jokeAnimated, 1500f, 0.5f).setDelay(2f).setEase(LeanTweenType.easeInBack).setOnComplete(DestroyJokeAnimated);
        LeanTween.scale(jokeAnimated, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutQuad);
    }

    private void DestroyJokeAnimated()
    {
        Destroy(jokeAnimated);
    }

    public void AnimateMoveTo(Vector3 position)
    {

        LeanTween.moveLocal(jokeAnimated, position, 0.5f).setEase(LeanTweenType.easeOutQuad);
    }

    public void ActiveAnimationsEnabled(bool isEnabled)
    {
        enableActiveAnimations = isEnabled;
    }

}
   