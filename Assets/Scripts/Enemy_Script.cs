using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        enemy_x = 16;
        enemy_y = 16; 

        mmScript = m.GetComponent<MapManagment>();
        plScript = p.GetComponent<Player_Script>();
        myTilemap.SetTile(new Vector3Int(enemy_x, enemy_y, 1), enemy);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void checkForPlayerPosition() 
    {
        plX = plScript.player_x;
        plY = plScript.player_y; 

    }

}
