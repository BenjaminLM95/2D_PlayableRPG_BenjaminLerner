using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Script : Actor
{
    public Tilemap myTilemap;
    public TileBase player;
    public int player_x;
    public int player_y;
    public MapManagment mmScript;
    public GameObject m;
    public Enemy_Script enScript;
    public GameObject e;
    public bool myTurn = true;
    public int movCount;
    public int movCountMax = 2;
    public TextMeshProUGUI turnTx;
    public TextMeshProUGUI statsUpdate;
    public int attack;
    public int iAttack; 
    public int money = 0; 

    public int lastCheckedHealth;
    public int lastCheckedXp;
    public int lastCheckedLevel;    
    public int checkLives;

    // Start is called before the first frame update
    void Start()
    {
        player_x = 3;
        player_y = 3;
        attack = 25;
        iAttack = 25; 
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
        mmScript = m.GetComponent<MapManagment>();
        enScript = e.GetComponent<Enemy_Script>();
        movCount = movCountMax;
        healthSystem.resetGame(); 
    }

    // Update is called once per frame
    void Update()
    {       
        if (lastCheckedHealth != healthSystem.hp || lastCheckedLevel != healthSystem.level || lastCheckedXp != healthSystem.xp)
        {

            lastCheckedHealth = healthSystem.hp;
            lastCheckedLevel = healthSystem.level;
            lastCheckedXp = healthSystem.xp;    
            
        }

        attack = iAttack + (money / 2); 

        statsUpdate.text = healthSystem.ShowHUD() + " Attack: " + attack + " Money: " + money;


        if (myTurn)
        {
            turnTx.gameObject.SetActive(true);
            turnTx.text = "Is your Turn. Movements left: " + movCount;

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                // The player moves down
                if (player_x == enScript.enemy_x && player_y - 1 == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y - 1, '#', mmScript.multidimensionalMap) || checkForCollision(player_x, player_y - 1, '@', mmScript.multidimensionalMap)
                    || checkForCollision(player_x, player_y - 1, 'D', mmScript.multidimensionalMap) || checkForCollision(player_x, player_y - 1, 'B', mmScript.multidimensionalMap)
                    || checkForCollision(player_x, player_y - 1, 'W', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else if (checkForCollision(player_x, player_y - 1, 'O', mmScript.multidimensionalMap))
                {
                    openChest(player_x, player_y - 1);
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y - 1, '$', mmScript.multidimensionalMap))
                {
                    money++;
                    consumeItem(player_x, player_y - 1);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y--;
                    restartTileMap();
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y - 1, 'S', mmScript.multidimensionalMap))
                {
                    money += 5;  
                    consumeItem(player_x, player_y);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y--;
                    restartTileMap();
                    movCount--;
                }
                else if (is_a_borderWall(mmScript.multidimensionalMap[player_x, player_y - 1]))
                    Debug.Log("Border");
                else
                {
                    movCount--;
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y--;
                    restartTileMap();

                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // The player moves up
                if (player_x == enScript.enemy_x && player_y + 1 == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y + 1, '#', mmScript.multidimensionalMap) || checkForCollision(player_x, player_y + 1, '@', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x, player_y + 1, 'B', mmScript.multidimensionalMap) || checkForCollision(player_x, player_y + 1, 'D', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x, player_y + 1, 'w', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else if (checkForCollision(player_x, player_y + 1, 'O', mmScript.multidimensionalMap))
                {
                    openChest(player_x, player_y + 1);
                    movCount--;                    
                }
                else if (checkForCollision(player_x, player_y + 1, '$', mmScript.multidimensionalMap)) 
                {
                    money++;
                    consumeItem(player_x, player_y + 1);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y++;
                    restartTileMap();
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y + 1, 'S', mmScript.multidimensionalMap))
                {
                    money += 5;
                    consumeItem(player_x, player_y + 1);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y++;
                    restartTileMap();
                    movCount--;
                }
                else if (is_a_borderWall(mmScript.multidimensionalMap[player_x, player_y + 1]))
                    Debug.Log("Border");
                else
                {
                    movCount--;
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y++;
                    restartTileMap();                    
                    
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (player_x - 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    movCount--;
                }
                else if (checkForCollision(player_x - 1, player_y, '#', mmScript.multidimensionalMap) || checkForCollision(player_x - 1, player_y, '@', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x - 1, player_y, 'D', mmScript.multidimensionalMap) || checkForCollision(player_x - 1, player_y, 'B', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x - 1, player_y, 'w', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else if (checkForCollision(player_x - 1, player_y, 'O', mmScript.multidimensionalMap))
                {
                    openChest(player_x - 1, player_y);
                    movCount--;                    
                }
                else if (checkForCollision(player_x - 1, player_y, '$', mmScript.multidimensionalMap))
                {
                    money++;
                    consumeItem(player_x - 1, player_y);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x--;
                    restartTileMap();
                    movCount--;
                }
                else if (checkForCollision(player_x - 1, player_y, 'S', mmScript.multidimensionalMap))
                {
                    money += 5;
                    consumeItem(player_x - 1, player_y);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x--;
                    restartTileMap();
                    movCount--;
                }
                else if (is_a_borderWall(mmScript.multidimensionalMap[player_x - 1, player_y]))
                    Debug.Log("Border");
                else
                {
                    movCount--;
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x--;
                    restartTileMap();
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (player_x + 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    movCount--;
                }
                else if (checkForCollision(player_x + 1, player_y, '#', mmScript.multidimensionalMap) || checkForCollision(player_x + 1, player_y, '@', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x + 1, player_y, 'D', mmScript.multidimensionalMap) || checkForCollision(player_x + 1, player_y, 'B', mmScript.multidimensionalMap) ||
                    checkForCollision(player_x + 1, player_y, 'w', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else if (checkForCollision(player_x + 1, player_y, 'O', mmScript.multidimensionalMap))
                {
                    openChest(player_x + 1, player_y);
                    movCount--;                    
                }
                else if (checkForCollision(player_x + 1, player_y, '$', mmScript.multidimensionalMap))
                {
                    money++;
                    consumeItem(player_x + 1, player_y);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x++;
                    restartTileMap();
                    movCount--;
                }
                else if (checkForCollision(player_x + 1, player_y, 'S', mmScript.multidimensionalMap))
                {
                    money += 5;
                    consumeItem(player_x + 1, player_y);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x++;
                    restartTileMap();
                    movCount--;
                }
                else if (is_a_borderWall(mmScript.multidimensionalMap[player_x + 1, player_y]))
                    Debug.Log("Border");
                else
                {
                    movCount--;
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x++;
                    restartTileMap();                    
                    
                }
            }

            if (movCount <= 0)
            {
                myTurn = false;
                Invoke("DelayedSetEnemyTurn", 0.5f);
            }

        }               

    }

    public void DelayedSetEnemyTurn()
    {
        turnTx.gameObject.SetActive(false);
        movCount = movCountMax;
        enScript.enemyTurn = true;        
    }

    void restartTileMap() 
    {        
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
       
    }

    public bool checkForCollision(int x, int y, char colChar, char[,] map)
    {
        //Check if in a specific position, a specific characte exists on it
        if (map[x, y] == colChar)
        {
            return true;
        }
        else
        {
            return false;
        }

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

    private void openChest(int x, int y) 
    {
        myTilemap.SetTile(new Vector3Int(x, y, 0), null);
        myTilemap.SetTile(new Vector3Int(x, y, 0), mmScript.openChest);
    }

    private void consumeItem(int x, int y) 
    {
        myTilemap.SetTile(new Vector3Int(x, y, 0), null);
        myTilemap.SetTile(new Vector3Int(x, y, 0), mmScript.field);
    }


}
