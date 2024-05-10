using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class E_BattleController : MonoBehaviour
{
    private const string ATK = "Atk";
    private const string DIE = "Dead";

    public GameObject markerSelect;
    private BattleManager _sbm;
    public bool enemyEndTurn;
    public P_BattleController playerTarget;

    private P_BattleController _playerController;


    public Animator enemyAnim;


    //nuevo
    public Image healthBar;
    public Image manaBar;
    public Image jugoBar;

    private float currentHealthValue = 400f;
    private float currentManaValue = 150f;
    private float currentJugoValue = 120f;

    private Image[] playerHealthBar;

    public TMP_Text damageText;

    List<P_BattleController> alivePlayers = new List<P_BattleController>();
    //nuevo
    void Start()
    {
        playerHealthBar = _sbm.playersHealthBars;
    }
    //

    private void Awake(){
        _sbm = FindObjectOfType<BattleManager>();
        _playerController = FindObjectOfType<P_BattleController>();

        enemyAnim = GetComponent<Animator>();
    }

 

    private void OnMouseDown()
    {
        if (_sbm.playerActive == null || currentHealthValue <= 0)
        {
            return; // No seleccionar al enemigo si no hay jugador activo seleccionado o el enemigo está muerto
        }

        if (_sbm.enemyTarget != gameObject && _sbm.enemyTarget != null)
        {
            _sbm.EnemyDeSelect();
        }

        EnemySelect();
        _sbm.EnemySelect(this);
    }

    private void EnemySelect(){
        markerSelect.SetActive(true);
        _sbm.playerActive.action1.SetActive(true);
        _sbm.playerActive.action2.SetActive(true);
        _sbm.playerActive.action3.SetActive(true);
        _sbm.playerActive.action4.SetActive(true);
    }

    public void EnemyDeSelect(){
        markerSelect.SetActive(false);
    }

    //public void EnemyAtk(){
    //    // TODO: Comprobar que el jugador seleccionado esté vivo.
    //    // 1 Obtener un array con solo los vivos
    //    // 2 Obtener random de ese nuevo array

    //    playerTarget = _sbm.players[Random.Range(0,_sbm.players.Length)];

    //    //nuevo
    //    int attackType = Random.Range(1,5);

    //    int minDamage = 0;
    //    int maxDamage = 100;
    //    string animTrigger = "";

    //    switch (attackType)
    //    {
    //        case 1:
    //            if (currentManaValue >= 30)
    //            {
    //                minDamage = 5;
    //                maxDamage = 15;
    //                animTrigger = ATK;

    //                currentManaValue -= 30;
    //                Debug.Log("ataque1");
    //            }
    //            break;
    //        case 2:
    //            if (currentManaValue >= 50)
    //            {
    //                minDamage = 20;
    //                maxDamage = 35;
    //                animTrigger = ATK;

    //                currentManaValue -= 50;
    //                Debug.Log("ataque2");
    //            }
    //            break;
    //        case 3:
    //            if (currentJugoValue >= 20)
    //            {
    //                minDamage = 40;
    //                maxDamage = 55;
    //                animTrigger = ATK;

    //                currentJugoValue -= 35;
    //                currentManaValue += 40;
    //                Debug.Log("ataque3");
    //                if (currentManaValue > 150) currentManaValue = 150;
    //            }
    //            break;
    //        case 4:
    //            if (currentJugoValue >= 40)
    //            {
    //                minDamage = 60;
    //                maxDamage = 80;
    //                animTrigger = ATK;

    //                currentJugoValue -= 40;
    //                currentHealthValue += 20;
    //                Debug.Log("ataque4");
    //                if (currentHealthValue > 400) currentHealthValue = 400;
    //            }
    //            break;
    //    }

    //    int damage = Random.Range(minDamage, maxDamage);
    //    enemyAnim.SetTrigger(animTrigger);
    //    ShowDamageText(damage);

    //    enemyEndTurn = true;
    //    UpdateBars();
    //    PlayerDamageRecive(damage);
    //    //
    //}

    public void EnemyAtk()
    {
     
        foreach (P_BattleController player in _sbm.players)
        {
          
            float playerHealth = player.GetCurrentHealthValue(); // Obteniendo la salud actual del jugador
            if (playerHealth > 0)
            {
                alivePlayers.Add(player);
                
            }
        }

        // Verificar si hay jugadores vivos disponibles para atacar
        if (alivePlayers.Count > 0)
        {
            // Seleccionar un jugador vivo al azar
            playerTarget = alivePlayers[Random.Range(0, alivePlayers.Count)];
           
            int attackType = Random.Range(1, 5);

            int minDamage = 0;
            int maxDamage = 100;
            string animTrigger = "";

            switch (attackType)
            {
                case 1:
                    if (currentManaValue >= 30)
                    {
                        minDamage = 5;
                        maxDamage = 15;
                        
                        animTrigger = ATK;

                        currentManaValue -= 30;
                        Debug.Log("ataque1");
                    }
                    break;
                case 2:
                    if (currentManaValue >= 50)
                    {
                        minDamage = 20;
                        maxDamage = 35;
                      
                        animTrigger = ATK;

                        currentManaValue -= 50;
                        Debug.Log("ataque2");
                    }
                    break;
                case 3:
                    if (currentJugoValue >= 20)
                    {
                        minDamage = 40;
                        maxDamage = 55;
                        
                        animTrigger = ATK;

                        currentJugoValue -= 35;
                        currentManaValue += 40;
                        Debug.Log("ataque3");
                        if (currentManaValue > 150) currentManaValue = 150;
                    }
                    break;
                case 4:
                    if (currentJugoValue >= 40)
                    {
                        minDamage = 60;
                        maxDamage = 80;
                        
                        animTrigger = ATK;

                        currentJugoValue -= 40;
                        currentHealthValue += 20;
                        Debug.Log("ataque4");
                        if (currentHealthValue > 400) currentHealthValue = 400;
                    }
                    break;
            }

            int damage = Random.Range(minDamage, maxDamage);
            enemyAnim.SetTrigger(animTrigger);
            ShowDamageText(damage);

            enemyEndTurn = true;
            
            playerTarget.PlayerDamage(damage);
            playerTarget.UpdateBars();
            //UpdateBars();
            PlayerDamageRecive(damage);
        }
       


    }


    //nuevo
    public void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 400f;
        manaBar.fillAmount = currentManaValue / 150f;
        jugoBar.fillAmount = currentJugoValue / 120f;
        Debug.Log("enemigo" + currentHealthValue);
    }
    //

    public void PlayerDamageRecive(int damage){
        playerTarget.GetComponent<P_BattleController>().playerAnim.SetTrigger("Damage");
        
        currentHealthValue -= damage;
        if (currentHealthValue <= 0)
        {
            // El jugador ha muerto, por lo que lo eliminamos de la lista de jugadores vivos
            alivePlayers.Remove(playerTarget);
            
        }

        _playerController.PlayerDamage(damage);

    }

    public void EnemyDamage(int damage)
    {
        currentHealthValue -= damage;
        UpdateBars();
        if (currentHealthValue <= 0)
        {
            enemyAnim.SetTrigger(DIE); // Activar la animación de "Dead" si la vida llega a cero
        }
    }

    void ShowDamageText(int damage)
    {
        damageText.text = "-" + damage.ToString();
        StartCoroutine(FadeOutDamageText());
    }

    IEnumerator FadeOutDamageText()
    {
        float fadeTime = 1f;
        float alpha = 1f;  // Inicialmente el texto es completamente visible

        damageText.gameObject.SetActive(true);

        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeTime;
            damageText.alpha = alpha;
            yield return null;
        }

        damageText.gameObject.SetActive(false);  // Oculta el texto después de desvanecerlo
    }

    public void ResetTurn()
    {
        enemyEndTurn = false;
    }
}
