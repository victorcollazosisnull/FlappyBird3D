using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    [Header("Configuracion de Tubos")]
    public GameObject TubesPrefab; 
    public float positionX = 13.19f; 
    public float positionToDestroy = -36f; 
    public float spawTimer = 3f; 

    private void Start()
    {
        InvokeRepeating("SpawingTubes", 0, spawTimer); 
    }
    private void SpawingTubes()
    {
        float newY = Random.Range(-26.25f, -16.95f); 
        GameObject newTubes = Instantiate(TubesPrefab, new Vector3(positionX, newY, 128.99f), Quaternion.identity);
    }
}