using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DoorLockCode : MonoBehaviour
{
    public Button[] digitButtons; // Array para almacenar los botones de los dígitos
    public Button submitButton; // Botón para enviar el código
    public Button clearButton; // Botón para limpiar el código
    public TMP_Text messageText; // Texto para mostrar mensajes al usuario y números ingresados
    public Color correctColor = Color.green; // Color para el código correcto
    public Color incorrectColor = Color.red; // Color para el código incorrecto
    public Color grayColor = Color.gray; // Color para los números ingresados

    private int[] correctCode = { 0, 4, 2 }; // Código correcto
    private int[] enteredCode = new int[3]; // Código ingresado por el usuario
    private int currentIndex = 0; // Índice actual del código ingresado


    public bool isCodeCorrect = false; // Variable pública para verificar si el código es correcto


    [SerializeField] Animator puerta_anim;
    [SerializeField] GameObject panel;

    private void Start()
    {
        // Asignar funciones a los botones
        for (int i = 0; i < digitButtons.Length; i++)
        {
            int digit = i == digitButtons.Length - 1 ? 0 : i + 1; // Asignar el número correspondiente a cada botón
            digitButtons[i].onClick.AddListener(() => AddDigit(digit));
        }
        
        submitButton.onClick.AddListener(CheckCode);
        clearButton.onClick.AddListener(ClearCode);
    }

    private void AddDigit(int digit)
    {
        if (currentIndex < enteredCode.Length)
        {
            enteredCode[currentIndex] = digit;
            currentIndex++;
            UpdateMessageText();
        }
    }


    private void UpdateMessageText()
    {
        string codeText = "";

        // Convertir los números ingresados en una cadena
        for (int i = 0; i < enteredCode.Length; i++)
        {
            // Reemplazar los números no ingresados por guiones "-" y mostrar los números ingresados
            if (i < currentIndex)
            {
                codeText += enteredCode[i].ToString();
            }
            else
            {
                codeText += "-";
            }
        }

        messageText.text = codeText;
    }


    private void CheckCode()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctCode.Length; i++)
        {
            if (enteredCode[i] != correctCode[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            
            messageText.color = correctColor; // Cambiar color del texto a verde
    
            OpenDoor();
            isCodeCorrect = true; //nuevo


        }
        else
        {
            isCodeCorrect = false; //nuevo
            messageText.color = incorrectColor; // Cambiar color del texto a rojo
            Invoke("ResetCode", 1f); // Reiniciar el código después de 1 segundo
        }
    }

    private void ClearCode()
    {
        currentIndex = 0;
        for (int i = 0; i < enteredCode.Length; i++)
        {
            enteredCode[i] = 0;
        }
        UpdateMessageText();
        messageText.color = Color.white; // Restaurar color del texto a negro
        
    }

    private void ResetCode()
    {
        currentIndex = 0;
        for (int i = 0; i < enteredCode.Length; i++)
        {
            enteredCode[i] = 0;
        }
        UpdateMessageText();
        messageText.color = Color.white; // Restaurar color del texto a negro
    }

    private void OpenDoor()
    {

        
        Invoke("DesactivarPanel", 1f);
        Invoke("AbrirPuerta", 2f);
        Invoke("EliminarPanel",3f);

    }

    public void DesactivarPanel(){
        panel.SetActive(false);

    }

    public void AbrirPuerta(){
        puerta_anim.SetTrigger("Open");
    }

    public void EliminarPanel(){
        Destroy(panel);
    }
}




