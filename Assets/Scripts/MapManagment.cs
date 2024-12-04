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

    public TileBase topLeftCorner;
    public TileBase topRightCorner;
    public TileBase topWalls;
    public TileBase lowerLeftCorner;
    public TileBase lowerRightCorner;
    public TileBase lowerWalls;
    public TileBase leftWalls;
    public TileBase rightWalls;

    public TextMeshProUGUI textMap;

    // 't' for TLC, 'y' for TW, 'u' for TRC 'n' for Left Walls
    // 'g' for LLC, 'h' for LW, 'j' for LRC 'm' for Right Walls

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
        textMap.text = mString;



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
                if (i == 0 && j == height - 1)
                    mapMatrix[i, j] = 't';
                else if (i == 0 && j == 0)
                    mapMatrix[i, j] = 'g';
                else if (i == width - 1 && j == 0)
                    mapMatrix[i, j] = 'j';
                else if (i == width - 1 && j == height - 1)
                    mapMatrix[i, j] = 'u';
                else if (i == 0 && j > 0 && j < height - 1)
                    mapMatrix[i, j] = 'n';
                else if (i == width - 1 && j > 0 && j < height - 1)
                    mapMatrix[i, j] = 'm';
                else if (i > 0 && i < width - 1 && j == 0)
                    mapMatrix[i, j] = 'h';
                else if (i > 0 && j < width - 1 && j == height - 1)
                    mapMatrix[i, j] = 'y';
                else if (i == width - 3 && j == 3)
                {
                    //Where the player is
                    mapMatrix[i, j] = 'P';

                }
                else if ((j == height / 3 - 3 || j == 2 * height / 3 + 3) && (i > width / 5 - 1 && i < 4 * width / 5 + 1))
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
        // 't' for TLC, 'y' for TW, 'u' for TRC 'n' for Left Walls
        // 'g' for LLC, 'h' for LW, 'j' for LRC 'm' for Right Walls

        for (int i = 0; i < lines.Length - 1; i++)
        {

            for (var j = 0; j < lines[i].Length - 1; j++)
            {
                if (lines[i][j] == '#') // wall
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), wall);
                    
                }
                else if (lines[i][j] == 't')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), topLeftCorner);
                }
                else if (lines[i][j] == 'y')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), topWalls);
                }
                else if (lines[i][j] == 'u')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), topRightCorner);
                }
                else if (lines[i][j] == 'g')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), lowerLeftCorner);
                }
                else if (lines[i][j] == 'h')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), lowerWalls);
                }
                else if (lines[i][j] == 'j')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), lowerRightCorner);
                }
                else if (lines[i][j] == 'n')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), leftWalls);
                }
                else if (lines[i][j] == 'm')
                {
                    myTilemap.SetTile(new Vector3Int(j, i, 0), rightWalls);
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
        // 't' for TLC, 'y' for TW, 'u' for TRC 'n' for Left Walls
        // 'g' for LLC, 'h' for LW, 'j' for LRC 'm' for Right Walls
        var lines = sMap.Split('\n');

        for (int j = 0; j < daMap.GetLength(1); j++)
        {

            for (int i = 0; i < daMap.GetLength(0); i++)
            {
                if (lines[j][i] == '#') // wall
                {
                    daMap[i, j] = '#';
                }
                else if (lines[j][i] == 't') // 
                {
                    daMap[i, j] = 't';
                }
                else if (lines[j][i] == 'y') // 
                {
                    daMap[i, j] = 'y';
                }
                else if (lines[j][i] == 'u') // 
                {
                    daMap[i, j] = 'u';
                }
                else if (lines[j][i] == 'n') // 
                {
                    daMap[i, j] = 'n';
                }
                else if (lines[j][i] == 'g') // 
                {
                    daMap[i, j] = 'g';
                }
                else if (lines[j][i] == 'h') // 
                {
                    daMap[i, j] = 'h';
                }
                else if (lines[j][i] == 'j') // 
                {
                    daMap[i, j] = 'j';
                }
                else if (lines[j][i] == 'm') // 
                {
                    daMap[i, j] = 'm';
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

    public int randomNumber(int a, int b)
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

    public void resetTileMap() 
    {
        myTilemap.ClearAllTiles();
        ConvertMapToTilemap(convertMapToString(30, 20, multidimensionalMap));
    }

}
