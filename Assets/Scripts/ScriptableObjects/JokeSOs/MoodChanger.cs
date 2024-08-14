using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MoodChanger : JokeSOScript
{


    public override string GetDescription()
    {
        return "+10 Wholesome Mood";
    }

    public override void OnPlay()
    {
        moodChange = 10;
    }

}
