using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectsScript;

public class EffectsScript : MonoBehaviour
{
    
    public static EffectsScript Instance { get; private set; }

    public enum BattlerEffects
    {
        HoldingLaugh,
    }

    public enum AudienceEffects
    {
        Giggles,

    }

    private List<BattlerEffects> effectsOnPlayer = new List<BattlerEffects>();
    private List<BattlerEffects> effectsOnEnemy = new List<BattlerEffects>();
    private List<AudienceEffects> effectsOnAudienceByPlayer = new List<AudienceEffects>();
    private List<AudienceEffects> effectsOnAudienceByEnemy = new List<AudienceEffects>();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManagerScript.Instance.OnPlayerTurnStart += GameManagerScript_OnPlayerTurnStart;
        GameManagerScript.Instance.OnEnemyTurnStart += GameManagerScript_OnEnemyTurnStart;
    }

    private void GameManagerScript_OnEnemyTurnStart(object sender, System.EventArgs e)
    {
        List<BattlerEffects> battlerEffectsCleared = new List<BattlerEffects>();
        foreach (BattlerEffects battlerEffect in effectsOnEnemy)
        {
            if(!battlerEffectsCleared.Contains(battlerEffect))
            {
                effectsOnEnemy.Remove(battlerEffect);
                battlerEffectsCleared.Add(battlerEffect);
            }
        }

        List<AudienceEffects> audienceEffectsCleared = new List<AudienceEffects>();
        foreach (AudienceEffects audienceEffect in effectsOnAudienceByEnemy)
        {
            if(!audienceEffectsCleared.Contains(audienceEffect))
            {
                effectsOnAudienceByEnemy.Remove(audienceEffect);
                audienceEffectsCleared.Add(audienceEffect);
            }
        }
    }

    private void GameManagerScript_OnPlayerTurnStart(object sender, System.EventArgs e)
    {
        List<BattlerEffects> battlerEffectsCleared = new List<BattlerEffects>();
        foreach (BattlerEffects battlerEffect in effectsOnPlayer)
        {
            if(!battlerEffectsCleared.Contains(battlerEffect))
            {
                effectsOnPlayer.Remove(battlerEffect);
                battlerEffectsCleared.Add(battlerEffect);
            }
        }

        List<AudienceEffects> audienceEffectsCleared = new List<AudienceEffects>();
        foreach (AudienceEffects audienceEffect in effectsOnAudienceByPlayer)
        {
            if(!audienceEffectsCleared.Contains(audienceEffect))
            {
                effectsOnAudienceByPlayer.Remove(audienceEffect);
                audienceEffectsCleared.Add(audienceEffect);
            }
        }
        


    }



    
}
