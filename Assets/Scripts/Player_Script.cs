using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        player_x = 3;
        player_y = 3;
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
        mmScript = m.GetComponent<MapManagment>();
        enScript = e.GetComponent<Enemy_Script>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // The player moves down
            if(player_x == enScript.enemy_x && player_y - 1 == enScript.enemy_y) 
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
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow )) 
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
            }
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
