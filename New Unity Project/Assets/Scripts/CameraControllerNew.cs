using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class CameraControllerNew : MonoBehaviour
{

    public Transform player;
    public GameObject playerObject;
    public Vector3 offset;
    public float thresholdUp; // if the player is on a platform THRESHOLD% up the screen, we shift
    public float thresholdDown; // Generally, this should be set to 1 or so since offsetY is a thing
    private float nextY; // when we have to shift up, we'll store the next Y here
    private bool isShifting; // are we shifting upward?
    private float shiftAmount = 10; // when the player moves, the amount that the camera shifts with each second
    private Camera camera;
    float vertExtent;
    public float lastGroundedY;
    public float leftBound;
    public float rightBound;
    public float upBound;
    public float downBound;
    public bool isConstrained;

    
    void Start()
    {
        isShifting = false;
        camera = GetComponent<Camera>();
        vertExtent = camera.orthographicSize; // https://answers.unity.com/questions/501893/calculating-2d-camera-bounds.html
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        lastGroundedY = transform.position.y;
    }
    void Update()
    {
        Vector3 nextPos = transform.position;
        nextPos = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z); // Camera follows the player with specified offset position
        transform.position = nextPos;
        if (playerObject.GetComponent<MainPlayerController>().IsGrounded())
        {
            if (player.position.y > transform.position.y + vertExtent*thresholdUp)
            {
                isShifting = true;
                nextY = player.position.y + offset.y;
                lastGroundedY = nextY;
            }
            if (player.position.y  < transform.position.y - vertExtent * thresholdDown)
            {
                isShifting = true;
                nextY = player.position.y + offset.y;
                lastGroundedY = nextY;
            }
        }
        if (isShifting)
        {
            ShiftY(nextY, Time.deltaTime);
        }
       else // keeps the player from going off the screen
        {
            if (player.position.y > transform.position.y + vertExtent)
            {
                nextPos.y += Time.deltaTime * shiftAmount;
            }
            else if (player.position.y < transform.position.y - vertExtent)
            {
                nextPos.y -= ((float)3)*Time.deltaTime * shiftAmount;
               
            }
            transform.position = nextPos;
        } 
        if (isConstrained) // if the camera is constrained, we want it to fit within the edges
        {
            Vector3 corrected = transform.position;
            if (corrected.x > rightBound)
            {
                corrected.x = rightBound;
            }
            if (corrected.x < leftBound)
            {
                corrected.x = leftBound;
            }
            if (corrected.y > upBound)
            {
                corrected.y = upBound;
            }
            if (corrected.y < downBound)
            {
                corrected.y = downBound;
            }
            transform.position = corrected;
        }
    }
    void ShiftY(float y, float time) // will slowly shift to a new platform after being called. We'l call in Time*deltaTime to time
    {
        float ourShift = time * shiftAmount;
        Vector3 newPos = transform.position;
        if (Math.Abs(transform.position.y - y) < time*shiftAmount)
        {
            newPos.y = y;
            isShifting = false;
        }
        else if (transform.position.y < y)
        {
            newPos.y = newPos.y + time*shiftAmount;
        }
        else
        {
            newPos.y = newPos.y - time*shiftAmount;
        }
        transform.position = newPos;
    }
}