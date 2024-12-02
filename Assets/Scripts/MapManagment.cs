using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using TMPro;

public class MapManagment : MonoBehaviour
{
    public Tilemap myTilemap;
    public TileBase wall;
    public TileBase wall2;
    public TileBase wall3; 
    public TileBase block;
    public TileBase field;
    public TileBase player;
    public TileBase field2;
    public TileBase chest;
    public TileBase openChest;
    public TileBase water;
    public TileBase food;
    public TileBase diamond;
    public TileBase gold; 
    string mString;
    public char[,] multidimensionalMap = new char[30, 20];

    // '#' for walls, '@' for walls2, 'D' for wall3, 'B' for block, '*' for field, '+' for field2 'O' for chest, 'o' for open chest
    // 'w' for water, 'S' for diamond, '$' for gold, 'f' food, 'P' for player, 'E' for enemy
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
        // '#' for walls, '@' for walls2, 'D' for wall3, 'B' for block, '*' for field, '+' for field2 'O' for chest, 'o' for open chest
        // 'w' for water, 'S' for diamond, '$' for gold, 'f' food, 'P' for player, 'E' for enemy
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
                else if ((j == height/3 - 3 || j == 2 * height / 3 + 3) && (i > width/5 - 1 && i < 4 * width / 5 + 1)) 
                {
                    mapMatrix[i, j] = '@'; 
                }
                else if ((j > height / 3 - 3 && j < 2 * height / 3 + 3) && (i > width / 5 - 1 && i < 4 * width / 5 + 1)) 
                {
                    mapMatrix[i, j] = GenerateString();
                }
                else
                    mapMatrix[i, j] = '*';     
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
        // '#' for walls, '@' for walls2, 'D' for wall3, 'B' for block, '*' for field, '&' for field2 'O' for chest, 'o' for open chest
        // 'w' for water, 'S' for diamond, '$' for gold, 'f' food, 'P' for player, 'E' for enemy
        for (int i = 0; i < lines.Length - 1; i++)
        {

            for (var j = 0; j < lines[i].Length - 1; j++)
            {
                if (lines[i][j] == '#') // wall
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), wall);
                    
                }
                else if (lines[i][j] == '@')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), wall2);
                }
                else if (lines[i][j] == 'D')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), wall3);
                }
                else if (lines[i][j] == '*')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field);
                }
                else if (lines[i][j] == 'B')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), block);
                }
                else if (lines[i][j] == '&')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field2);
                }
                else if (lines[i][j] == 'O')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), chest);
                }
                else if (lines[i][j] == 'w')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), water);
                }
                else if (lines[i][j] == '$')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), gold);
                }
                else if (lines[i][j] == 'S')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), diamond);
                }
                else if (lines[i][j] == 'P')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), field);
                }
                else if (lines[i][j] == 'f')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), food);
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
        // '#' for walls, '@' for walls2, 'D' for wall3, 'B' for block, '*' for field, '&' for field2 'O' for chest, 'o' for open chest
        // 'w' for water, 'S' for diamond, '$' for gold, 'f' food, 'P' for player, 'E' for enemy
        var lines = sMap.Split('\n');

        for (int j = 0; j < daMap.GetLength(1); j++)
        {

            for (int i = 0; i < daMap.GetLength(0); i++)
            {
                if (lines[j][i] == '#') // wall
                {
                    daMap[i, j] = '#';
                }
                else if (lines[j][i] == '@') // wall2
                {
                    daMap[i, j] = '@';
                }
                else if (lines[j][i] == 'D') // Wall3
                {
                    daMap[i, j] = 'D';
                }
                else if (lines[j][i] == 'B') //Block
                {
                    daMap[i, j] = 'B';
                }
                else if (lines[j][i] ==  '*')  // field
                {
                    daMap[i, j] = '*';
                }
                else if (lines[j][i] == '&')  //Field2
                {
                    daMap[i, j] = '&';
                }
                else if (lines[j][i] == 'O') // Chest
                {
                    daMap[i, j] = 'O';
                }
                else if (lines[j][i] == 'w') //Water
                {
                    daMap[i, j] = 'w';
                }
                else if (lines[j][i] == 'S') //Diamond
                {
                    daMap[i, j] = 'S';
                }
                else if (lines[j][i] == '$') //Gold
                {
                    daMap[i, j] = '$';
                }
                else if (lines[j][i] == 'f') //Food
                {
                    daMap[i, j] = 'f';
                }
                else 
                {
                    daMap[i, j] = '*';
                }
            }
        }
    }

    public static int randomNumber(int a, int b)
    {
        // Generate a random number to later get a random character
        System.Random random = new System.Random();
        int rslt = random.Next(a, b);
        return rslt;
    }

    char GenerateString()
    {
        // Generate the char at random
        // '#' for walls, '@' for walls2, 'D' for wall3, 'B' for block, '*' for field, '&' for field2 'O' for chest, 'o' for open chest
        // 'w' for water, 'S' for diamond, '$' for gold, 'f' food, 'P' for player, 'E' for enemy
        char charElement;
        int typeOfString = randomNumber(0, 100);

        if (typeOfString < 34)
        {
            charElement = '*';
        }
        else if (typeOfString < 68)
        {
            charElement = '&';
        }
        else if (typeOfString < 72)
        {
            charElement = '@';
        }
        else if (typeOfString < 76)
        {
            charElement = 'D';
           
        }
        else if (typeOfString < 80)
        {
            charElement = 'B';
        }
        else if (typeOfString < 84)
        {
            charElement = 'O';
        }
        else if (typeOfString < 88)
        {
            charElement = 'w';
        }
        else if (typeOfString < 92)
        {
            charElement = 'S';
        }
        else if (typeOfString < 96)
        {
            charElement = '$';
        }
        else if (typeOfString < 100)
        {
            charElement = 'f';
        }
        else
        {
            charElement = '*';
        }


        return charElement;
    }

}
