using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public CombatManager combatManager;
    public Image healthBar;
    public Image abilityBar;
    public Image resistanceBar;
    public TMP_Text damageText;
    public GameObject attackPrefab;

    private float currentAbilityValue = 100f;
    private float currentResistanceValue = 100f;
    private float currentHealthValue = 100f;

    private Image enemyHealthBar;

    void Start()
    {
        enemyHealthBar = combatManager.enemyHealthBar;
    }

    public void Attack(int attackType)
    {
        int damage = 0;
        switch (attackType)
        {
            case 1:
                if (currentAbilityValue >= 30)
                {
                    damage = 10;
                    currentAbilityValue -= 30;
                    UpdateBars();
                    ShowDamageText(damage);
                    combatManager.EnemyTurn(); // Llamada corregida aquí
                }
                break;
            case 2:
                if (currentAbilityValue >= 50)
                {
                    damage = 20;
                    currentAbilityValue -= 50;
                    UpdateBars();
                    ShowDamageText(damage);
                    combatManager.EnemyTurn(); // Llamada corregida aquí
                }
                break;
            case 3:
                if (currentResistanceValue >= 20)
                {
                    currentResistanceValue -= 20;
                    currentAbilityValue += 30;
                    if (currentAbilityValue > 100) currentAbilityValue = 100;
                    UpdateBars();
                    // Reproducir animación de ataque "violin"
                }
                break;
            case 4:
                if (currentResistanceValue >= 40)
                {
                    currentResistanceValue -= 40;
                    currentHealthValue += 20;
                    if (currentHealthValue > 100) currentHealthValue = 100;
                    UpdateBars();
                    // Reproducir animación de ataque "curar"
                }
                break;
        }
    }

    void ShowDamageText(int damage)
    {
        GameObject damageTextInstance = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        damageTextInstance.GetComponent<Text>().text = "-" + damage.ToString();
        Destroy(damageTextInstance, 1f); // Destruir el texto de daño después de 1 segundo
    }

    void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 100f;
        abilityBar.fillAmount = currentAbilityValue / 100f;
        resistanceBar.fillAmount = currentResistanceValue / 100f;
    }
}
