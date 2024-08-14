using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ContagiousLaughter : JokeSOScript
{

    public override string GetDescription()
    {
        return "Give the audience 3 cases of The Giggles";
    }

    public override void OnPlay()
    {
        if (isPlayerJoke)
        {
            AudienceScript.Instance.AddStatusEffectsFromPlayer(AudienceScript.StatusEffect.TheGiggles, 3);
        }
        else
        {
            AudienceScript.Instance.AddStatusEffectsFromEnemy(AudienceScript.StatusEffect.TheGiggles, 3);
        }
    }

}