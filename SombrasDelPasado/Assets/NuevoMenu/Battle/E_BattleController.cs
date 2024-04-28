
//////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class E_BattleController : MonoBehaviour
{
    private const string ATK = "Atk";

    public GameObject markerSelect;
    private BattleManager _sbm;
    public bool enemyEndTurn;
    public GameObject playerTarget;

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


    //nuevo
    void Start()
    {
        playerHealthBar = _sbm.playerHealthBars;
    }
    //

    private void Awake(){
        _sbm = FindObjectOfType<BattleManager>();

        enemyAnim = GetComponent<Animator>();
    }

    private void OnMouseDown(){
        if(_sbm.playerActive == null){
            return; // No seleccionar al enemigo si no hay jugador activo seleccionado
        }

        if(_sbm.enemyTarget != gameObject && _sbm.enemyTarget != null){
            _sbm.EnemyDeSelect();
        }
        
        EnemySelect();
        _sbm.EnemySelect(gameObject);
    }

    private void EnemySelect(){
        markerSelect.SetActive(true);
        _sbm.playerActive.GetComponent<P_BattleController>().action1.SetActive(true);
        _sbm.playerActive.GetComponent<P_BattleController>().action2.SetActive(true);
        _sbm.playerActive.GetComponent<P_BattleController>().action3.SetActive(true);
        _sbm.playerActive.GetComponent<P_BattleController>().action4.SetActive(true);
    }

    public void EnemyDeSelect(){
        markerSelect.SetActive(false);
    }

    public void EnemyAtk(){
        playerTarget = _sbm.players[Random.Range(0,_sbm.players.Length)];
        //enemyAnim.SetTrigger("Atk");
        //enemyEndTurn = true;
        //PlayerDamage();

        //nuevo
        int attackType = Random.Range(1,5);

        int minDamage = 0;
        int maxDamage = 100;
        string animTrigger = "";

        switch (attackType)
        {
            case 1:
                if (currentManaValue >= 30)
                {
                    minDamage = 5;
                    maxDamage = 20;
                    animTrigger = ATK;

                    currentManaValue -= 30;
                    Debug.Log("ataque1");
                }
                break;
            case 2:
                if (currentManaValue >= 50)
                {
                    minDamage = 15;
                    maxDamage = 30;
                    animTrigger = ATK;

                    currentManaValue -= 50;
                    Debug.Log("ataque2");
                }
                break;
            case 3:
                if (currentJugoValue >= 20)
                {
                    minDamage = 35;
                    maxDamage = 70;
                    animTrigger = ATK;

                    currentJugoValue -= 20;
                    currentManaValue += 30;
                    Debug.Log("ataque3");
                    if (currentManaValue > 150) currentManaValue = 150;
                }
                break;
            case 4:
                if (currentJugoValue >= 40)
                {
                    minDamage = 35;
                    maxDamage = 70;
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
        UpdateBars();
        PlayerDamage(damage);
        //
    }
    
    //nuevo
    public void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 400f;
        manaBar.fillAmount = currentManaValue / 150f;
        jugoBar.fillAmount = currentJugoValue / 120f;
    }
    //

    public void PlayerDamage(int damage){
        Debug.Log("accion de daño luke");
        playerTarget.GetComponent<P_BattleController>().playerAnim.SetTrigger("Damage");
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
}

///////////////////////////////////////////////////////////////////////


 