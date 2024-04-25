
//////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject playerActive;
    public GameObject enemyTarget;
    public GameObject[] players;
    public bool playerEndTurn;

    public GameObject[] enemies;
    public bool enemyEndTurn;

    public int time;

    private float timeTrans;

    //nuevo
    public Image[] playerHealthBars;
    public Image[] playerManaBars;
    public Image[] playerJugoBars;

    public Image[] enemyHealthBars;
    public Image[] enemyManaBars;
    public Image[] enemyJugoBars;
    //




    private void Awake(){
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //nuevo
        playerHealthBars = new Image[players.Length];
        playerManaBars = new Image[players.Length];
        playerJugoBars = new Image[players.Length];

        enemyHealthBars = new Image[enemies.Length];
        enemyManaBars = new Image[enemies.Length];
        enemyJugoBars = new Image[enemies.Length];
        //
    }


    // Update is called once per frame
    void Update()
    {
        PlayerEndTurn();
        EnemyAtk();
    }


    //nuevo
    public void EndCombat()
    {
        bool allPlayersDead = true;
        bool allEnemiesDead = true;

        // Comprobar las barras de vida de los jugadores
        foreach (Image healthBar in playerHealthBars)
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
            Debug.Log("El jugador ha perdido");
        }
        else if (allEnemiesDead)
        {
            Debug.Log("El enemigo ha perdido");
        }
        else
        {
            Debug.Log("Combate terminado sin ganador");
        }
    }
    //

    public void PlayerEndTurn(){
        int x = 0;
        for(int i = 0; i < players.Length; i++){
            if(players[i].GetComponent<P_BattleController>().PlayerEndTurn())
            {
                x++;
            }
        }
        if(x== players.Length){
            playerEndTurn = true;
        }
    }

    public void PlayerSelect(GameObject PlayerSelect){
        playerActive = PlayerSelect;
    }

    public void PlayerDeSelect(){
        playerActive.GetComponent<P_BattleController>().PlayerDeSelect();
        playerActive = null;
    }

    public void EnemySelect(GameObject enemySelect){
        enemyTarget = enemySelect;
    }

    public void EnemyDeSelect(){
        enemyTarget.GetComponent<E_BattleController>().EnemyDeSelect();
        enemyTarget = null;
    }

    public void EnemyAtk(){
        if(playerEndTurn){
            for(int i=0; i < enemies.Length; i++)
            {
                if(enemies[i].GetComponent<E_BattleController>().enemyEndTurn == false)
                {
                    timeTrans += Time.deltaTime;
                    time = Mathf.RoundToInt(timeTrans);

                    if(time == 3)
                    {
                        enemies[i].GetComponent<E_BattleController>().EnemyAtk();
                        timeTrans = 0;
                        time = 0;
                    }
                
                    
                }
            }
            
        }
    }
    
}
/////////////////////////////////////////////////////////////////////



