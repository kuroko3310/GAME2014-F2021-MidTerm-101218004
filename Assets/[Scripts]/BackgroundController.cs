/*  Source file name: BackgroundController.cs
 *  Student Name: Jen Marc Capistrano
 *  Student ID: 101218004
 *  Date Last modified: October 24, 2021
 *  Program Description: This script controls the movement of the background, the goal of this script is to make it 
 *                       move from right->left while in landscape mode and up->down when in portrait mode.
 *  Revision History: v0.01 -- added variables for the horizontal values
 *                          -- modified _Reset(), _Move(), and _CheckBounds() to adapt to the landscape mode orientation
 *                         
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;
    // horizontal variables
    public float horizontalSpeed;
    public float horizontalBoundary;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    // Check the current orientation and resets the background
    private void _Reset()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            transform.position = new Vector3(horizontalBoundary, 0.0f);
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.position = new Vector3(0.0f, verticalBoundary);
        }
                
    }

    // modified to check which orientation is going and implement the movement of the background based on that
    private void _Move()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.position -= new Vector3(0.0f, verticalSpeed) * Time.deltaTime;
        }
            
    }

    private void _CheckBounds()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // if the background move past the left screen, it will reset back to the right
            if (transform.position.x <= -horizontalBoundary)
            {
                _Reset();
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            // if the background is lower than the bottom of the screen then reset
            if (transform.position.y <= -verticalBoundary)
            {
                _Reset();
            }
        }
        
    }
}
