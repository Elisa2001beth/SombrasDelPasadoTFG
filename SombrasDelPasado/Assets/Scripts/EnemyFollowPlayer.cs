using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener referencia al Animator
    }

    // Update is called once per frame
    void Update()
    {
        // Establecer la posición de destino del enemigo
        enemy.SetDestination(Player.position);

        // Determinar si el enemigo está en movimiento
        bool isMoving = enemy.velocity.magnitude > 0.1f;

        // Si el enemigo está en movimiento, reproducir la animación de caminar
        if (isMoving)
        {
            animator.SetTrigger("Walk");
        }
        else // Si el enemigo no está en movimiento, reproducir la animación de idle
        {
            animator.SetTrigger("Idle");
        }

        Debug.Log(isMoving);
    }
}
