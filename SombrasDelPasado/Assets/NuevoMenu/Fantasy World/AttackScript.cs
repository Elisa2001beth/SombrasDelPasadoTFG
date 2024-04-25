using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool resistenceAttack;

    [SerializeField]
    private float resistenceCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private CharacterStats attackerStats;
    private CharacterStats targetStats;
    private float damage = 0.0f;
    
    public void Attack(GameObject attacker, GameObject victim)
    {
        attackerStats = attacker.GetComponent<CharacterStats>();
        targetStats = victim.GetComponent<CharacterStats>();
    
        if (attackerStats != null && targetStats != null)
        {
            if (attackerStats.resistence >= resistenceCost)
            {
                float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

                damage = multiplier * attackerStats.melee;
                if (resistenceAttack)
                {
                    damage = multiplier * attackerStats.resistenceRange;
                }

                float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
                damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
                owner.GetComponent<Animator>().Play(animationName);
                targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
                attackerStats.updateResistenceFill(resistenceCost);
            }
            else
            {
                Invoke("SkipTurnContinueGame", 2);
            }
        }
        else
        {
            Debug.LogError("CharacterStats component not found on attacker or victim");
        }
    }
    
    void SkipTurnContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<BattleGameController>().NextTurn();
    }
}
