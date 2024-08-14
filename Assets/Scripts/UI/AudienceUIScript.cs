using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudienceUIScript : MonoBehaviour
{

    [SerializeField] private Image laughsMeter;
    [SerializeField] private Image laughsMeterPreview;

    [SerializeField] private TextMeshProUGUI statusEffectsFromPlayerText;
    [SerializeField] private TextMeshProUGUI statusEffectsFromEnemyText;


    private void Start()
    {
        AudienceScript.Instance.OnLaughsChanged += AudienceScript_OnLaughsChanged;
        JokeUIScript.OnJokeSelected += JokeUIScript_OnJokeSelected;
        JokeUIScript.OnJokeUnselected += JokeUIScript_OnJokeUnselected;
        AudienceScript.Instance.OnStatusEffectsFromPlayerChanged += AudienceScript_OnStatusEffectsFromPlayerChanged;
        AudienceScript.Instance.OnStatusEffectsFromEnemyChanged += AudienceScript_OnStatusEffectsFromEnemyChanged;
    }

    private void AudienceScript_OnStatusEffectsFromEnemyChanged(object sender, EventArgs e)
    {
        statusEffectsFromEnemyText.text = GetNewStatusEffectsText(AudienceScript.Instance.statusEffectsFromEnemy);
    }

    private void AudienceScript_OnStatusEffectsFromPlayerChanged(object sender, EventArgs e)
    {
        statusEffectsFromPlayerText.text = GetNewStatusEffectsText(AudienceScript.Instance.statusEffectsFromPlayer);
    }

    private void JokeUIScript_OnJokeUnselected(object sender, System.EventArgs e)
    {
        laughsMeterPreview.gameObject.SetActive(false);
    }

    private void JokeUIScript_OnJokeSelected(object sender, JokeUIScript.OnJokeSelectedEventArgs e)
    {
        
        int laughsToAdd = (int)(e.jokeSO.laughs * TypeWheelScript.Instance.GetMultiplierForType(e.jokeSO.baseType));
        laughsMeterPreview.fillAmount = (AudienceScript.Instance.GetLaughs() + laughsToAdd) / 100f;
        laughsMeterPreview.gameObject.SetActive(true);
    }

    private void AudienceScript_OnLaughsChanged(object sender, System.EventArgs e)
    {
        laughsMeter.fillAmount = AudienceScript.Instance.GetLaughs() / 100f;
    }

    private string GetNewStatusEffectsText(List<AudienceScript.StatusEffect> statusEffects)
    {
        string newText = "";

        foreach (AudienceScript.StatusEffect statusEffect in Enum.GetValues(typeof(AudienceScript.StatusEffect)) )
        {
            int occurances = FindStatusEffectOccurances(statusEffects, statusEffect);
            if ( occurances > 0 )
            {
                newText += statusEffect.ToString();

                if (occurances > 1 )
                {
                    newText += "x" + occurances;
                }

                newText += "\n";
            }
        }

        return newText;

    }

    private int FindStatusEffectOccurances(List<AudienceScript.StatusEffect> statusEffects, AudienceScript.StatusEffect passedStatusEffect)
    {
        int count = 0;
        foreach (AudienceScript.StatusEffect statusEffect in statusEffects)
        {
            if (statusEffect == passedStatusEffect)
            {
                count++;
            }
        }
        Debug.Log(count);
        return count;
    }
}
