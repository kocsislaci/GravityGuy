using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTextureMovement : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Material mat = GetComponent<Renderer>().material;
        mat.SetTextureOffset("_MainTex", new Vector2(pos.x * moveSpeed, 0));
    }
}
