using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TMP_Text

public class DoorLockCode : MonoBehaviour
{
    public Button[] digitButtons; // Array para almacenar los botones de los dígitos
    public Button submitButton; // Botón para enviar el código
    public Button clearButton; // Botón para limpiar el código
    public TMP_Text messageText; // Texto para mostrar mensajes al usuario y números ingresados
    public Color correctColor = Color.green; // Color para el código correcto
    public Color incorrectColor = Color.red; // Color para el código incorrecto

    private int[] correctCode = { 0, 4, 2 }; // Código correcto
    private int[] enteredCode = new int[3]; // Código ingresado por el usuario
    private int currentIndex = 0; // Índice actual del código ingresado

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

        for (int i = 0; i < enteredCode.Length; i++)
        {
            codeText += enteredCode[i].ToString();
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
            //messageText.text = "Código correcto. ¡Puerta abierta!";
            messageText.color = correctColor; // Cambiar color del texto a verde
            // Aquí puedes añadir la lógica para abrir la puerta
            OpenDoor();
        }
        else
        {
            //messageText.text = "Código incorrecto. Inténtalo de nuevo.";
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
        messageText.color = Color.black; // Restaurar color del texto a negro
    }

    private void ResetCode()
    {
        currentIndex = 0;
        for (int i = 0; i < enteredCode.Length; i++)
        {
            enteredCode[i] = 0;
        }
        UpdateMessageText();
        messageText.color = Color.black; // Restaurar color del texto a negro
    }

    private void OpenDoor()
    {
        // Aquí puedes añadir la lógica para abrir la puerta.
        // Por ejemplo, desactivar la puerta cerrada y activar la puerta abierta.
        // doorClosedGameObject.SetActive(false);
        // doorOpenGameObject.SetActive(true);
    }
}


