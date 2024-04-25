/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAction : MonoBehaviour
{
    private GameObject character;
    private GameObject enemy;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject rangePrefab;

    [SerializeField]
    private Image faceIcon;

    private GameObject currentAttack;
    
    void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    public void SelectAttack(string btn)
    {
        GameObject victim = character;
        if (tag == "Character")
        {
            victim = enemy;
        }
        if (btn.CompareTo("move1") == 0)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim);

        } else if (btn.CompareTo("move2") == 0)
        {
            rangePrefab.GetComponent<AttackScript>().Attack(victim);
        } //else
        //{
        //    Debug.Log("Run");
        //}
    }
}
 */

using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    private GameObject character;
    private GameObject enemy;

    public GameObject meleePrefab;
    public GameObject rangePrefab;

    public void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = character;
        if (gameObject.CompareTag("Character"))
        {
            victim = enemy;
        }

        if (btn.CompareTo("move1") == 0)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(gameObject, victim);
        }
        else if (btn.CompareTo("move2") == 0)
        {
            rangePrefab.GetComponent<AttackScript>().Attack(gameObject, victim);
        }
    }
}
