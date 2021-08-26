using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extremely basic script to move an object similar to a game player.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    /// <summary>
    /// Speed of Player Movement in meters/frame.
    /// </summary>
    [SerializeField]
    [Tooltip("Speed of Player Movement.")]
    private float _moveSpeed = 10.0f;

    // Holder variable for the Forward/Backward movement value
    private float _zMovement;

    // Holder variable for the Straffe movement value
    private float _xMovement;

    // Holder variable for the movement vector
    private Vector3 _movement;

    // Use this for initialization.
    void Start()
    {
        // Nothing here for now.
    }

    // Update is called once per frame
    void Update()
    {
        // Get the value of vertical input (W/S/Up Arrow/Down Arrow) and calculate a movement value.
        _zMovement = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;

        // Get the value of horizontal input (A/D/Left Arrow/Right Arrow) and calculate a movement value.
        _xMovement = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;

        // Multiply those movement values with their respective direction vectors to get a resultant movement vector.
        _movement = transform.forward * _zMovement + transform.right * _xMovement;

        // Move according to the resultant vector.
        transform.Translate(_movement);

    }
}