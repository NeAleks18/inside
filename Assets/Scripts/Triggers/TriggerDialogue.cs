using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPawn>().inTriggerDialogue = true;
            other.gameObject.GetComponent<PlayerPawn>().dialogue = gameObject.GetComponent<Dialogue>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPawn>().inTriggerDialogue = false;
            other.gameObject.GetComponent<PlayerPawn>().dialogue = null;
        }
    }
}
