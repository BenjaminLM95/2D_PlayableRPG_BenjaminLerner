using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Script : MonoBehaviour
{
    public Tilemap myTilemap;
    public TileBase player;
    int player_x;
    int player_y;
    //public MapManagment mmScript; 

    // Start is called before the first frame update
    void Start()
    {
        player_x = 3;
        player_y = 3;
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
        //mmScript = GameObject.FindObjectOfType<MapManagment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // The player moves down
            myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
            player_y--;
            restartTileMap();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // The player moves up
            myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
            player_y++;
            restartTileMap();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
            player_x--;
            restartTileMap();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), null);
            player_x++;
            restartTileMap();
        }
    }

    void restartTileMap() 
    {        
        myTilemap.SetTile(new Vector3Int(player_x, player_y, 1), player);
       
    }
}
