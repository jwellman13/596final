using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject helperUI;

    private void Awake()
    {
        helperUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            helperUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            helperUI.SetActive(false);
        }
    }
    public void Interact()
    {
        throw new System.NotImplementedException();
    }




}
