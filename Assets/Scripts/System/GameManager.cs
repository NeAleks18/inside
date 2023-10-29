using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Пока что бесполезно но нужен для Book
    public GameObject[] BookUI;
    public GameObject[] DialogueUI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
