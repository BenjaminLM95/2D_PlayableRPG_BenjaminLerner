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
        enemy_x = 16;
        enemy_y = 14;
        enemyHP = 5;
        attack = 3; 
        enemyMvs = enemyMvsMax; 
        mmScript = m.GetComponent<MapManagment>();
        plScript = p.GetComponent<Player_Script>();
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);
        enemyTurn = false; 
        playerDir();
        healthSystem.setMaxHP(10);
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
        }
    }

    public void checkForPlayerPosition() 
    {
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

        if (enemy_x > plX)
        {
            mvX = -1;
        }
        else if (enemy_x == plX)
        {
            mvX = 0;
        }
        else
            mvX = 1;
                

        if (enemy_y > plY)
        {
            mvY = -1;
        }
        else if (enemy_y == plY)
            {
            mvY = 0;
        }
        else
        {
            mvY = 1; 
        }

                
        if(mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '#')
        {
            mvX = 0;
            if (mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '#')
                mvY = 0;

        }

        if (mmScript.multidimensionalMap[enemy_x, enemy_y + mvY] == '#')
        {
            mvY = 0;
            if (mmScript.multidimensionalMap[enemy_x + mvX, enemy_y] == '#')
                mvX = 0;

        }

        if ((Mathf.Abs(mvX) == 1 && Mathf.Abs(mvY) == 1))
        {
            if (rNum == 0)
                mvY = 0;
            else
                mvX = 0; 

                       

        }

        if((enemy_x + mvX == plX) && (enemy_y + mvY == plY)) 
        {
            mvX = 0;
            mvY = 0;
            plScript.healthSystem.TakeDamage(attack);
            plScript.checkForLife(); 
        }

       
    }

    public void playerTurn() 
    {
        plScript.myTurn = true;
        enTurnTx.gameObject.SetActive(false);
    }

}
