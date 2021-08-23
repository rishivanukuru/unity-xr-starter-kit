using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Placeholder Script for any events connected to the Basic AR Placement Interactable.
/// </summary>

[Serializable]
public class ARPlacementEvent : UnityEvent<BasicARPlacementInteractable, GameObject>
{

}