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
    public int iAttack = 20; 
    public int money = 0; 

    public int lastCheckedHealth;
    public int lastCheckedXp;
    public int lastCheckedLevel;    
    public int checkLives;
    public TextMeshProUGUI chestResult;
    public TextMeshProUGUI winOrLose; 

    public bool youWin = false;
    public bool youLose = false; 

    // Start is called before the first frame update
    void Start()
    {
        player_x = 3;
        player_y = 3;
        attack = iAttack;  
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
        mmScript = m.GetComponent<MapManagment>();
        enScript = e.GetComponent<Enemy_Script>();
        movCount = movCountMax;
        //healthSystem.resetGame(); 
        healthSystem.setHP(50);
        healthSystem.setLevel(1); 
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

        attack = iAttack + (money / 2) + (3 * healthSystem.level); 

        statsUpdate.text = healthSystem.ShowHUD() + " Attack: " + attack + " Money: " + money;


        if (myTurn)
        {
            turnTx.gameObject.SetActive(true);
            turnTx.text = "Is your Turn. Movements left: " + movCount;

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && (!youLose || !youWin))
            {
                // The player moves down
                if (player_x == enScript.enemy_x && player_y - 1 == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    youAttack();
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
                    consumeItem(player_x, player_y - 1);
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y--;
                    restartTileMap();
                    movCount--;
                }
                else if (checkForCollision(player_x, player_y - 1, 'f', mmScript.multidimensionalMap))
                {
                    healthSystem.Recover(30);
                    consumeItem(player_x, player_y - 1);
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

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && (!youLose || !youWin))
            {
                // The player moves up
                if (player_x == enScript.enemy_x && player_y + 1 == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    youAttack();
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
                else if (checkForCollision(player_x, player_y + 1, 'f', mmScript.multidimensionalMap))
                {
                    healthSystem.Recover(30);
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

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && (!youLose || !youWin))
            {
                if (player_x - 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    youAttack();
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
                else if (checkForCollision(player_x - 1, player_y, 'f', mmScript.multidimensionalMap))
                {
                    healthSystem.Recover(30);
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

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && (!youLose || !youWin))
            {
                if (player_x + 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                    enScript.healthSystem.TakeDamage(attack);
                    youAttack(); 
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
                else if (checkForCollision(player_x + 1, player_y, 'f', mmScript.multidimensionalMap))
                {
                    healthSystem.Recover(30);
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

        if (healthSystem.hp <= 0 && healthSystem.lives <= 0)
        {
            youLose = true;
            winOrLose.gameObject.SetActive(true);
            winOrLose.text = "You Lose!!"; 
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
        int rnd = mmScript.randomNumber(0, 100); 

        chestResult.gameObject.SetActive(true);

        if(rnd < 30) 
        {
            money++;
            chestResult.text = "You obtained 1 coin."; 
        }
        else if(rnd < 35) 
        {
            healthSystem.IncreaseXP(20);
            chestResult.text = "Your experience has increased a little";
        }
        else if(rnd < 50)
        {
            healthSystem.IncreaseXP(50);
            chestResult.text = "Your experience has increased"; 
        }
        else if (rnd < 60) 
        {
            money += 5;
            chestResult.text = "You obtained 5 coin.";
        }
        else if (rnd < 70) 
        {
            iAttack += 5;
            chestResult.text = "Your attack increased by 5";
        }
        else if (rnd < 95) 
        {
            healthSystem.Recover(30);
            chestResult.text = "You recovered 30 hp";
        }
        else if (rnd < 99) 
        {
            healthSystem.Recover(100);
            chestResult.text = "You recovered 100 hp";
        }
        else if (rnd < 100) 
        {
            healthSystem.Recover(healthSystem.hpMax);
            chestResult.text = "You fully restored your hp";
        }

        Invoke("desactivateChestResult", 5f); 
    
    }

    private void consumeItem(int x, int y) 
    {
        myTilemap.SetTile(new Vector3Int(x, y, 0), null);
        myTilemap.SetTile(new Vector3Int(x, y, 0), mmScript.field);
    }

    private void desactivateChestResult() 
    {
        chestResult.gameObject.SetActive(false);
    }

    public void beingAttack() 
    {
        chestResult.gameObject.SetActive(true);
        chestResult.text = "You have been attacked";
        Invoke("desactivateChestResult", 3f);

    }

    public void youAttack()
    {
        chestResult.gameObject.SetActive(true);
        chestResult.text = "You attacked the enemy";
        Invoke("desactivateChestResult", 3f);
    }

}
