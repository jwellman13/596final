using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeathUI : MonoBehaviour
{
    [SerializeField] Image[] hearts;

    private PlayerController player;
    private int maxHearts = 10;
    private int numHearts;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        numHearts = maxHearts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        numHearts = player.GetHealth();
        for (int i = 0; i < maxHearts; i++)
        {
            if (i >= numHearts)
            {
                hearts[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    
}
