using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ContagiousLaughter : JokeSOScript
{

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