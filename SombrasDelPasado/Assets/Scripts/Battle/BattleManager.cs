
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    PLAYER_TURN,
    ENEMY_TURN,
    END
}

public class BattleManager : MonoBehaviour
{
    private BattleState _bState = BattleState.PLAYER_TURN;

    public delegate void PlayerVictoryEvent();
    public static event PlayerVictoryEvent OnPlayerVictory;


    public BattleState GetBattleState()
    {
        return _bState;
    }

    public GameObject panel;
    public GameObject lukePlayer;
    public GameObject emilyPlayer;
    public Transform teleportTargetLuke;
    public Transform teleportTargetEmily;
    private PlayerController playerController;
    public GameObject mainCamera;
    public GameObject battleCamera;
    public GameObject[] objetosADesactivar;
    public GameObject enemigoObject;




    public P_BattleController playerActive;
    public P_BattleController[] players;

    public E_BattleController enemyTarget;
    public E_BattleController[] enemies;

    [SerializeField]
    private float _delayBtwnEnemies = 3f;

    
    public Image[] playersHealthBars;
    public Image[] playerManaBars;
    public Image[] playerJugoBars;

    public Image[] enemyHealthBars;
    public Image[] enemyManaBars;
    public Image[] enemyJugoBars;

    public TMP_Text victoryText;
    public TMP_Text defeatText;
    
    public void Start()
    {
        panel.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();

        victoryText.gameObject.SetActive(false);
        defeatText.gameObject.SetActive(false);
    }
    

    public void StartBattle()
    {
        mainCamera.SetActive(false);
        battleCamera.SetActive(true);

        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }

        panel.SetActive(true);
        ResetBarsPlayer();
        ResetBarsEnemies();
        _bState = BattleState.PLAYER_TURN;
        playerController.puedeMoverse = false;
    }

    public void ActivateObjects()
    {
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(true);
        }

    }

    public void ResetBarsPlayer()
    {
        

        foreach (Image healthBar in playersHealthBars)
        {
            healthBar.fillAmount = 1f;
        }

        foreach(Image manaBar in playerManaBars)
        {
            manaBar.fillAmount = 1f;
        }

        foreach(Image jugoBar in playerJugoBars)
        {
            jugoBar.fillAmount = 1f;
        }
    }

    public void ResetBarsEnemies()
    {


        foreach (Image healthBar in enemyHealthBars)
        {
            healthBar.fillAmount = 1f;
        }

        foreach (Image manaBar in enemyManaBars)
        {
            manaBar.fillAmount = 1f;
        }

        foreach (Image jugoBar in enemyJugoBars)
        {
            jugoBar.fillAmount = 1f;
        }
    }

    private void ResetPlayerPosition()
    {
        lukePlayer.transform.position = teleportTargetLuke.transform.position;
        emilyPlayer.transform.position = teleportTargetEmily.transform.position;
        playerController.puedeMoverse = true;

    }

    

    public void CheckEnemyDeads()
    {
        bool allEnemiesDead = true;

        foreach (Image healthBar in enemyHealthBars)
        {
            if (healthBar.fillAmount > 0)
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allEnemiesDead)
        {
            
            _bState = BattleState.END;

            EndGame();
            
        }
        else if (PlayersCanAttack())
        {
            _bState = BattleState.PLAYER_TURN;
        }
        else
        {
            _bState = BattleState.ENEMY_TURN;
            StartCoroutine(EnemiesAttack());
        }
    }

    private bool PlayersCanAttack()
    {
        bool atLeastOneCanAttack = false;

        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].playerEndTurn)
            {
                atLeastOneCanAttack = true;
                break;
            }
        }

        return atLeastOneCanAttack;
    }

    private IEnumerator EnemiesAttack()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(_delayBtwnEnemies);

            if (enemyHealthBars[i].fillAmount <= 0)
            {
                break;
            }

            enemies[i].EnemyAtk();

            if (CheckPlayersDeads())
            {
                _bState = BattleState.END;
                
                EndGame();
               
                break;
            }
        }

        NextTurn();
    }

    public bool CheckPlayersDeads()
    {
        bool allPlayersDead = true;

        foreach (Image healthBar in playersHealthBars)
        {
            if (healthBar.fillAmount > 0)
            {
                allPlayersDead = false;
                break;
            }
        }

        return allPlayersDead;
    }

 

    public void NextTurn()
    {
        _bState = BattleState.PLAYER_TURN;

        foreach (P_BattleController player in players)
        {
            player.ResetTurn();
        }

        foreach (E_BattleController enemy in enemies)
        {
            enemy.ResetTurn();
        }
    }


    public void PlayEnemyDeathAnimation()
    {
        // Reproduce la animación de muerte del enemigo
        foreach (E_BattleController enemy in enemies)
        {
            enemy.EnemyDead();
        }
    }

    public void PlayPlayerDeathAnimation()
    {
        // Reproduce la animación de muerte del jugador
        foreach (P_BattleController player in players)
        {
            player.PlayerDead();
        }
    }

    
    public void EndGame()
    {
        
        bool allPlayersDead = true;
        bool allEnemiesDead = true;

        // Comprobar las barras de vida de los jugadores
        foreach (Image healthBar in playersHealthBars)
        {
            if (healthBar.fillAmount > 0)
            {
                allPlayersDead = false;
                break;
            }
            
        }

        // Comprobar las barras de vida de los enemigos
        foreach (Image healthBar in enemyHealthBars)
        {
            if (healthBar.fillAmount > 0)
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allPlayersDead)
        {
            PlayPlayerDeathAnimation();
            Debug.Log("El jugador ha perdido");

            StartCoroutine(ShowDefeatMessage());

            ResetPlayerPosition();
            ResetBarsPlayer();
            ResetBarsEnemies();
            panel.SetActive(false);
            ActivateObjects();

            foreach (P_BattleController player in players)
            {
                player.ResetCombat();
            }

            foreach (E_BattleController enemy in enemies)
            {
                enemy.ResetCombat();
            }




        }
        else if (allEnemiesDead)
        {
            if (OnPlayerVictory != null)
            {
                OnPlayerVictory();
            }
            PlayEnemyDeathAnimation();
            

            StartCoroutine(ShowVictoryMessage());

            panel.SetActive(false);
            playerController.puedeMoverse = true;
            ActivateObjects();
            enemigoObject.SetActive(false);
            
        }

        mainCamera.SetActive(true);
        battleCamera.SetActive(false);

    }

    private IEnumerator ShowVictoryMessage()
    {
        victoryText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        victoryText.gameObject.SetActive(false);
    }

    private IEnumerator ShowDefeatMessage()
    {
        defeatText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        defeatText.gameObject.SetActive(false);
    }

    

    public void PlayerSelect(P_BattleController PlayerSelect)
    {
        playerActive = PlayerSelect;
    }

    public void PlayerDeSelect()
    {
        playerActive.PlayerDeSelect();
        playerActive = null;
    }

    public void EnemySelect(E_BattleController enemySelect)
    {
        enemyTarget = enemySelect;
    }

    public void EnemyDeSelect()
    {
        enemyTarget.EnemyDeSelect();
        enemyTarget = null;
    }

}




