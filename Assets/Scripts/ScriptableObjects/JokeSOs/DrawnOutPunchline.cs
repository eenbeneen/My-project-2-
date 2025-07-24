using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DrawnOutPunchline : JokeSOScript
{
    public override string GetDescription()
    {
        return "+" + laughs * (TypeWheelScript.Instance?.GetMultiplierForType(type) ?? 1) + " Laughs. +" + moodChange + " Mood.";
    }
}
