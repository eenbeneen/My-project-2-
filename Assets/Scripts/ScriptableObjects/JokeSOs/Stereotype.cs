using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Stereotype : JokeSOScript
{
    public override string GetDescription()
    {
        return "+" + laughs * TypeWheelScript.Instance?.GetMultiplierForType(type) ?? 1 + " Laughs";
    }

}
