using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic script to move inside a Cardboard VR environment.
/// </summary>
public class BasicCardboardMovement : MonoBehaviour
{
    /// <summary>
    /// Sets the speed of movement in VR.
    /// </summary>
    [SerializeField]
    [Tooltip("Speed of Movement")]
    private float _movementSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {

        // If the cardboard button is pressed
        //if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            // Move forward in the direction that the camera is facing
            transform.position += this.transform.GetChild(0).transform.forward.normalized;
        }
    }
}
