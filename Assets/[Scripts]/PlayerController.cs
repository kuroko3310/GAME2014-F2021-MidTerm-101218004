/*  Source file name: PlayerController.cs
 *  Student Name: Jen Marc Capistrano
 *  Student ID: 101218004
 *  Date Last modified: October 24, 2021
 *  Program Description: This script basically controls the player's input such as move, fire bullet, 
 *                       and limit the player movements on screen by adding some boundary.
 *  Revision History: v0.01 -- added variables for landscape orientations
 *                          -- modified _Move() and _CheckBounds by adding an if-statement to check what screen orientation
 *                             is the device using
 *                          -- placed the player's ship to the left-hand side and rotated to face the right side of the screen
 *                          -- adjusted the transform.position's values, changed from x->y to adapt on the landscape orientation
 */


using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    [Header("Boundary Check")]
    public float horizontalBoundary;
    // vertical values
    public float verticalBoundary;

    [Header("Player Speed")]
    public float horizontalSpeed;
    public float maxSpeed;
    public float horizontalTValue;
    // vertical values
    public float verticalSpeed;
    public float verticalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            transform.position = new Vector3(-9,transform.position.y); // to bring the player's ship on the left-hand side of the screen
            transform.eulerAngles = new Vector3(0, 0, -90);  // to rotate the player's ship to face the right side of the screen
           
            
            // touch input support
            foreach (var touch in Input.touches)
            {
                var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                if (worldTouch.y > transform.position.y)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (worldTouch.y < transform.position.y)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                m_touchesEnded = worldTouch;

            }

            // keyboard support
            if (Input.GetAxis("Vertical") >= 0.1f)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (Input.GetAxis("Vertical") <= -0.1f)
            {
                // direction is negative
                direction = -1.0f;
            }

            if (m_touchesEnded.y != 0.0f)
            {
                transform.position = new Vector2(transform.position.x,Mathf.Lerp(transform.position.y, m_touchesEnded.y, verticalTValue));
            }
            else
            {
                Vector2 newVelocity = m_rigidBody.velocity + new Vector2(0.0f, direction * verticalSpeed);
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.position = new Vector3(transform.position.x, -4.0f);  // to reset the position of the player ship's on portrait mode
            transform.eulerAngles = new Vector3(0, 0, 0);       // to reset the rotation of the player ship's back to facing upwards
            // touch input support
            foreach (var touch in Input.touches)
            {
                var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                if (worldTouch.x > transform.position.x)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (worldTouch.x < transform.position.x)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                m_touchesEnded = worldTouch;

            }

            // keyboard support
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                // direction is negative
                direction = -1.0f;
            }

            if (m_touchesEnded.x != 0.0f)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
            }
            else
            {
                Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }

       
    }

    private void _CheckBounds()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // check right bounds
            if (transform.position.y >= verticalBoundary)
            {
                transform.position = new Vector3(transform.position.x, verticalBoundary, 0.0f);
            }

            // check left bounds
            if (transform.position.y <= -verticalBoundary)
            {
                transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            // check right bounds
            if (transform.position.x >= horizontalBoundary)
            {
                transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
            }

            // check left bounds
            if (transform.position.x <= -horizontalBoundary)
            {
                transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
            }
        }
         

    }
}
