using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanel;
    //[SerializeField] private TMP_Text _promptText;

    private void Start(){
        _uiPanel.SetActive(false);

    }

    public bool IsDisplayed = false;

    public void SetUp(string promptText){
        //_promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close(){
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
}
