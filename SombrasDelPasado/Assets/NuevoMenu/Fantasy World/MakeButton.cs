/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject character;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        character = GameObject.FindGameObjectWithTag("Character");
    }

    private void AttachCallback(string btn)
    {
        if (btn.CompareTo("Move1Btn") == 0)
        {
            character.GetComponent<CharacterAction>().SelectAttack("move1");
        } else if (btn.CompareTo("Move2Btn") == 0)
        {
            character.GetComponent<CharacterAction>().SelectAttack("move2");
        } //else
        //{
        //    character.GetComponent<CharacterAction>().SelectAttack("run");
        //}
    }
} */

using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private string moveType;  // Tipo de movimiento (move1, move2, etc.)

    private CombatPanel combatPanel;

    private void Start()
    {
        combatPanel = FindObjectOfType<CombatPanel>();
        if (combatPanel != null)
        {
            GetComponent<Button>().onClick.AddListener(SeleccionarMovimiento);
        }
        else
        {
            Debug.LogError("CombatPanel not found");
        }
    }

    public void SeleccionarMovimiento()
    {
        if (combatPanel != null)
        {
            if (combatPanel.characterInstance != null)
            {
                CharacterAction characterAction = combatPanel.characterInstance.GetComponent<CharacterAction>();
                
                if (characterAction != null)
                {
                    characterAction.SelectAttack(moveType);
                }
                else
                {
                    Debug.LogError("CharacterAction component not found on characterInstance");
                }
            }
            else
            {
                Debug.LogError("characterInstance is not set in CombatPanel");
            }
        }
        else
        {
            Debug.LogError("CombatPanel is not set");
        }
    }


}
