using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughsCalculatorScript : MonoBehaviour
{
    
    public static LaughsCalculatorScript Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

}
