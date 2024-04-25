using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject combatPanel;

    private GameObject currentPlayer;
    private GameObject currentEnemy;

    public Image playerHealthBar;
    public Image enemyHealthBar;

    public GameObject[] objectsToDeactivate;

    private bool isCombatActive = false;

    void Start()
    {
        // Instanciar los prefabs y desactivarlos
        currentPlayer = Instantiate(playerPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        currentEnemy = Instantiate(enemyPrefab, new Vector3(2, 0, 0), Quaternion.identity);
        currentPlayer.SetActive(false);
        currentEnemy.SetActive(false);
        
        // Desactivar objetos de la escena
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        // Desactivar el panel de combate al inicio
        combatPanel.SetActive(false);
    }

    public void StartCombat()
    {
        // Desactivar objetos de la escena
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        // Activar el panel de combate y los prefabs
        combatPanel.SetActive(true);
        currentPlayer.SetActive(true);
        currentEnemy.SetActive(true);

        isCombatActive = true;
    }

    public void EndCombat()
    {
        if (playerHealthBar.fillAmount <= 0)
        {
            Debug.Log("El jugador ha perdido");
        }
        else if (enemyHealthBar.fillAmount <= 0)
        {
            Debug.Log("El enemigo ha perdido");
        }
        else
        {
            Debug.Log("Combate terminado sin ganador");
        }

        // Activar objetos de la escena
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }

        // Desactivar el panel de combate y los prefabs
        combatPanel.SetActive(false);
        currentPlayer.SetActive(false);
        currentEnemy.SetActive(false);

        isCombatActive = false;
    }

    public void PlayerTurn(int attackType)
    {
        currentPlayer.GetComponent<PlayerCombat>().Attack(attackType);

        // Comprobar si el combate ha terminado
        if (isCombatActive)
        {
            EnemyTurn();
        }
    }

    public void EnemyTurn()
    {
        currentEnemy.GetComponent<EnemyCombat>().AutoAttack();

        // Comprobar si el combate ha terminado
        if (isCombatActive)
        {
            // Aquí puedes añadir lógica para pasar el turno al jugador nuevamente si es necesario
        }
    }
}
