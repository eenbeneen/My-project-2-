using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NormalJoke : JokeSOScript
{


    public override string GetDescription()
    {
        return "+10 Laughs";
    }


    public override void OnPlay()
    {
        if (isPlayerJoke)
        {
            AudienceScript.Instance.AddLaughs(10);
        }
        else
        {
            AudienceScript.Instance.SubtractLaughs(10);
        }
    }
}
