

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class P_BattleController : MonoBehaviour
{
    private const string ATK = "Atk";
    private const string DIE = "Dead";



    public bool playerEndTurn { get; private set; }
    public GameObject markerSelect;
    //nuevo
    public GameObject action1;
    public GameObject action2;
    public GameObject action3;
    public GameObject action4;

    private BattleManager _sbm;

    public Animator playerAnim;

    //nuevo
    public Image healthBar;
    public Image manaBar;
    public Image jugoBar;

    private float currentHealthValue = 200f;
    private float currentManaValue = 100f;
    private float currentJugoValue = 80f;

    private Image[] enemyHealthBar;

    //nuevo
    private E_BattleController _enemyController;


    public TMP_Text damageText;
    
    void Start()
    {
        enemyHealthBar = _sbm.enemyHealthBars;
        healthBar = _sbm.playersHealthBars[Array.IndexOf(_sbm.players, this)];

    }

    private void Awake(){
        _sbm = FindObjectOfType<BattleManager>();
        _enemyController = FindObjectOfType<E_BattleController>();
        playerAnim = GetComponent<Animator>();
    }


    private void OnMouseDown()
    {

        if (playerEndTurn || currentHealthValue <= 0)
        {
            return;
        }

        if (_sbm.GetBattleState() == BattleState.PLAYER_TURN)
        {
            if (_sbm.playerActive != gameObject && _sbm.playerActive != null)
            {
                _sbm.PlayerDeSelect();
            }

            PlayerSelect();
            _sbm.PlayerSelect(this);
        }
    }


    private void PlayerSelect(){
        markerSelect.SetActive(true);
    }

    public void PlayerDeSelect(){
        markerSelect.SetActive(false);
        //NUEVO
        action1.SetActive(false);
        action2.SetActive(false);
        action3.SetActive(false);
        action4.SetActive(false);     

        _sbm.enemyTarget.EnemyDeSelect();
    }

    //nuevo

    public void ActionButton1() {
        ActionCommonLogic(5, 20, ATK);
    }

    public void ActionButton2() {
        if (currentManaValue >= 20) {
            currentManaValue -= 20;
            ActionCommonLogic(30, 50, ATK);
        }   
    }

    public void ActionButton3(){
        if (currentJugoValue >= 30){
           
            currentJugoValue -= 40;
            currentManaValue += 30;
            if (currentManaValue > 100) currentManaValue = 100;
            ActionCommonLogic(40, 75, ATK);
        }   
    }

    public void ActionButton4(){
        if (currentJugoValue >= 50){
            currentJugoValue -= 50;
            currentHealthValue += 30;
            if (currentHealthValue > 200) currentHealthValue = 200;
            ActionCommonLogic(80, 100, ATK);
        }   
    }

    private void ActionCommonLogic(int minDamage, int maxDamage, string animTrigger)
    {
        //EnemyDamage();//esto no debe ir asi debe hacerse una accion en la animacion. minuto 9:36 video 08-Activacion animacion script/Batalla turnos unity

        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        UpdateBars();
        ShowDamageText(damage);
        playerAnim.SetTrigger(animTrigger);
        playerEndTurn = true;
        EnemyDamageRecive(damage);

        _sbm.CheckEnemyDeads();
        _sbm.PlayerDeSelect();
    }

    public float GetCurrentHealthValue()
    {
        return currentHealthValue;
    }

    public bool PlayerEndTurn(){
        if(playerEndTurn){
            markerSelect.SetActive(false);
            //NUEVO
            action1.SetActive(false);
            action2.SetActive(false);
            action3.SetActive(false);
            action4.SetActive(false);
            return true;
        }
        return false;
    }

    public void PlayerDamage(int damage) 
    {
        currentHealthValue -= damage;
        UpdateBars();
        if (currentHealthValue <= 0)
        {
            playerAnim.SetTrigger(DIE); // Activar la animación de "Dead" si la vida llega a cero
        }
    }




    public void EnemyDamageRecive(int damage){
        _sbm.enemyTarget.enemyAnim.SetTrigger("Damage");
        //_enemyController.currentHealthValue -= damage;// esto tiene que ser del enemigo y esta cogiendo la del player
        _enemyController.EnemyDamage(damage);
    }

    //nuevo
    public void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 200f;
        manaBar.fillAmount = currentManaValue / 100f;
        jugoBar.fillAmount = currentJugoValue / 80f;
        Debug.Log("jugador" + currentHealthValue);
    }
    //

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
        playerEndTurn = false;
    }

    public void ResetCombat()
    {
        currentHealthValue = 200f;
        currentManaValue = 100f;
        currentJugoValue = 80f;
        playerEndTurn = false;
    }
}
