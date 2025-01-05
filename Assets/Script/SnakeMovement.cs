using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    private Rigidbody2D body;

    public static Action<float> SnakeRotation;

    public float bounceForce = 20f;
    bool isCollided = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        SnakeRotation += Rotate;
        UIManager.ResetGame += ResetSnake;
        body.velocity = -transform.up * speed;
    }

    private void ResetSnake()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //body.velocity = -transform.up * speed;
    }

    private void Rotate(float rotationAmount)
    {
        // Get the current rotation of the object
        Vector3 currentRotation = transform.eulerAngles;

        // Calculate the new rotation by adding the rotation amount, adjusted for time and speed
        currentRotation.z += rotationAmount * Time.deltaTime * rotationSpeed;

        // Apply the new rotation to the transform
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void LateUpdate()
    {
        if (!isCollided)
        {
            body.velocity = -transform.up * speed;
        }
    }

    private void OnDestroy()
    {
        SnakeRotation -= Rotate;
        UIManager.ResetGame -= ResetSnake;
    }

    Coroutine bounceBackCoroutine;

    private void OnCollisionEnter2D(Collision2D other)
    {
        BounceBack(other.contacts[0].normal);

    }
    private void BounceBack(Vector2 collisionNormal)
    {
        isCollided = true;
        // Calculate the reflected direction
        Vector2 bounceDirection = Vector2.Reflect(-transform.up, collisionNormal);

        // Apply force in the reflected direction
        body.velocity = Vector2.zero;
        body.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

        if (bounceBackCoroutine != null)
        {
            StopCoroutine(bounceBackCoroutine);
        }
        // Start coroutine to gradually rotate to the new direction
        bounceBackCoroutine = StartCoroutine(RotateToDirection(bounceDirection));
    }

    private IEnumerator RotateToDirection(Vector2 targetDirection)
    {
        // Calculate the target rotation angle based on the direction
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 90f;

        while (true)
        {
            // Gradually rotate the snake head towards the target angle
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, 5 * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, 0, newAngle);

            Debug.Log("Current Angle: " + currentAngle + " Target Angle: " + targetAngle + " New Angle: " + newAngle);

            // Check if the rotation is close enough to the target angle
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 0.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, targetAngle);
                isCollided = false;
                bounceBackCoroutine = null;
                yield break; // Exit the coroutine once the rotation is complete
            }

            yield return null; // Wait for the next frame
        }
    }



}
