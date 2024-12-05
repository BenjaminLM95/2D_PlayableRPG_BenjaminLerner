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
        //The game start with the default values
        healthSystem.resetGame(); 
    }

       
    public void checkForLife() 
    {
        //Check for the hp, and revives if the player or enemy has more than 1 life (The enemy only has 1 life)
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
