
//////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class E_BattleController : MonoBehaviour
{
    public GameObject markerSelect;
    private BattleManager sbm;
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
    int damage = 0;
    //

    //nuevo
    void Start()
    {
        playerHealthBar = sbm.playerHealthBars;
    }
    //

    private void Awake(){
        sbm = FindObjectOfType<BattleManager>();
        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnMouseDown(){
        if(sbm.playerActive == null){
            return; // No seleccionar al enemigo si no hay jugador activo seleccionado
        }

        if(sbm.enemyTarget != gameObject && sbm.enemyTarget != null){
            sbm.EnemyDeSelect();
        }
        
        EnemySelect();
        sbm.EnemySelect(gameObject);
    }

    private void EnemySelect(){
        markerSelect.SetActive(true);
        sbm.playerActive.GetComponent<P_BattleController>().action1.SetActive(true);
        sbm.playerActive.GetComponent<P_BattleController>().action2.SetActive(true);
        sbm.playerActive.GetComponent<P_BattleController>().action3.SetActive(true);
        sbm.playerActive.GetComponent<P_BattleController>().action4.SetActive(true);
    }

    public void EnemyDeSelect(){
        markerSelect.SetActive(false);
    }

    public void EnemyAtk(){
        playerTarget = sbm.players[Random.Range(0,sbm.players.Length)];
        //enemyAnim.SetTrigger("Atk");
        //enemyEndTurn = true;
        //PlayerDamage();

        //nuevo
        int attackType = Random.Range(1,5);

        switch (attackType)
        {
            case 1:
                if (currentManaValue >= 30)
                {
                    damage = Random.Range(5,20);
                    currentManaValue -= 30;
                    enemyAnim.SetTrigger("Atk");
                    ShowDamageText(damage);
                    Debug.Log("ataque1");
                }
                break;
            case 2:
                if (currentManaValue >= 50)
                {
                    damage = Random.Range(15,30);
                    currentManaValue -= 50;
                    enemyAnim.SetTrigger("Atk");
                    ShowDamageText(damage);
                    Debug.Log("ataque2");
                }
                break;
            case 3:
                if (currentJugoValue >= 20)
                {
                    damage = Random.Range(35,70);
                    currentJugoValue -= 20;
                    currentManaValue += 30;
                    enemyAnim.SetTrigger("Atk");
                    ShowDamageText(damage);
                    Debug.Log("ataque3");
                    if (currentManaValue > 150) currentManaValue = 150;
                }
                break;
            case 4:
                if (currentJugoValue >= 40)
                {
                    damage = Random.Range(35,70);
                    currentJugoValue -= 40;
                    currentHealthValue += 20;
                    enemyAnim.SetTrigger("Atk");
                    ShowDamageText(damage);
                    Debug.Log("ataque4");
                    if (currentHealthValue > 400) currentHealthValue = 400;
                }
                break;
        }

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


 