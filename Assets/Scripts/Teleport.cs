using UnityEngine;
using System.Collections;

///<summary>
///The Controller for Teleporting
///</summary>
public class Teleport : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    ///<summary>
    ///Wraps when an object goes off screen
    ///</summary>
    void OnBecameInvisible()
    {
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.x > 1 || viewportPosition.x< 0)
        {
            newPosition.x = -newPosition.x;
        }

        if (viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
        }

        transform.position = newPosition;
    }
}
