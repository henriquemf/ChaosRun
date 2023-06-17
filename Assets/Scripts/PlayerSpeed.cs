using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    // get Player transform
    public Transform player;
    public int position = 0;
    // Start is called before the first frame update
    void Start()
    {
        // get Player transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        // make this object follow the player with X offset of the position variable
        transform.position = new Vector3(player.position.x + position, transform.position.y, transform.position.z);
    }
}
