using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght;
    private float StartPos;

    private Transform cam;

    public float parallaxEffect;


    private void Start()
    {
        StartPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        float RePos = cam.position.x * (1 - parallaxEffect);
        float Distance = (cam.position.x * parallaxEffect);

        transform.position = new Vector3(StartPos + Distance, transform.position.y, transform.position.z);

        if(RePos > StartPos + lenght)
        {
            StartPos += lenght;
        }
        else if(RePos < StartPos - lenght)
        {
            StartPos -= lenght;
        }
    }
}
