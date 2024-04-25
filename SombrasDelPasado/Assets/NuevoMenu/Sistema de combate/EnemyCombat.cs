using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    public CombatManager combatManager;
    public Image healthBar;
    public Image abilityBar;
    public Image resistanceBar;

    private float currentAbilityValue = 100f;
    private float currentResistanceValue = 100f;
    private float currentHealthValue = 100f;

    private Image playerHealthBar;

    void Start()
    {
        playerHealthBar = combatManager.playerHealthBar;
    }

    public void AttackPlayer(int damage)
    {
        currentHealthValue -= damage;
        healthBar.fillAmount = currentHealthValue / 100f;
        // Aquí puedes añadir lógica para actualizar la interfaz de usuario con el daño recibido
    }

    public void AutoAttack()
    {
        int attackType = Random.Range(1, 5); // Genera un número aleatorio entre 1 y 4
        int damage = 0;

        switch (attackType)
        {
            case 1:
                if (currentAbilityValue >= 30)
                {
                    damage = 10;
                    currentAbilityValue -= 30;
                }
                break;
            case 2:
                if (currentAbilityValue >= 50)
                {
                    damage = 20;
                    currentAbilityValue -= 50;
                }
                break;
            case 3:
                if (currentResistanceValue >= 20)
                {
                    currentResistanceValue -= 20;
                    currentAbilityValue += 30;
                    if (currentAbilityValue > 100) currentAbilityValue = 100;
                }
                break;
            case 4:
                if (currentResistanceValue >= 40)
                {
                    currentResistanceValue -= 40;
                    currentHealthValue += 20;
                    if (currentHealthValue > 100) currentHealthValue = 100;
                }
                break;
        }

        if (damage > 0)
        {
            AttackPlayer(damage);
        }

        UpdateBars();
    }

    void UpdateBars()
    {
        healthBar.fillAmount = currentHealthValue / 100f;
        abilityBar.fillAmount = currentAbilityValue / 100f;
        resistanceBar.fillAmount = currentResistanceValue / 100f;
    }
}
