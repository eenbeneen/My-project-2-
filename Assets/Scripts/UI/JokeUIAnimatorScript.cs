using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JokeUIAnimatorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject jokeAnimated;

    private bool jokeBeingPlayed = false;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!jokeBeingPlayed)
        {
            LeanTween.scale(jokeAnimated, new Vector3(1.5f, 1.5f, 1.5f), 0.2f).setEase(LeanTweenType.easeOutQuad);
        }
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!jokeBeingPlayed)
        {
            LeanTween.scale(jokeAnimated, new Vector3(1f, 1f, 1f), 0.2f).setEase(LeanTweenType.easeOutQuad);
        }
    }


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
}
   