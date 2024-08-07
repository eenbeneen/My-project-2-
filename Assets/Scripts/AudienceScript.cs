using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceScript : MonoBehaviour
{

    

    public static AudienceScript Instance { get; private set; }

    public event EventHandler OnLaughsChanged;
    public event EventHandler OnMeterFull;
    public event EventHandler OnMeterEmpty;


    [SerializeField] private float laughsForPlayer = .5f;


    private void Awake()
    {
        Instance = this;
    }

    public void ChangeLaughsForPlayerWithJoke(JokeSOScript jokeSO, bool isPositiveChange)
    {
        int changeSign = -1;
        if (isPositiveChange)
        {
            changeSign = 1;
        }

        //Formula to calculate how much laughs player/enemy gets
        laughsForPlayer += (changeSign * ((float)jokeSO.baseLaughs / 100)) * TypeWheelScript.Instance.GetMultiplierForType(jokeSO.type);

        OnLaughsChanged?.Invoke(this, EventArgs.Empty);

        if (laughsForPlayer >= 1f)
        {
            
            OnMeterFull?.Invoke(this, EventArgs.Empty);
            
        }
        else if (laughsForPlayer <= 0f)
        {
            
            OnMeterEmpty?.Invoke(this, EventArgs.Empty);
        }

        Debug.Log(laughsForPlayer);
    }

    public float GetLaughs()
    {
        return laughsForPlayer;
    }

    


}
