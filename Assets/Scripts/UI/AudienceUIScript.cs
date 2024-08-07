using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceUIScript : MonoBehaviour
{

    [SerializeField] private Image laughsMeter;


    private void Start()
    {
        AudienceScript.Instance.OnLaughsChanged += AudienceScript_OnLaughsChanged;
    }

    private void AudienceScript_OnLaughsChanged(object sender, System.EventArgs e)
    {
        laughsMeter.fillAmount = AudienceScript.Instance.GetLaughs();
    }
}
