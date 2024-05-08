using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("First");
        if (other.tag == "Player")
        {
            Debug.Log("Load castle");
            SceneManager.LoadScene("Castle");
        }
    }
}
