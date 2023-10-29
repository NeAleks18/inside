using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBook : MonoBehaviour
{
    private Book Book;

    private void Awake()
    {
        Book = GetComponent<Book>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPawn>().inTriggerBook = true;
            other.gameObject.GetComponent<PlayerPawn>().book = Book;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPawn>().inTriggerBook = false;
        }
    }
}
