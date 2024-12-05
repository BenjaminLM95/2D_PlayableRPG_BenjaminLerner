using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using JetBrains.Annotations;

public class Enemy_Script : Actor
{

    public Tilemap myTilemap;
    public TileBase enemy;
    public int enemy_x;
    public int enemy_y;
    public MapManagment mmScript;
    public GameObject m;
    public Player_Script plScript;
    public GameObject p; 
    private int plX;
    private int plY;
    private int mvX;
    private int mvY;
    public bool enemyTurn;
    public int enemyMvs;
    public int enemyMvsMax = 2;
    public TextMeshProUGUI enTurnTx;
    public TextMeshProUGUI statsUpdate;
    public System.Random rnd = new System.Random();
    public int enemyHP;
    public int attack;
    public int eLastCheckedHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemy_x = 26;
        enemy_y = 14;
        enemyHP = 300;
        attack = 75; 
        enemyMvs = enemyMvsMax; 
        mmScript = m.GetComponent<MapManagment>();
        plScript = p.GetComponent<Player_Script>();
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);
        enemyTurn = false; 
        playerDir();
        healthSystem.setMaxHP(300);
        healthSystem.resetGame(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (eLastCheckedHealth != healthSystem.hp )
        {
            eLastCheckedHealth = healthSystem.hp;            
        }

        

        if (enemyTurn && healthSystem.hp > 0)
        {
            //Enemy only moves when its hp is greater than 0
            enTurnTx.gameObject.SetActive(true);
            Invoke("enemyMove", 1f);
            enemyMvs--;
            System.Threading.Thread.Sleep(500);

        }

        if(enemyMvs <= 0) 
        {
            enemyMvs = enemyMvsMax;
            enemyTurn = false;
            Invoke("playerTurn", 2f); 
        }

        statsUpdate.text = "Enemy HP: " + healthSystem.hp;

        if(healthSystem.hp <= 0) 
        {
            myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), null);
            plScript.youWin = true;
            plScript.winOrLose.gameObject.SetActive(true);
            plScript.winOrLose.text = "You Win!!";

        }


    }

    public void checkForPlayerPosition() 
    {
        //Get the position of the player
        plX = plScript.player_x;
        plY = plScript.player_y; 

    }
    public void enemyMove() 
    {
        checkForPlayerPosition();
        playerDir();       
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), null);
        enemy_x += mvX;
        enemy_y += mvY;
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);     
        
    }
    public void playerDir()
    {
        checkForPlayerPosition();
        int rNum = rnd.Next(0, 2);

        //Porcentages of each direction the enemy will move. Only is necessary to know the first two
        int LeftMv;
        int noXMv;
        //int RightMv;
        int UpMv;
        int noYMv;
        //int DownMv;

        int rndX = mmScript.randomNumber(0, 100);
        int rndY = mmScript.randomNumber(0, 100);

        //Getting by random if the enemy will move right or left, having more chance to go to the position the player is

        if (enemy_x > plX)
        {
            LeftMv = 80;
            noXMv = 10;
            //RightMv = 10;
        }
        else if (enemy_x == plX)
        {
            LeftMv = 10;
            noXMv = 80;
            //RightMv = 10;
        }
        else  
        {
            LeftMv = 10;
            noXMv = 10;
            //RightMv = 80;
        }
        

        if(rndX < LeftMv) 
        {
            mvX = -1;
        }
        else if(rndX < LeftMv + noXMv) 
        {
            mvX = 0; 
        }
        else if(rndX < 100) 
        {
            mvX = 1; 
        }
        else
            mvX = 0;

        //Getting by random if the enemy will move up or down, having more chance to go to the position the player is

        if (enemy_y > plY)
        {
            UpMv =80;
            noYMv = 10;
            //DownMv = 10;
        }
        else if (enemy_y == plY)
        {
            UpMv = 10;
            noYMv = 80;
            //DownMv = 10;
        }
        else
        {
            UpMv = 10;
            noYMv = 10;
            //DownMv = 80; 
        }

        if (rndY < UpMv)
        {
            mvY = -1;
        }
        else if (rndY < UpMv + noYMv)
        {
            mvY = 0;
        }
        else if (rndY < 100)
        {
            mvY = 1;
        }
        else
            mvY = 0;
        

        // Checking for collisions

        if (mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '#' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '@' ||
            mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'D' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'B' ||
            mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'O' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'o')
        {
            mvX = 0;
            if (mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '#' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '@' ||
                mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'D' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'B' ||
                mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'O' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'o')
                mvY = 0;

        }

        if (mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '#' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '@' || 
            mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'D' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'B' ||
            mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'O' || mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == 'o')
        {
            mvY = 0;
            if (mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '#' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '@' || 
                mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'D' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'B' ||
                 mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'O' || mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == 'o')
                mvX = 0;

        }

        if (is_a_borderWall(mmScript.multidimensionalMap[enemy_x + mvX, enemy_y + mvY])) 
        {
            mvX = 0;
            mvY = 0; 
        }

        // This doesn't let the enemy to make two moves as a one  (When he has for example (+1, -1) as a movement 

        if ((Mathf.Abs(mvX) == 1 && Mathf.Abs(mvY) == 1))
        {
            if (rNum == 0)
                mvY = 0;
            else
                mvX = 0; 

                       

        }

        //The enemy attack the player when moves to the player's position
        if((enemy_x + mvX == plX) && (enemy_y + mvY == plY)) 
        {
            mvX = 0;
            mvY = 0;
            plScript.healthSystem.TakeDamage(attack);
            plScript.checkForLife();
            plScript.beingAttack(); 
        }


       
    }

    public void playerTurn() 
    {
        plScript.myTurn = true;
        enTurnTx.gameObject.SetActive(false);
    }


    public bool is_a_borderWall(char c)
    {
        bool result;

        if (c == 't' || c == 'y' || c == 'u' || c == 'g' || c == 'h' || c == 'j' || c == 'n' || c == 'm')
            result = true;
        else
            result = false;

        return result;
    }
}
