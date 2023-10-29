using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string text;
    public GameObject DialogueUI;
    private GameObject DialogueText;
    public bool used = false;
    private GameManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        DialogueUI = _manager.DialogueUI[0];
        DialogueText = _manager.DialogueUI[1];
    }
    
    public void Use()
    {
        //Debug.Log(da);
        if (!used)
        {
            DialogueUI.SetActive(true); // Включение холста
            DialogueText.GetComponent<TMP_Text>().text = text; // Текст Диалога
        }
        
    }
}
