using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityContainer : MonoBehaviour
{
    [SerializeField] private InputActionProperty velocityinput;

    public Vector3 Velocity => velocityinput.action.ReadValue<Vector3>();
}
