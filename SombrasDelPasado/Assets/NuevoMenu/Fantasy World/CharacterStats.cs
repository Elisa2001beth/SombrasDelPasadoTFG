using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject resistenceFill;

    [Header("Stats")]
    public float health;
    public float resistence;
    public float melee;
    public float resistenceRange;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startResistence;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Resize health and resistence bar
    private Transform healthTransform;
    private Transform resistenceTransform;

    private Vector2 healthScale;
    private Vector2 resistenceScale;

    private float xNewHealthScale;
    private float xNewresistenceScale;

    private GameObject GameControllerObj;

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        resistenceTransform = resistenceFill.GetComponent<RectTransform>();
        resistenceScale = resistenceFill.transform.localScale;

        startHealth = health;
        startResistence = resistence;

        GameControllerObj = GameObject.Find("GameControllerObject");
    }

    public void ReceiveDamage(float damage)
    {
        health = health - damage;
        Debug.Log("esta atacando");
        animator.Play("Damage");

        // Set damage text

        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        } else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }
        if(damage > 0)
        {
            GameControllerObj.GetComponent<BattleGameController>().battleText.gameObject.SetActive(true);
            GameControllerObj.GetComponent<BattleGameController>().battleText.text = damage.ToString();
        }
        Invoke("ContinueGame", 2);
    }

    public void updateResistenceFill(float cost)
    {
        if(cost > 0)
        {
            resistence = resistence - cost;
            xNewresistenceScale = resistenceScale.x * (resistence / startResistence);
            resistenceFill.transform.localScale = new Vector2(xNewresistenceScale, resistenceScale.y);
        }
    }

    public bool GetDead()
    {
        return dead;
    }

    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<BattleGameController>().NextTurn();
    }
    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((CharacterStats)otherStats).nextActTurn);
        return nex;
    }

}


