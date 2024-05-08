using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject helperUI;

    private bool isPlayerNear = false;

    private void Awake()
    {
        helperUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerNear)
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            helperUI.SetActive(true);
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            helperUI.SetActive(false);
            isPlayerNear = false;
        }
    }
    public void Interact()
    {
        SceneManager.LoadScene("Boss");
    }




}
