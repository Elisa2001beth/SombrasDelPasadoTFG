

//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    private bool jugadorDetectado = false;
//    //private PanelCombate panelCombate; //para el script panel combate
//    //private CombatPanel panelCombate; //para el script combatpanel

//    private CombatManager panelCombate;
//    private Animator animador;
//    public float velocidad = 2.0f; // Velocidad de movimiento del enemigo
//    public float distancia = 5.0f; // Distancia de movimiento hacia adelante y hacia atrás

//    private void Start()
//    {
//        //panelCombate = FindObjectOfType<PanelCombate>(); //para el script panel combat
//        //panelCombate = FindObjectOfType<CombatPanel>();
//        panelCombate = FindObjectOfType<CombatManager>();
//        if (panelCombate == null)
//        {
//            Debug.LogError("PanelCombate no encontrado");
//        }
//        else
//        {
//            // Desactivar el panel de combate al inicio
//            panelCombate.gameObject.SetActive(false);
//        }

//        // Obtener el componente Animator del enemigo
//        animador = GetComponent<Animator>();
//        if (animador == null)
//        {
//            Debug.LogError("Animator no encontrado en el enemigo");
//        }
//    }

//    private void Update()
//    {
//        // Si el jugador no ha sido detectado, mover al enemigo de adelante hacia atrás
//        if (!jugadorDetectado)
//        {
//            MoverEnemigo();
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("OnTriggerEnter llamado");
//        if (!jugadorDetectado && other.CompareTag("Character"))
//        {
//            Debug.Log("Colisión detectada con el jugador");
//            ActivarCombate();
//            jugadorDetectado = true;  // Marcar que el jugador ha sido detectado
//        }
//    }

//    void ActivarCombate()
//    {
//        Debug.Log("Activando combate");

//        // Restringir movimientos del jugador
//        PlayerController playerController = FindObjectOfType<PlayerController>();
//        if (playerController != null)
//        {
//            playerController.PuedeMoverse = false;

//            // Activar el panel de combate
//            if (panelCombate != null)
//            {
//                panelCombate.gameObject.SetActive(true);  // Activar el panel de combate
//                panelCombate.StartCombat();
//                Debug.Log("Panel de combate activado");
//            }
//        }
//        else
//        {
//            Debug.LogError("PlayerController no encontrado");
//        }
//    }

//    void MoverEnemigo()
//    {
//        // Calcular la dirección del movimiento
//        float movimiento = Mathf.PingPong(Time.time * velocidad, distancia * 2) - distancia;

//        // Mover al enemigo en el eje Z
//        Vector3 nuevaPosicion = transform.position;
//        nuevaPosicion.z += movimiento * Time.deltaTime;
//        transform.position = nuevaPosicion;

//        // Actualizar la animación del enemigo
//        if (animador != null)
//        {
//            animador.SetFloat("Speed", Mathf.Abs(movimiento));
//        }
//    }
//}


