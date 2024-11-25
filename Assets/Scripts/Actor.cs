using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();
    int lastCheckingForDeath = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.resetGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.hp != lastCheckingForDeath)
        {
            lastCheckingForDeath = healthSystem.hp;
            if (healthSystem.hp <= 0)
            {
                if (healthSystem.lives > 0)
                {
                    healthSystem.Revive();
                }
                else
                {
                    
                }

            }

        }



    }
}
