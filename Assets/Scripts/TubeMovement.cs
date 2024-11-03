using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    public float tubeSpeed = 3f; 

    private void Update()
    {
        transform.Translate(Vector3.left * tubeSpeed * Time.deltaTime);

        if (transform.position.x < -36f) 
        {
            Destroy(gameObject); 
        }
    }
}