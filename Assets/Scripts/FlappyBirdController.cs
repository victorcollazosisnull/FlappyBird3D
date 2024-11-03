using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyBirdController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float birdForceImpulse = 4f;
    public float birdRotation = 3f;
    public float velocityBird;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        BirdRotate();
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
        _rigidbody.AddForce(Vector3.up * birdForceImpulse, ForceMode.Impulse);
        velocityBird = birdForceImpulse;
    }
    private void BirdRotate()
    {
        velocityBird += Physics.gravity.y * Time.deltaTime;
        float rotation = Mathf.Clamp(velocityBird * 5f, -30f, 30f);
        Quaternion targetRotation = Quaternion.Euler(rotation, -96.322f, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, birdRotation * Time.deltaTime);
    }
}
