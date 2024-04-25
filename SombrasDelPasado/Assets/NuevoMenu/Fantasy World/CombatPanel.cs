/* using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatPanel : MonoBehaviour
{
    public GameObject panelCombate;
    
    //public Button botonUsarObjeto; // Botón para usar un objeto del inventario

    //private PlayerController playerController;

    public GameObject character;

    // Array de objetos a desactivar/activar
    public GameObject[] objetosADesactivar;

    private void Start()
    {
        //playerController = FindObjectOfType<PlayerController>();

        // Asignar el método UsarObjeto al botón botonUsarObjeto
        //botonUsarObjeto.onClick.AddListener(UsarObjeto);
    }

    public void IniciarCombate()
    {
        // Desactivar objetos de la escena
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }

        panelCombate.SetActive(true);
        character.SetActive(false);

        // Restringir movimientos del jugador
        //playerController.PuedeMoverse = false;

        // Aquí podrías inicializar el combate, establecer los turnos, etc.
    }

    public void FinalizarCombate()
    {
        // Reactivar objetos de la escena
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(true);
        }

        panelCombate.SetActive(false);
        character.SetActive(true);
        // Permitir que el jugador se mueva de nuevo
        //playerController.PuedeMoverse = true;
    }

    
 */

 using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CombatPanel : MonoBehaviour
{
    public GameObject panelCombate;
    public GameObject characterPrefab;
    public GameObject enemyPrefab;
    public TMP_Text battleText;
    public GameObject actionMenu;

    public GameObject characterInstance { get; private set; }
    public GameObject enemyInstance { get; private set; }
    public List<CharacterStats> characterStats { get; private set; }

    public GameObject[] objetosADesactivar;

    public void IniciarCombate()
    {
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }

        characterInstance = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
        enemyInstance = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

        characterStats = new List<CharacterStats>
        {
            characterInstance.GetComponent<CharacterStats>(),
            enemyInstance.GetComponent<CharacterStats>()
        };

        foreach (CharacterStats stats in characterStats)
        {
            if (stats != null)
            {
                stats.CalculateNextTurn(0);
            }
            else
            {
                Debug.LogError("CharacterStats component not found on Character or Enemy");
            }
        }

        panelCombate.SetActive(true);
        actionMenu.SetActive(true);

        NextTurn();
    }

    public void FinalizarCombate()
    {
        Destroy(characterInstance);
        Destroy(enemyInstance);

        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(true);
        }

        panelCombate.SetActive(false);
        actionMenu.SetActive(false);

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.PuedeMoverse = true;
        }
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
                currentCharacterStats.CalculateNextTurn(currentCharacterStats.nextActTurn);
                characterStats.Add(currentCharacterStats);

                if (currentCharacterStats.gameObject.tag == "Character")
                {
                    actionMenu.SetActive(true);
                }
                else
                {
                    actionMenu.SetActive(false);
                    string attackType = Random.Range(0, 2) == 1 ? "move1" : "move2";
                    currentCharacterStats.gameObject.GetComponent<CharacterAction>().SelectAttack(attackType);
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
