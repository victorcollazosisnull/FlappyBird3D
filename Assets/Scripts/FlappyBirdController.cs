using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class FlappyBirdController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Salto y Rotación de FlappyBird")]
    public float birdForceImpulse = 6f; // Aumentamos para hacerlo más dinámico
    public float birdRotation = 5f;
    [Header("Configuración de caída")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isJumpingHeld = false;
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
    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_rigidbody.velocity.y > 0 && !isJumpingHeld)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    public void ReadJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJumpingHeld = true;
        }
        if (context.canceled)
        {
            isJumpingHeld = false;
        }
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
            jumpSound?.Play();
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
        if (transform.position.y < -12.7f || transform.position.y > 15.3f || transform.position.x < -23.53f || transform.position.x > 23.76f)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube"))
        {
            GameOver(); // muerte instantánea
        }
    }

    private void GameOver()
    {
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene("GameOver");
    }
}