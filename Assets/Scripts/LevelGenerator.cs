using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject cubePlatform1Prefab;
    public GameObject cubePlatform1CroppedPrefab;
    public int cameraXPos;

    private int generatedSections = 0;

    void Start()
    {

    }
    void Update()
    {
        cameraXPos = (int)GameObject.FindObjectOfType<Camera>().transform.position.x;
        if (cameraXPos >= (generatedSections * 50 - 25) && generatedSections > 0)
        {
            generateRandomSection();
        }
    }
    public void RestartGame() // meg kell hivni mashonnan!!
    {
        if (!ApplicationModel.multiplayer)
        {
            cameraXPos = (int)GameObject.FindObjectOfType<Camera>().transform.position.x;
            generatePlayerStarterPlatforms();
            generateRandomSection();
        }
        else
        {
            cameraXPos = (int)GameObject.FindObjectOfType<Camera>().transform.position.x;
            generatePlayerStarterPlatforms();
            generateRandomSection();
        }
    }
  

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !ApplicationModel.multiplayer)
        {
            GameOver();
        }
        else if (col.gameObject.tag == "Player" && ApplicationModel.multiplayer)
        {
            GameOver();
        }
        if (col.gameObject.tag == "Platform")
        {
            Destroy(col.gameObject);
        }
    }
    public void GameOver()
    {
        this.generatedSections = 0;
        deletePlatforms();
    }

    private void generateRandomSection()
    {
        float seed = Random.Range(0.0f, 2.0f);
        int intSeed = (int)(seed);

        switch (intSeed)
        {
            case 0:
                generateSectionOne(generatedSections);
                break;
            case 1:
                generateSectionTwo(generatedSections);
                break;
            case 2:
                generateSectionThree(generatedSections);
                break;
            case 3:
                generateSectionFour(generatedSections);
                break;
            case 4:
                generateSectionFive(generatedSections);
                break;
            default:
                generateSectionZero(generatedSections);
                break;
        }
    }
    private void generateBlocksInARow(int lenght, int startPosX, int PosY)
    {
        float xPos;
        float yPos = (float)(PosY % 6);
        for (int i = 0; i < lenght; i++)
        {
            xPos = (float)i + startPosX;
            if (i == 0)
            {
                Instantiate(cubePlatform1Prefab, new Vector3(xPos, yPos, -1.0f), Quaternion.identity);
            }
            else
            {
                Instantiate(cubePlatform1CroppedPrefab, new Vector3(xPos, yPos, -1.0f), Quaternion.identity);
            }
        }
    }
    private void generateBlocksInAColumn(int height, int PosX, int startPosY) // upwards
    {
        for (int i = 0; i < height; i++)
        {
            Instantiate(cubePlatform1Prefab, new Vector3((float)(PosX), (float)(startPosY + i), -1.0f), Quaternion.identity);
        }
    }
    private void generateStaircase(int length, int upOrDown, int startPosX, int startPosY)
    {
        if (upOrDown > 0)
        {
            for (int i = 0; i < length; i++)
            {
                Instantiate(cubePlatform1Prefab, new Vector3((float)(startPosX + i), (float)((startPosY + i) % 6), -1.0f), Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                Instantiate(cubePlatform1Prefab, new Vector3((float)(startPosX + i), (float)((startPosY - i) % 6), -1.0f), Quaternion.identity);
            }
        }
    }

    private void generatePlayerStarterPlatforms()
    {
        generateBlocksInARow(10, -10, -5);
        generateBlocksInARow(10, -10, 5);
        generateBlocksInARow(10, -10, 0);
    }
    private void generateSectionZero(int generatedSections)
    {
        int xOffset = generatedSections * 50;
        generateBlocksInARow(50, 0 + xOffset, -5);
        generateBlocksInARow(50, 0 + xOffset, 5);
        this.generatedSections++;
    }
    private void generateSectionOne(int generatedSections)
    {
        int xOffset = generatedSections * 50;

        generateBlocksInAColumn(5, 2 + xOffset, -2);
           generateBlocksInARow(4, 3 + xOffset, -5);
           generateBlocksInARow(4, 3 + xOffset, 5);

        generateBlocksInAColumn(5, 9 + xOffset, -2);
        generateBlocksInARow(4, 10 + xOffset, -5);
        generateBlocksInARow(4, 10 + xOffset, 5);

        generateBlocksInAColumn(5, 16 + xOffset, -2);
        generateBlocksInARow(4, 17 + xOffset, -5);
        generateBlocksInARow(4, 17 + xOffset, 5);

        generateStaircase(8, 1, 21 + xOffset, -4);
        generateBlocksInARow(10, 22 + xOffset, 5);
        generateBlocksInARow(10, 30 + xOffset, 3);
        generateBlocksInARow(10, 34 + xOffset, 5);
        generateBlocksInARow(1, 50 + xOffset, -5);

        this.generatedSections++;
    }
    private void generateSectionTwo(int generatedSections)
    {
        int xOffset = generatedSections * 50;

        generateStaircase(5, 1, 3 + xOffset, -5);
        generateStaircase(5, 1, 3 + xOffset, 1);

        generateStaircase(5, -1, 9 + xOffset, 5);
        generateStaircase(5, -1, 9 + xOffset, -1);

        generateStaircase(5, 1, 15 + xOffset, -5);
        generateStaircase(5, 1, 15 + xOffset, 1);

        generateStaircase(5, -1, 21 + xOffset, 5);
        generateStaircase(5, -1, 21 + xOffset, -1);

        generateStaircase(5, 1, 27 + xOffset, -5);
        generateStaircase(5, 1, 27 + xOffset, 1);

        generateStaircase(5, -1, 33 + xOffset, 5);
        generateStaircase(5, -1, 33 + xOffset, -1);

        generateStaircase(5, 1, 39 + xOffset, -5);
        generateStaircase(5, 1, 39 + xOffset, 1);

        generateStaircase(5, -1, 45 + xOffset, 5);
        generateStaircase(5, -1, 45 + xOffset, -1);

        //generateBlocksInARow(4, 46 + xOffset, 5);
        //generateBlocksInARow(4, 46 + xOffset, -5);
        this.generatedSections++;
    }
    private void generateSectionThree(int generatedSections)
    {
        int xOffset = generatedSections * 50;
        generateBlocksInARow(50 - 5, 5 + xOffset, -2);
        generateBlocksInARow(50 - 5, 5 + xOffset, 2);
        generateStaircase(5, 1, 0, -5);
        generateStaircase(5, -1, 0, 5);

        this.generatedSections++;
    }
    private void generateSectionFour(int generatedSections)
    {
        int xOffset = generatedSections * 50;

        this.generatedSections++;
    }
    private void generateSectionFive(int generatedSections)
    {
        int xOffset = generatedSections * 50;

        this.generatedSections++;
    }

    private void deletePlatforms()
    {
        GameObject[] everyPlatform = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < everyPlatform.Length; i++)
        {
            Destroy(everyPlatform[i]);
        }
    }
}
