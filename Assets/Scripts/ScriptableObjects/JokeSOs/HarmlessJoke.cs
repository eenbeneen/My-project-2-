using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HarmlessJoke : JokeSOScript
{

    public override string GetDescription()
    {
        return "+" + laughs * TypeWheelScript.Instance.GetMultiplierForType(type) + " Laughs, +" + moodChange + " Mood";
    }



}
