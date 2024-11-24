using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using TMPro;

public class MapManagment : MonoBehaviour
{
    public Tilemap myTilemap;
    public TileBase wall;
    public TileBase block;
    public TileBase field;
    public TileBase player;
    string mString;
    public char[,] multidimensionalMap = new char[30, 20];


    // Start is called before the first frame update
    void Start()
    {
        mString = GenerateMapString(30, 20);
        ConvertToMap(mString, multidimensionalMap);
        ConvertMapToTilemap(mString);
        //stringMapText.text = mString;



    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public string GenerateMapString(int width, int height)
    {
        // '#' for walls, '@' for doors, '*' for field '%' for grass, '&' for a tree
        // Creating a bidimensional array for the map to later convert it into a string

        char[,] mapMatrix = new char[width, height];

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    mapMatrix[i, j] = '#';  //1st rule: The borders should be walls
                else if (i == width - 3 && j == 3)
                {
                    //Where the player is
                    mapMatrix[i, j] = 'P';

                }
                else if ((j == height/3  || j == 2 * height / 3) && (i > width/5 && i < 4 * width / 5)) 
                {
                    mapMatrix[i, j] = '#'; 
                }
                else
                    mapMatrix[i, j] = '*';     //mapMatrix[i, j] = GenerateString();
            }
        }

        return convertMapToString(width, height, mapMatrix);

    }

    string convertMapToString(int x, int y, char[,] smap)
    {
        string result = "";

        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                result += smap[i, j];
            }
            result += Environment.NewLine;
        }

        return result;
    }

    private void ConvertMapToTilemap(string mapData)
    {
        // Split the char (string) to set it into the 2d array
        var lines = mapData.Split('\n');
        // '#' for walls, '@' for doors, '*' for field '%' for grass, '$' for grass2, '&' for a tree
        for (int i = 0; i < lines.Length - 1; i++)
        {

            for (var j = 0; j < lines[i].Length - 1; j++)
            {
                if (lines[i][j] == '#') // wall
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), wall);
                    
                }
                else if (lines[i][j] == '*')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field);
                }
                else if (lines[i][j] == '&')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), block);
                }
                else if (lines[i][j] == 'P')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field);
                }
                else
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field);
                     
                }
            }
        }
    }


    private void ConvertToMap(string sMap, char[,] daMap)
    {
        // Split the char (string) from a specific 2d array
        // '#' for walls, '@' for doors, '*' for field '%' for grass, '$' for grass2, '&' for a tree, 'P' for players, '!' or '^' for magic doors
        var lines = sMap.Split('\n');

        for (int j = 0; j < daMap.GetLength(1); j++)
        {

            for (int i = 0; i < daMap.GetLength(0); i++)
            {
                if (lines[j][i] == '#') // wall
                {
                    daMap[i, j] = '#';
                }
                else if (lines[j][i] ==  '*')  // door
                {
                    daMap[i, j] = '*';
                }
                else if (lines[j][i] == '&') 
                {
                    daMap[i, j] = '&';
                }
            }
        }
    }
}
