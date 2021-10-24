/*  Source file name: BulletController.cs
 *  Student Name: Jen Marc Capistrano
 *  Student ID: 101218004
 *  Date Last modified: October 24, 2021
 *  Program Description: This script controls the movement of the bullet, and checks the boundary in which the bullet will be returned 
 *                        in the bullet pool.
 *  Revision History: v0.01 -- added variables for the horizontal values
 *                          -- modified  _Move(), and _CheckBounds() to adapt to the landscape mode orientation
 *                          -- added an if-statement to check which current orientation the device have 
 *                             then fires the bullet in correct direction
 *                         
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IApplyDamage
{
    public float verticalSpeed;
    public float verticalBoundary;
    public BulletManager bulletManager;
    public int damage;
    // horizontal variables
    public float horizontalSpeed;
    public float horizontalBoundary;


    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // moves the bullet to the right
            transform.eulerAngles = new Vector3(0, 0, -90);
            transform.position += new Vector3(horizontalSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += new Vector3(0.0f, verticalSpeed, 0.0f) * Time.deltaTime;
        }
            
    }

    private void _CheckBounds()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // check to see if the bullet reaches the end of the screen on the right
            if (transform.position.x > horizontalBoundary)
            {
                bulletManager.ReturnBullet(gameObject);
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            if (transform.position.y > verticalBoundary)
            {
                bulletManager.ReturnBullet(gameObject);
            }
        }
           
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }
}
