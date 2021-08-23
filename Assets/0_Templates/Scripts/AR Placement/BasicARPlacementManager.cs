using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic script to place one or more objects anywhere in a room-scale AR experience.
/// </summary>
public class BasicARPlacementManager : MonoBehaviour
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
    /// This is the Prefab that is to be placed.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the Prefab that is to be placed.")]
    private GameObject _placedObjectPrefab;

    /// <summary>
    /// Distance from the camera where the Prefab should be placed.
    /// </summary>
    [SerializeField]
    [Tooltip("Distance from the camera where the Prefab should be placed.")]
    private Vector3 _offsetFromCamera;

    /// <summary>
    /// A list of all the placed objects (used in multiple mode).
    /// </summary>
    private List<GameObject> _placedObjects;

    /// <summary>
    /// A reference to the placed object in single mode.
    /// </summary>
    private GameObject _singlePlacedObject;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the list of placed objects.
        _placedObjects = new List<GameObject>();

        // Setting the reference of the single placed object to be empty.
        _singlePlacedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button is pressed down OR a single touch is made on a screen
        if(Input.GetMouseButtonDown(0))
        {
            // If we are in single placement mode
            if(_placementMode == PlacementMode.single)
            {
                // If the object has not been placed yet (i.e. the object reference is empty)
                if(!_singlePlacedObject)
                {
                    // Create a new object and assign it to the object reference
                    _singlePlacedObject = Instantiate(_placedObjectPrefab);

                    // Set the position of the object to be at the specified offset from the main camera.
                    // For this to work, there must be a camera with the tag "Main Camera" in the scene.
                    // Transform.TransformPoint converts a position from local coordinates to global coordinates.
                    _singlePlacedObject.transform.position = Camera.main.gameObject.transform.TransformPoint(_offsetFromCamera);

                    // Set the rotation of the object to be the same as the AR Camera.
                    _singlePlacedObject.transform.rotation = Camera.main.transform.rotation;

                }
                // If the object has already been placed
                else
                {
                    // Just move the object to the new location, and rotate it appropriately
                    _singlePlacedObject.transform.position = Camera.main.gameObject.transform.TransformPoint(_offsetFromCamera);
                    _singlePlacedObject.transform.rotation = Camera.main.transform.rotation;
                }
            }
            else
            // If we are in multiple placement mode
            if(_placementMode == PlacementMode.multiple)
            {
                // Create a new Gameobject of the type of the Prefab
                GameObject _currentPlacedObject = Instantiate(_placedObjectPrefab);
                
                // Set the position and rotation of the gameobject
                _currentPlacedObject.transform.position = Camera.main.gameObject.transform.TransformPoint(_offsetFromCamera);
                _currentPlacedObject.transform.rotation = Camera.main.transform.rotation;

                // Add the gameobject to the list of placed objects
                _placedObjects.Add(_currentPlacedObject);
            }
        }
    }
}
