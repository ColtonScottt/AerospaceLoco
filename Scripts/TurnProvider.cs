using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class TurnProvider : MonoBehaviour
{
    public enum turnMode
    {
        smooth,
        snap,
        none
    };

    [SerializeField]
    public turnMode TurnMode;

    [SerializeField] private InputActionReference[] inputActionReferencesArray;

    Rigidbody rb;
    ClimbProvider climbingProvider;
    public Transform cam;

    private float turnSpeed;

    private void Start()
    {
        if(PlayerPrefs.GetString("turnType") == "smooth")
            TurnMode = turnMode.smooth;
        if(PlayerPrefs.GetString("turnType") == "snap")
            TurnMode = turnMode.snap;
        if(PlayerPrefs.GetString("turnType") == "none")
            TurnMode = turnMode.none;

        rb = GetComponent<Rigidbody>();
        turnSpeed = PlayerPrefs.GetFloat("turnSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        turnYaw();
        turnPitch();
        turnRoll();

        if(climbingProvider.activeTransforms.Count <= 1)
        {
            rb.centerOfMass = climbingProvider[0].position;
        }
        else
        {
            rb.centerOfMass = new Vector3(0, 0, 0;)
        }
    }

    void turnYaw()
    {
        switch(TurnMode)
        {
            case turnMode.smooth:
                if(inputActionReferencesArray[0].action.ReadValue<Vector2>().x >= 0.5)
                {
                    rb.AddTorque(cam.up * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                if(inputActionReferencesArray[0].action.ReadValue<Vector2>().x <= -0.5)
                {
                    rb.AddTorque(-cam.up * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                return;
            case turnMode.snap:
                // TBC
                return;
        }
    }
    void turnPitch()
    {
        switch(TurnMode)
        {
            case turnMode.smooth:
                if(inputActionReferencesArray[1].action.ReadValue<Vector2>().y >= 0.5)
                {
                    rb.AddTorque(-cam.right * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                if(inputActionReferencesArray[1].action.ReadValue<Vector2>().y <= -0.5)
                {
                    rb.AddTorque(cam.right * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                return;
            case turnMode.snap:
                // TBC
                return;
        }
    }
    void turnRoll()
    {
        switch(TurnMode)
        {
            case turnMode.smooth:
                if(inputActionReferencesArray[2].action.ReadValue<Vector2>().x >= 0.5)
                {
                    rb.AddTorque(cam.forward * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                if(inputActionReferencesArray[2].action.ReadValue<Vector2>().x <= -0.5)
                {
                    rb.AddTorque(-cam.forward * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
                return;
            case turnMode.snap:
                // TBC
                return;
        }
    }
}
