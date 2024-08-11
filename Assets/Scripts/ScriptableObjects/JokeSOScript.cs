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
    public JokeType baseType;
    public int baseMoodChange;
    public int baseSecondsToTell;

    [HideInInspector] public int laughs;
    [HideInInspector] public JokeType type;
    [HideInInspector] public int moodChange;
    [HideInInspector] public int secondsToTell;
    [HideInInspector] public bool isPlayerJoke;

    public void InitializeVariables(bool sentByPlayer)
    {
        laughs = baseLaughs;
        type = baseType;
        moodChange = baseMoodChange;
        secondsToTell = baseSecondsToTell;
        isPlayerJoke = sentByPlayer;
    }

    public virtual bool PlayCondition()
    {
        return true;
    }

    public virtual void OnPlay()
    {
        
    }

    public virtual void OnDraw()
    {
        
    }


}


