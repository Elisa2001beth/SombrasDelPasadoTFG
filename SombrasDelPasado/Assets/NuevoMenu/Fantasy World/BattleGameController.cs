/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using TMPro; 

public class BattleGameController : MonoBehaviour
{
    private List<CharacterStats> characterStats;

    private GameObject battleMenu;

    public TMP_Text battleText;

    private void Awake()
    {
        battleMenu = GameObject.Find("ActionMenu");
    }
    void Start()
    {
        characterStats = new List<CharacterStats>();
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        CharacterStats currentCharacterStats = character.GetComponent<CharacterStats>();
        currentCharacterStats.CalculateNextTurn(0);
        characterStats.Add(currentCharacterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        CharacterStats currentEnemyStats = enemy.GetComponent<CharacterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        characterStats.Add(currentEnemyStats);

        characterStats.Sort();
        

        NextTurn();
    }

    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);
        CharacterStats currentCharacterStats = characterStats[0];
        characterStats.Remove(currentCharacterStats);
        if (!currentCharacterStats.GetDead())
        {
            GameObject currentUnit = currentCharacterStats.gameObject;
            currentCharacterStats.CalculateNextTurn(currentCharacterStats.nextActTurn);
            characterStats.Add(currentCharacterStats);
            characterStats.Sort();
            if(currentUnit.tag == "Character")
            {
                this.battleMenu.SetActive(true);
            } else
            {
                this.battleMenu.SetActive(false);
                string attackType = Random.Range(0, 2) == 1 ? "move1" : "move2";
                currentUnit.GetComponent<CharacterAction>().SelectAttack(attackType);
            }
        } else
        {
            NextTurn();
        }
    }
}
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using TMPro; 

public class BattleGameController : MonoBehaviour
{
    private List<CharacterStats> characterStats;

    private GameObject battleMenu;

    public TMP_Text battleText;

    private void Awake()
    {
        battleMenu = GameObject.Find("ActionMenu");

        if (battleMenu == null)
        {
            Debug.LogError("ActionMenu not found");
        }
    }

    void Start()
    {
        characterStats = new List<CharacterStats>();

        GameObject character = GameObject.FindGameObjectWithTag("Character");
        if (character != null)
        {
            CharacterStats currentCharacterStats = character.GetComponent<CharacterStats>();
            
            if (currentCharacterStats != null)
            {
                currentCharacterStats.CalculateNextTurn(0);
                characterStats.Add(currentCharacterStats);
            }
            else
            {
                Debug.LogError("CharacterStats component not found on Character");
            }
        }
        else
        {
            Debug.LogError("Character object not found");
        }

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            CharacterStats currentEnemyStats = enemy.GetComponent<CharacterStats>();
            
            if (currentEnemyStats != null)
            {
                currentEnemyStats.CalculateNextTurn(0);
                characterStats.Add(currentEnemyStats);
            }
            else
            {
                Debug.LogError("CharacterStats component not found on Enemy");
            }
        }
        else
        {
            Debug.LogError("Enemy object not found");
        }

        characterStats.Sort();

        NextTurn();
    }

    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);

        if (characterStats.Count > 0)
        {
            CharacterStats currentCharacterStats = characterStats[0];
            characterStats.Remove(currentCharacterStats);

            if (!currentCharacterStats.GetDead())
            {
                GameObject currentUnit = currentCharacterStats.gameObject;

                if (currentUnit != null)
                {
                    currentCharacterStats.CalculateNextTurn(currentCharacterStats.nextActTurn);
                    characterStats.Add(currentCharacterStats);
                    characterStats.Sort();

                    if (currentUnit.tag == "Character")
                    {
                        this.battleMenu.SetActive(true);
                    }
                    else
                    {
                        this.battleMenu.SetActive(false);
                        string attackType = Random.Range(0, 2) == 1 ? "move1" : "move2";
                        currentUnit.GetComponent<CharacterAction>().SelectAttack(attackType);
                    }
                }
                else
                {
                    Debug.LogError("Current unit object is null");
                }
            }
            else
            {
                NextTurn();
            }
        }
        else
        {
            Debug.LogError("No character stats available");
        }
    }
}
