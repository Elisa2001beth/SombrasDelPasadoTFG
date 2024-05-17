using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool jugadorDetectado = false;
    private BattleManager battleManager;
    private Animator animador;

    public float velocidad = 2.0f; // Velocidad de movimiento del enemigo
    public Transform puntoInicial; // Punto inicial del movimiento
    public Transform puntoFinal;   // Punto final del movimiento

    private Vector3 destinoActual;

    private void Start()
    {
        animador = GetComponent<Animator>();
        battleManager = FindObjectOfType<BattleManager>();
        destinoActual = puntoFinal.position; // Comenzar moviéndose hacia el punto final
    }

    private void Update()
    {
        // Si el jugador no ha sido detectado, mover al enemigo entre los dos puntos
        if (!jugadorDetectado)
        {
            MoverEnemigo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!jugadorDetectado && other.CompareTag("Player"))
        {
            Debug.Log("Colision");
            battleManager.StartBattle();

            jugadorDetectado = true;  // Marcar que el jugador ha sido detectado
        }
        else
        {
            jugadorDetectado = false;
        }
    }

    void MoverEnemigo()
    {
        // Mover al enemigo hacia el destino actual
        float step = velocidad * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, step);

        // Calcular la dirección del movimiento y rotar el enemigo
        Vector3 direccion = destinoActual - transform.position;
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, step);
        }

        // Comprobar si el enemigo ha llegado al destino
        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            // Cambiar el destino al otro punto
            destinoActual = destinoActual == puntoInicial.position ? puntoFinal.position : puntoInicial.position;
        }

        // Actualizar la animación del enemigo
        if (animador != null)
        {
            animador.SetFloat("Speed", velocidad);
        }
    }
}

