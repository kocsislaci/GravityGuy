using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool debugMode;
    public GameObject player;

    void Start()
    {
    }
    void FixedUpdate()
    {
        debugMode = ApplicationModel.debugMode;

        if (debugMode)
        {
            if (player != null)
                this.transform.position = new Vector3(player.transform.position.x, 0.0f, -10.0f);
        }
        else
        {
            //this.transform.position += new Vector3(Time.deltaTime * ApplicationModel.difficulty * speed, 0.0f, 0.0f);
        }
    }
    public void Restart()
    {
        if (!ApplicationModel.multiplayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            this.transform.position = new Vector3(-8.0f, 0.0f, -10.0f);
            Debug.Log("player found, camera set");
        }
        else
        {
            this.transform.position = new Vector3(-8.0f, 0.0f, -10.0f);
        }
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(ApplicationModel.difficulty * 2, 0.0f);
    }
    public void searchPlayerGameobject()
    {
        if (!ApplicationModel.multiplayer)
            player = GameObject.FindGameObjectWithTag("Player");
    }
}
