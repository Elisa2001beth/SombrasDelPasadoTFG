

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool jugadorDetectado = false;
    private BattleManager battleManager;

    private Animator animador;
    public float velocidad = 2.0f; // Velocidad de movimiento del enemigo
    public float distancia = 5.0f; // Distancia de movimiento hacia adelante y hacia atrás

    private void Start()
    {
     
        animador = GetComponent<Animator>();
        battleManager = FindObjectOfType<BattleManager>();

    }

    private void Update()
    {
        // Si el jugador no ha sido detectado, mover al enemigo de adelante hacia atrás
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
        // Calcular la dirección del movimiento
        float movimiento = Mathf.PingPong(Time.time * velocidad, distancia * 2) - distancia;

        // Mover al enemigo en el eje Z
        Vector3 nuevaPosicion = transform.position;
        nuevaPosicion.z += movimiento * Time.deltaTime;
        transform.position = nuevaPosicion;

        // Actualizar la animación del enemigo
        if (animador != null)
        {
            animador.SetFloat("Speed", Mathf.Abs(movimiento));
        }
    }
}


