using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthSystem
{
    //Variables
    public int hpMax = 500; 
    public int hp = 0;
    public string healthStatus;
    public int lives = 2;
    // this variables for the time being Im not going to use it
    public int xp;
    public int level; 

    public void TakeDamage(int damage) 
    {
        if (damage < 0)
            damage = 0; 

        if(damage >= hp) 
        {
            hp = 0;
        }
        else
        {
            hp = hp - damage; 
        }
        Debug.Log("Damage"); 
        
    }

    public void Recover(int heal) 
    {
        // Implement healing logic
        //Can't be negative
        if (heal < 0)
            heal = 0;

        //Can't exceed the max hp
        if (hp + heal > hpMax)
            hp = hpMax;
        else
            hp = hp + heal; 
               
    }

    public string ShowHUD()
    {
        // Implement HUD display
        //healthStatus is left
        healthStatus = GetStatusHealth(hp);
        return "Lives: " + lives.ToString() + "  Health: " + hp.ToString() + " (" + healthStatus + ") " + " Exp: " + xp + " Level: " + level.ToString();
    }


    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
        xp = xp + exp;

        if (xp >= 100)
        {
            xp = xp - 100;
            if (level < 99)
            {
                level++;
            }

        }
    }

    public string GetStatusHealth(int health)
    {
        string status;

        if (health <= 100)
        {
            status = "Imminent Danger";
        }
        else if (health > 100 && health <= 200)
        {
            status = "Badly Hurt";
        }
        else if (health > 200 && health <= 400)
        {
            status = "Hurt";
        }
        else if (health > 400 && health <= 499)
        {
            status = "Heathy";
        }
        else
        {
            status = "Perfect Health";
        }

        return status;
    }

    public void Revive()
    {
        // Implement revive logic

        lives--;
        if (lives > 0)
        {
            resetGame();
        }

    }
    public void resetGame() 
    {
        hp = hpMax; 
    }

    public void setMaxHP(int maxHp) 
    {
        hpMax = maxHp;        
    }

    public void setHP(int amount) 
    {
        if(amount <= 0 || amount > hpMax) 
        {
            return;
        }
        else 
        {
            hp = amount; 
        }
    }

}
