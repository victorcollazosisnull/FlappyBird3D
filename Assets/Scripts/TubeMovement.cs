using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    public float tubeSpeed = 6f; // Aumentamos velocidad

    public Transform parentTubes;

    private void Update()
    {
        transform.Translate(Vector3.left * tubeSpeed * Time.deltaTime);

        if (transform.position.x < -36f)
        {
            Destroy(parentTubes.gameObject);
        }
    }
}