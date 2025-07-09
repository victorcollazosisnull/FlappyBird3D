using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreZone : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!scored && other.CompareTag("Player")) 
        {
            ScoreManager.Instance.AddPoint();
            scored = true;
        }
    }
}
