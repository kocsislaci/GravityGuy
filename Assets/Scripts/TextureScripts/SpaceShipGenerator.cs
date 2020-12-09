using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipGenerator : MonoBehaviour
{
    public GameObject razorCrestPrefab;
    public GameObject sikloPrefab;
    public GameObject deLoreanPrefab;
    public GameObject nagy1Prefab;
    public GameObject nagy2Prefab;
    public GameObject slave1Prefab;

    public float generationFrequency = 2.0f;
    public int maximumNumberOfShips = 5;
    public int maximumNumberOfBigShips = 1;
    public float spawnProbability = 0.5f;


    private float velocityMin = 1.0f;
    private float velocityMax = 10.0f;
    private float posZFar = 19.0f;
    private float posZNear = 14.0f;
    private float posYMin = 0.0f;
    private float posYMax = 4.5f;
    private float posXMin = -12.5f;
    private float posXMax = 12.5f;
    private float deltaTime = 0.0f;

    private float shipResult = -1.0f;
    private float posXResult = -1.0f;
    private float posYResult = -1.0f;
    private float posZResult = -1.0f;
    private float velocityResult = -1.0f;
    private int actualNumberOfShips = 0;
    private int actualNumberOfBigShips = 0;
    GameObject newShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deltaTime >= 1 / generationFrequency)
        {
            if (Random.Range(0.0f, 1.0f) < spawnProbability && actualNumberOfShips < maximumNumberOfShips)
            {
                shipResult = Random.Range(0.0f, 6.0f);
                if (shipResult > 3.0f && (actualNumberOfBigShips == 0))
                {
                    actualNumberOfBigShips++;
                }
                else
                {
                    shipResult /= 2.0f;
                }
                posXResult = Random.Range(posXMin, posXMax);
                posYResult = Random.Range(posYMin, posYMax);
                posZResult = Random.Range(0.0f, 1.0f);
                velocityResult = Random.Range(velocityMin, velocityMax);
                spawnShip(shipResult, posXResult, posYResult, posZResult, velocityResult);
                deltaTime = 0.0f;
                actualNumberOfShips++;
            }
        }
        deltaTime += Time.deltaTime;
    }
    private void spawnShip(float shipPrefab, float posX, float posY, float posZ, float velocity)
    {
        bool isBig = false;
        Vector3 spawnPosition = new Vector3();
        if (posZ <= 0.5f)
        {
            spawnPosition.z = posZNear;
        }
        else
        {
            spawnPosition.z = posZFar;
        }
        if (posX >=  (posXMax + posXMin) / 2.0f)
        {
            spawnPosition.x = posXMax;
            if (shipPrefab > 3.0f)
                spawnPosition.x += 2.3f;
        }
        else
        {
            spawnPosition.x = posXMin;
            if (shipPrefab > 3.0f)
                spawnPosition.x -= 2.3f;
        }
        spawnPosition.y = posY;


        if (shipPrefab <= 1.0)
        {
            newShip = Instantiate(razorCrestPrefab, transform);
        }
        else if (shipPrefab > 1.0 && shipPrefab <= 2.0)
        {
            newShip = Instantiate(sikloPrefab, transform);
        }
        else if (shipPrefab > 2.0 && shipPrefab <= 3.0)
        {
            newShip = Instantiate(deLoreanPrefab, transform);
        }
        else if (shipPrefab > 3.0 && shipPrefab <= 4.0)
        {
            newShip = Instantiate(nagy1Prefab, transform);
            isBig = true;
        }
        else if (shipPrefab > 4.0 && shipPrefab <= 5.0)
        {
            newShip = Instantiate(nagy2Prefab, transform);
            isBig = true;
        }
        else if (shipPrefab > 5.0 && shipPrefab <= 6.0)
        {
            newShip = Instantiate(slave1Prefab, transform);
            isBig = true;
        }
        newShip.transform.localPosition = spawnPosition;

        if (posX >= (posXMax + posXMin) / 2.0f)
        {
            newShip.GetComponent<SpriteRenderer>().flipX = true;
            newShip.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity + newShip.transform.parent.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
        }
        else
        {
            newShip.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity + newShip.transform.parent.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
        }
        if (posZ <= 0.5f)
        {
            if (!isBig)
                newShip.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (isBig && newShip.transform.localPosition.y <= 1.0f)
        {
            newShip.transform.localPosition += new Vector3(0.0f, -(posYMin - newShip.transform.localPosition.y), 0.0f);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BigShip" || col.gameObject.tag == "SmallShip")
        {
            Destroy(col.gameObject);
            actualNumberOfShips--;
            if (col.gameObject.tag == "BigShip")
            {
                actualNumberOfBigShips--;
            }
        }
    }

}
