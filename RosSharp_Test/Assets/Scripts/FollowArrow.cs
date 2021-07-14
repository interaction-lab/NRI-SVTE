using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0.2f, 0.5f);
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // follow arrow as it moves and have the camera offset so it is behind the arrow
        transform.position = arrow.transform.position + offset;
    }
}
