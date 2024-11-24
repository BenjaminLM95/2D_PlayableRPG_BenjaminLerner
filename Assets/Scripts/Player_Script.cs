using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Script : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        player_x = 3;
        player_y = 3;
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
        mmScript = m.GetComponent<MapManagment>();
        enScript = e.GetComponent<Enemy_Script>();
        movCount = movCountMax; 
    }

    // Update is called once per frame
    void Update()
    {

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
                }
                else if (checkForCollision(player_x, player_y - 1, '#', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else
                {
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y--;
                    restartTileMap();
                    movCount--;
                    Debug.Log("MovReduce: " + movCount);
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                // The player moves up
                if (player_x == enScript.enemy_x && player_y + 1 == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                }
                else if (checkForCollision(player_x, player_y + 1, '#', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else
                {
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_y++;
                    restartTileMap();
                    movCount--;
                    Debug.Log("MovReduce: " + movCount);
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (player_x - 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                }
                else if (checkForCollision(player_x - 1, player_y, '#', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else
                {
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x--;
                    restartTileMap();
                    movCount--;
                    Debug.Log("MovReduce: " + movCount); 
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (player_x + 1 == enScript.enemy_x && player_y == enScript.enemy_y)
                {
                    Debug.Log("Enemy");
                }
                else if (checkForCollision(player_x + 1, player_y, '#', mmScript.multidimensionalMap))
                {
                    Debug.Log("Colision");

                }
                else
                {
                    myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
                    player_x++;
                    restartTileMap();
                    movCount--;
                    Debug.Log("MovReduce: " + movCount);
                }
            }            
        }

        if (movCount <= 0)
        {
            myTurn = false;
            enScript.enemyTurn = true;
            Debug.Log("Enemy Turn"); 
            turnTx.gameObject.SetActive(false);           
            movCount = movCountMax; 
        }

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
}
