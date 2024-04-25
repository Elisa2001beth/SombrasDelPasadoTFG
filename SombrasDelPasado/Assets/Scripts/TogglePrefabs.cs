using UnityEngine;
using UnityEngine.UI;

public class TogglePrefabs : MonoBehaviour
{
    public GameObject prefabActivo;
    public GameObject prefabInactivo;
    public Button botonOpciones;
    public Button botonVolver;

    private void Start()
    {
        // Activa el prefab activo y desactiva el prefab inactivo al iniciar
        prefabActivo.SetActive(true);
        prefabInactivo.SetActive(false);

        // Asigna los métodos a los botones
        botonOpciones.onClick.AddListener(DesactivarPrefabs);
        botonVolver.onClick.AddListener(ActivarPrefabs);
    }

    void DesactivarPrefabs()
    {
        prefabActivo.SetActive(false);
        prefabInactivo.SetActive(true);
    }

    void ActivarPrefabs()
    {
        prefabActivo.SetActive(true);
        prefabInactivo.SetActive(false);
    }
}
