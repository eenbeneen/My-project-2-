using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InsideJoke : JokeSOScript
{


    public override void OnPlay()
    {
        if (GameManagerScript.Instance.jokesPlayed.Count > 0)
        {
            JokeSOScript jokeToCopy = GameManagerScript.Instance.jokesPlayed[Random.Range(0, GameManagerScript.Instance.jokesPlayed.Count)];
            laughs = jokeToCopy.baseLaughs;
            Debug.Log("Copied laughs from " + jokeToCopy + ". New laughs: " + laughs);
        }
    }

}
