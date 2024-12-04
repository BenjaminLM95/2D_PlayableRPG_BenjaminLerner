using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FightLevel");

    }

    public void goToMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void goToInstructions() 
    {
        SceneManager.LoadScene("Instructions"); 
    }
}
