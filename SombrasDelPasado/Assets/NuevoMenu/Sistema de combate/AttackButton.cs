using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public int attackType;

    public void OnClick()
    {
        playerCombat.Attack(attackType);
    }
}
