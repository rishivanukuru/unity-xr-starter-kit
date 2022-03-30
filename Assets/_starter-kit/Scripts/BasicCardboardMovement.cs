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
    private float _movementSpeed = 1f;

    // Flag to determine if the player should move or not.
    private bool _isMoving = false;
    
    // Start is called once at the beginning of a scene to initialize it.
    private void Start()
    {
        _isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a touch on the screen (or if the VR headset touch button is pressed)
        if(Input.touchCount>0)
        {
            // Get the properties of the first touch on the screen.
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase.
            switch (touch.phase)
            {
                //When a touch has first been detected:
                case TouchPhase.Began:
                    // Set player movement flag to true.
                    _isMoving = true;
                    break;


                // When a touch has finally been lifted:
                case TouchPhase.Ended:
                    // Set player movement flag to false.
                    _isMoving = false;
                    break;
            }

        }

        // If the player movement flag is true -- that means we should move
        if (_isMoving)
        {
            // Get and store the normalized forward vector of the camera.
            Vector3 _cameraForward = this.transform.GetChild(0).transform.forward.normalized;

            // Create a new vector that only cares about the component of the forward vector in the XZ plane (so that we don't move in the vertical direction).
            Vector3 _movementVector = new Vector3(_cameraForward.x, 0, _cameraForward.z);

            // Move forward in the direction that we just determined, at a certain speed, multiplied by a scaling factor that was determined experimentally.
            transform.position += _movementVector * _movementSpeed * 0.05f;
        }
        // Alternate method for reference - Google.XR.Cardboard.Api.IsTriggerPressed

    }
}