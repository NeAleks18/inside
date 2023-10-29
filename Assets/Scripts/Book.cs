using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Я заебался это делать
    public string headerBook;
    public string textInBook;
    private GameManager _manager;
    private bool timing = false;
    private bool da;

    private void Awake()
    {
        // Инициализация
        _manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void Switch()
    {
        //Debug.Log(da);
        if (da)
        {
                if (timing) return; // Задержка между свитчами
                _manager.BookUI[0].SetActive(true); // Включение холста
                _manager.BookUI[1].GetComponent<TMP_Text>().SetText(headerBook); // Текст загаловка
                _manager.BookUI[2].GetComponent<TMP_Text>().SetText(textInBook); // Текст книги
                StartCoroutine(timer()); // Старт задержки
        } else
        {
                if (timing) return; // Задержка между свитчами
                _manager.BookUI[0].SetActive(false); // Выключение холста
                StartCoroutine(timer()); // Старт задержки
        }
        
    }

    public void ExtremeClose()
    {
        // Принудительное выключение холста
        _manager.BookUI[0].SetActive(false);
        da = false;
        timing = false;
    }

    private IEnumerator timer()
    {
        // Задержка
        timing = true;
        yield return new WaitForSeconds(0.2f);
        timing = false;
        da = !da;
    }
}
