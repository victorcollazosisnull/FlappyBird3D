using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class FlappyBirdController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public ParticleSystem jumpParticles;
    public ParticleSystem collisionParticles;
    [Header("Salto y Rotación de FlappyBird")]
    public float birdForceImpulse = 4f;
    public float birdRotation = 3f;
    public float velocityBird;
    [Header("Fuerzas del FlappyBird")]
    public float forceChockTube = 10f; 
    public float forceOppositeDirection = 3f; 

    public float timePositionMyself = 0.5f;
    private bool collisionTube = false;
    private bool applyForce = false;

    public static event Action jumpColor;
    public AudioSource jumpSound;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }
    void Update()
    {
        BirdRotate();
        CheckIfPlayerDie();
    }
    public void ReadJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BirdJump();
        }
    }
    private void BirdJump()
    {
        if (Time.timeScale != 0f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * birdForceImpulse, ForceMode.Impulse);
            jumpColor?.Invoke();
            if (jumpParticles != null)
            {
                jumpParticles.Play();
            }
            jumpSound.Play();
        }
    }
    private void BirdRotate()
    {
        float flappyRote = Mathf.Clamp(_rigidbody.velocity.y * 5f, -30f, 30f);
        Quaternion rotation = Quaternion.Euler(flappyRote, -96.322f, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, birdRotation * Time.deltaTime);
    }
    private void CheckIfPlayerDie()
    {
        if (transform.position.y < -12.7f || transform.position.y > 15.3f || transform.position.x < -23.53f || transform.position.x > 23.76)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    private IEnumerator MoveToTubesCenter(Vector3 newPosition)
    {
        float time = 0;
        Vector3 initialPosition = transform.position;

        while (time < timePositionMyself)
        {
            transform.position = Vector3.Lerp(initialPosition, newPosition, time / timePositionMyself);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
        collisionTube = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube") && !collisionTube)
        {
            collisionTube = true;
            Vector3 backForce = new Vector3(-forceChockTube, 0, 0);
            _rigidbody.AddForce(backForce, ForceMode.Impulse);
            if (collisionParticles != null)
            {
                collisionParticles.transform.position = collision.contacts[0].point; 
                collisionParticles.Play();
            }
            Transform Tubes = collision.transform.parent;
            Transform TubeSup = Tubes.Find("TubeSup");
            Transform TubeInf = Tubes.Find("TubeInf");

            if (TubeSup != null && TubeInf != null)
            {
                float middlePosicion = (TubeSup.position.y + TubeInf.position.y) / 2;
                Vector3 currentPosicion = new Vector3(transform.position.x, middlePosicion, transform.position.z);
                StartCoroutine(MoveToTubesCenter(currentPosicion));
            }
            StartCoroutine(ReinicioVelocidadH());
            StartCoroutine(ActivarFuerzaContraria());
        }
    }
    private IEnumerator ActivarFuerzaContraria()
    {
        for (float time = 0; time < 0.5f; time += Time.deltaTime)
        {
            _rigidbody.AddForce(new Vector3(forceOppositeDirection, 0, 0), ForceMode.Force);
            yield return null;
        }
        applyForce = false;
    }
    private IEnumerator ReinicioVelocidadH()
    {
        yield return new WaitForFixedUpdate();
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
    }
    private void FixedUpdate()
    {
        if (applyForce)
        {
            Vector3 forwardForce = new Vector3(forceOppositeDirection, 0, 0);
            _rigidbody.AddForce(forwardForce, ForceMode.Force);
        }
    }
}