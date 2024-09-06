using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public int yDis;

// Start is called before the first frame update
void Start()
{
   
}

// Update is called once per frame
void Update()
{
    transform.position = new Vector3(player.transform.position.x, player.transform.position.y - yDis , player.transform.position.z - 10);
}
}
