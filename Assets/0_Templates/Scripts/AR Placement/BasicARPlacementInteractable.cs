using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.EventSystems;


/// <summary>
/// A basic script to place objects on detected AR planes.
/// </summary>
public class BasicARPlacementInteractable : ARPlacementInteractable
{
    /// <summary>
    /// An enumerator variable that defines the placement options available to us.
    /// </summary>
    public enum PlacementMode { single, multiple };

    /// <summary>
    /// Creates an instance of the enumerator that a user can select in the inspector window.
    /// </summary>
    [SerializeField]
    [Tooltip("What placement mode to choose.")]
    private PlacementMode _placementMode;

    /// <summary>
    /// A GameObject to place when a raycast from a user touch hits a plane.
    /// </summary>
    [SerializeField]
    [Tooltip("A GameObject Prefab to place when user touches a plane.")]
    private GameObject _placementPrefab;

    /// <summary>
    /// A reference to the placed object in single mode.
    /// </summary>
    private GameObject _placedObject;

    /// <summary>
    /// A list of all raycast hits when a user touches the screen.
    /// </summary>
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    

    /// <summary>
    /// Places an object on a detected plane.
    /// This function is called when the user taps - places and removes a single finger.
    /// </summary>
    /// <param name="gesture"></param>
    protected override void OnEndManipulation(TapGesture gesture)
    {

        // If it isn't a proper tap, don't do anything.
        if (gesture.WasCancelled)
            return;

        // If a gesture is targeting an existing object, we shouldn't add a new one.
        // Allow for test planes (layer 9)
        if (gesture.TargetObject != null && gesture.TargetObject.layer != 9)
            return;

        // Raycast against the location the player touched to search for planes.
        if (GestureTransformationUtility.Raycast(gesture.StartPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0];

            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (Vector3.Dot(Camera.main.transform.position - hit.pose.position, hit.pose.rotation * Vector3.up) < 0)
                return;

            // If we are in single placement mode
            if(_placementMode == PlacementMode.single)
            {
                // If the object hasn't been placed yet
                if (_placedObject == null)
                {
                    // Create a new object at the position and rotation of the touch on the plane.
                    _placedObject = Instantiate(_placementPrefab, hit.pose.position, hit.pose.rotation);
                }
                else
                {
                    // Move the placed object to the new position and rotation
                    _placedObject.transform.position = hit.pose.position;
                    _placedObject.transform.rotation = hit.pose.rotation;
                }
            }
            else
            // If we are in multiple placement mode
            if (_placementMode == PlacementMode.multiple)
            {
                // Create a new gameobject at the position and rotation of the touch on the plane.
                GameObject.Instantiate(_placementPrefab, hit.pose.position, hit.pose.rotation);
            }
            
        }


    }

}