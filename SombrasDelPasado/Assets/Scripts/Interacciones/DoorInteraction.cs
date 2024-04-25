using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    //public string InteractionPrompt {get;}
    public bool Interact(Interactor interactor)
    {
        Debug.Log("opening door");
        return true;
    }
}
