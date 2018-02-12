using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depthNpcBubble : MonoBehaviour
{

    private Vector3 pos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        transform.position = new Vector3(pos.x, pos.y, pos.y - 1.46f);
    }
}
