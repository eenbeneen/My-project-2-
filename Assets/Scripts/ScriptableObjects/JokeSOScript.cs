using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class JokeSOScript : ScriptableObject
{
    public enum JokeType
    {
        Normal,
        Physical,
        Educated,
        Ironic,
        Rude,
        Controversial,
        Bad,
        Shocking,
        Wholesome,
    }

    public string description;
    public int baseLaughs;
    public JokeType type;
    public int moodChange;
    public int secondsToTell;
    public int secondsToTellVariance;

}

