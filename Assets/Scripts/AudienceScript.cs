using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudienceScript;

public class AudienceScript : MonoBehaviour
{

    public enum StatusEffect
    {
        TheGiggles,
    }


    public static AudienceScript Instance { get; private set; }

    public event EventHandler OnLaughsChanged;
    public event EventHandler OnMeterFull;
    public event EventHandler OnMeterEmpty;
    public event EventHandler OnStatusEffectsFromEnemyChanged;
    public event EventHandler OnStatusEffectsFromPlayerChanged;


    [SerializeField] private int laughsForPlayer = 50;

    public List<StatusEffect> statusEffectsFromPlayer;
    public List<StatusEffect> statusEffectsFromEnemy;


    private void Awake()
    {
        Instance = this;
        statusEffectsFromPlayer = new List<StatusEffect>();
        statusEffectsFromEnemy = new List<StatusEffect>();
    }

    private void Start()
    {
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;
        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
    }

    private void OnDisable()
    {
        GameManagerScript.Instance.OnEnemyTurnStart -= GameManagerScript_OnEnemyTurnStart;
        GameManagerScript.Instance.OnPlayerTurnStart -= GameManagerScript_OnPlayerTurnStart;
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, EventArgs e)
    {
        ActivateStatusEffectsFromPlayer();
    }

    private void GameManagerScript_OnEnemyTurnStart(object sender, EventArgs e)
    {
        ActivateStatusEffectsFromEnemy();
    }

    public void ChangeLaughsForPlayerWithJoke(JokeSOScript jokeSO, bool isPositiveChange)
    {
        int changeSign = -1;
        if (isPositiveChange)
        {
            changeSign = 1;
        }

        //Formula to calculate how much laughs player/enemy gets
        laughsForPlayer += (int)(changeSign * (float)jokeSO.laughs * TypeWheelScript.Instance.GetMultiplierForType(jokeSO.baseType));

        OnLaughsChanged?.Invoke(this, EventArgs.Empty);

        CheckMeterLimit();

    }

    public void SubtractLaughs(int laughs)
    {
        AddLaughs(-laughs);
    }

    public void AddLaughs(int laughs)
    {
        laughsForPlayer += laughs;

        Debug.Log("LAUGHS FOR PLAYER: " + laughsForPlayer);

        OnLaughsChanged?.Invoke(this, EventArgs.Empty);

        CheckMeterLimit();
    }

    public float GetLaughs()
    {
        return laughsForPlayer;
    }

    public void CheckMeterLimit()
    {
        if (laughsForPlayer >= 100)
        {

            OnMeterFull?.Invoke(this, EventArgs.Empty);

        }
        else if (laughsForPlayer <= 0)
        {

            OnMeterEmpty?.Invoke(this, EventArgs.Empty);
        }
    }
    

    private void ActivateStatusEffectsFromPlayer()
    {
        List<StatusEffect> effectsTriggered = new List<StatusEffect>();

        if (statusEffectsFromPlayer.Count == 0)
        {
            return;
        }

        for (int i = 0; i < statusEffectsFromPlayer.Count; i++)
        {
            StatusEffect effect = statusEffectsFromPlayer[i];

            switch (effect)
            {
                case StatusEffect.TheGiggles:
                    laughsForPlayer += 1;
                    if (!effectsTriggered.Contains(effect))
                    {
                        //the giggles has not been triggered before
                        statusEffectsFromPlayer.Remove(effect);
                        effectsTriggered.Add(effect);
                    }
                    break;
            }

        }

        OnStatusEffectsFromPlayerChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddStatusEffectsFromPlayer(StatusEffect statusEffect, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            statusEffectsFromPlayer.Add(statusEffect);
        }
        OnStatusEffectsFromPlayerChanged?.Invoke(this, EventArgs.Empty);
    }



    private void ActivateStatusEffectsFromEnemy()
    {
        List<StatusEffect> effectsTriggered = new List<StatusEffect>();

        if (statusEffectsFromEnemy.Count == 0)
        {
            return;
        }

        for (int i = 0; i < statusEffectsFromEnemy.Count; i++)
        {
            StatusEffect effect = statusEffectsFromEnemy[i];

            switch (effect)
            {
                case StatusEffect.TheGiggles:
                    laughsForPlayer += 1;
                    if (!effectsTriggered.Contains(effect))
                    {
                        //the giggles has not been triggered before
                        statusEffectsFromEnemy.Remove(effect);
                    }
                    break;
            }

        }
        OnStatusEffectsFromEnemyChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddStatusEffectsFromEnemy(StatusEffect statusEffect, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            statusEffectsFromEnemy.Add(statusEffect);
        }

        OnStatusEffectsFromEnemyChanged?.Invoke(this, EventArgs.Empty);
    }


}
