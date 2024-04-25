
///////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class P_BattleController : MonoBehaviour
{
    public bool playerEndTurn;
    public GameObject markerSelect;
    //nuevo
    public GameObject action1;
    public GameObject action2;
    public GameObject action3;
    public GameObject action4;

    private BattleManager sbm;

    public Animator playerAnim;

    //nuevo
    public Image healthBar;
    public Image manaBar;
    public Image jugoBar;

    private float currentHealthValue = 200f;
    private float currentManaValue = 100f;
    private float currentJugoValue = 80f;

    private Image[] enemyHealthBar;

    public TMP_Text damageText;

    int damage =0;
    
    void Start()
    {
        enemyHealthBar = sbm.enemyHealthBars;
        Debug.Log(currentJugoValue);
    }
    //


    private void Awake(){
        sbm = FindObjectOfType<BattleManager>();
        playerAnim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        PlayerEndTurn();
        Debug.Log(currentJugoValue);
    }

    private void OnMouseDown(){

        if(playerEndTurn == false){
            if(sbm.playerActive != gameObject && sbm.playerActive != null)
            {
                sbm.PlayerDeSelect();
            }

            PlayerSelect();
            sbm.PlayerSelect(gameObject);
        
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
        

        sbm.enemyTarget.GetComponent<E_BattleController>().EnemyDeSelect();
    }

    //nuevo

    public void ActionButton1(){
        //playerAnim.SetTrigger("Atk");
        //playerEndTurn = true;
        //EnemyDamage();//esto no debe ir asi debe hacerse una accion en la animacion. minuto 9:36 video 08-Activacion animacion script/Batalla turnos unity
        damage = Random.Range(5,10);
        UpdateBars();
        ShowDamageText(damage);
        playerAnim.SetTrigger("Atk");
        playerEndTurn = true;
        EnemyDamage(damage);
    }

    public void ActionButton2(){
        //playerAnim.SetTrigger("Atk");
        //playerEndTurn = true;
        //EnemyDamage();//esto no debe ir asi debe hacerse una accion en la animacion. minuto 9:36 video 08-Activacion animacion script/Batalla turnos unity
        if (currentManaValue >= 20){
            damage = Random.Range(15,30);
            currentManaValue -= 20;
            UpdateBars();
            ShowDamageText(damage);
            playerAnim.SetTrigger("Atk");
            playerEndTurn = true;
            EnemyDamage(damage);    
        }   
    }

    public void ActionButton3(){
        //playerAnim.SetTrigger("Atk");
        //playerEndTurn = true;
        //EnemyDamage();//esto no debe ir asi debe hacerse una accion en la animacion. minuto 9:36 video 08-Activacion animacion script/Batalla turnos unity
        if (currentJugoValue >= 30){
            damage = Random.Range(15,30);
            currentJugoValue -= 40;
            currentManaValue += 30;
            if (currentManaValue > 100) currentManaValue = 100;
            UpdateBars();
            ShowDamageText(damage);
            playerAnim.SetTrigger("Atk");
            playerEndTurn = true;
            EnemyDamage(damage);    
        }   
    }

    public void ActionButton4(){
        //playerAnim.SetTrigger("Atk");
        //playerEndTurn = true;
        //EnemyDamage();//esto no debe ir asi debe hacerse una accion en la animacion. minuto 9:36 video 08-Activacion animacion script/Batalla turnos unity
        if (currentJugoValue >= 50){
            damage = Random.Range(35,60);
            currentJugoValue -= 50;
            currentHealthValue += 30;
            if (currentHealthValue > 200) currentHealthValue = 200;
            UpdateBars();
            ShowDamageText(damage);
            playerAnim.SetTrigger("Atk");
            playerEndTurn = true;
            EnemyDamage(damage);  

        }   
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

    public void EnemyDamage(int damage){
        Debug.Log("accion de daño enemigo");
        sbm.enemyTarget.GetComponent<E_BattleController>().enemyAnim.SetTrigger("Damage");
    }

    //nuevo
    public void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 200f;
        manaBar.fillAmount = currentManaValue / 100f;
        jugoBar.fillAmount = currentJugoValue / 80f;
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

}

/////////////////////////////////////////////////////////////////////



 