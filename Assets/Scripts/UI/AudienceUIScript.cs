using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceUIScript : MonoBehaviour
{

    [SerializeField] private Image laughsMeter;
    [SerializeField] private Image laughsMeterPreview;


    private void Start()
    {
        AudienceScript.Instance.OnLaughsChanged += AudienceScript_OnLaughsChanged;
        JokeUIScript.OnJokeSelected += JokeUIScript_OnJokeSelected;
        JokeUIScript.OnJokeUnselected += JokeUIScript_OnJokeUnselected;
    }

    private void JokeUIScript_OnJokeUnselected(object sender, System.EventArgs e)
    {
        laughsMeterPreview.gameObject.SetActive(false);
    }

    private void JokeUIScript_OnJokeSelected(object sender, JokeUIScript.OnJokeSelectedEventArgs e)
    {
        
        float laughsToAdd = (float)e.jokeSO.baseLaughs / 100 * TypeWheelScript.Instance.GetMultiplierForType(e.jokeSO.type);
        laughsMeterPreview.fillAmount = AudienceScript.Instance.GetLaughs() + laughsToAdd;
        laughsMeterPreview.gameObject.SetActive(true);
    }

    private void AudienceScript_OnLaughsChanged(object sender, System.EventArgs e)
    {
        laughsMeter.fillAmount = AudienceScript.Instance.GetLaughs();
    }
}
