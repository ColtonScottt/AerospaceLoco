using UnityEngine.InputSystem;
using UnityEngine;

public class EchoLoco : MonoBehaviour
{
    Rigidbody playerRigidbody;

    [SerializeField] private InputActionReference[] inputActionReferencesArray;
    [SerializeField] private Transform[] playerPositionsArray;

    [Header("Float/Int/Bool")]
    public bool canMove;
    [SerializeField] private float boostForce;
    [SerializeField] private float breakingSpeed;
    [SerializeField] private float backBoostForce;
    [SerializeField] private int boosts;

    // Subscribing to back-booster binding.
    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        inputActionReferencesArray[3].action.started += BackBoosting;
    }
	// Unsubscribe to back-booster binding.
    private void OnDestroy()
    {
        inputActionReferencesArray[3].action.started -= BackBoosting;
    }
    private void Update()
    {
        UpdateControllerMovement();
    }
    private void UpdateControllerMovement()
    {
        if(inputActionReferencesArray[0].action.IsPressed())
            playerRigidbody.AddForce(playerPositionsArray[0].transform.forward * boostForce * Time.deltaTime);
        if(inputActionReferencesArray[1].action.IsPressed())
            playerRigidbody.AddForce(playerPositionsArray[1].transform.forward * boostForce * Time.deltaTime);
        if(inputActionReferencesArray[2].action.IsPressed())
            playerRigidbody.velocity = playerRigidbody.velocity * breakingSpeed;
    }
    private void BackBoosting(InputAction.CallbackContext context)
    {
        if(boosts >= 1 && canMove)
        {
            playerRigidbody.AddForce(playerPositionsArray[2].transform.forward * backBoostForce * Time.deltaTime, ForceMode.VelocityChange);
            boosts -= 1;
        }
    }
}