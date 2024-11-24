using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy_Script : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        enemy_x = 16;
        enemy_y = 16;
        enemyMvs = enemyMvsMax; 
        mmScript = m.GetComponent<MapManagment>();
        plScript = p.GetComponent<Player_Script>();
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);
        enemyTurn = false; 
        playerDir(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("X: " + enemy_x + " Y: " + enemy_y); 

        if (enemyTurn)
        {
            enTurnTx.gameObject.SetActive(true);            
            checkForPlayerPosition();
            playerDir();
            System.Threading.Thread.Sleep(1000);
            myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), null);
            enemy_x += mvX;
            enemy_y += mvY;
            myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);            
            enemyMvs--;

        }

        if(enemyMvs <= 0) 
        {
            enemyTurn = false;
            plScript.myTurn = true;          
            enTurnTx.gameObject.SetActive(false);
            enemyMvs = enemyMvsMax; 
        }


    }

    public void checkForPlayerPosition() 
    {
        plX = plScript.player_x;
        plY = plScript.player_y; 

    }

    public void playerDir()
    {
        checkForPlayerPosition();

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





    }

}
