/*  Source file name: EnemyController.cs
 *  Student Name: Jen Marc Capistrano
 *  Student ID: 101218004
 *  Date Last modified: October 24, 2021
 *  Program Description: Everything's that happening on the enemy is in this script, such as movement
 *  Revision History: v0.01 -- added variables for landscape orientations, and an offset values to add a unique position for each enemy
 *                          -- made the enemy move up and down for landscape orientation, rotate the enemy to face the player, and added 
 *                             top and bottom boundaries.
 *                          
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float direction;
    // vertical variables
    public float verticalSpeed;
    public float verticalBoundary;
    // position offset values
    public float PosXOffset;
    public float PosYOffset;
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
            transform.position = new Vector3(PosXOffset, transform.position.y); // moves the enemy to the right side of the screen
            transform.eulerAngles = new Vector3(0, 0, -90);     // rotates the enemy's direction to face the player
            transform.position += new Vector3(0.0f,verticalSpeed * direction * Time.deltaTime,0.0f);   // make the enemy move up and down
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.position = new Vector3(transform.position.x, PosYOffset);
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
        }
            
    }

    private void _CheckBounds()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // check the top and bottom boundaries
            if (transform.position.y >= verticalBoundary)
            {
                direction = -1.0f;
            }

            if (transform.position.y <= -verticalBoundary)
            {
                direction = 1.0f;
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            // check right boundary
            if (transform.position.x >= horizontalBoundary)
            {
                direction = -1.0f;
            }

            // check left boundary
            if (transform.position.x <= -horizontalBoundary)
            {
                direction = 1.0f;
            }
        }
        
    }
}
