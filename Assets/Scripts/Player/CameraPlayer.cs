using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public Transform player;

    float z = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        z = (player.position.z-transform.position.z);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
