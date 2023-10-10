using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbingProvider : LocomotionProvider
{
    public Rigidbody playerRigidbody;
    [SerializeField] private GameObject playerRig;
    [SerializeField] private EchoMovement em;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip climbSFX;
    public string tempTag;
    public float flickSens;

    private bool isClimbing = false;
    private List<VelocityContainer> activeVelocities = new List<VelocityContainer>();
    public List<Transform> activeTransforms = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddProvider(VelocityContainer provider, Transform position)
    {
        if(!activeVelocities.Contains(provider))
        {
            activeTransforms.Add(position);
            activeVelocities.Add(provider);
            playerRigidbody.velocity = Vector3.zero;
            audioSource.PlayOneShot(climbSFX, PlayerPrefs.GetFloat("SFXVol"));
            em.canMove = false;
        }
    }

    public void RemoveProvider(VelocityContainer provider, Transform position)
    {
        if(activeVelocities.Contains(provider))
        {
            Vector3 saveVelocity = playerRig.transform.rotation * CollectControllerVelocity();

            tempTag = activeTransforms[0].tag;

            activeVelocities.Remove(provider);
            activeTransforms.Remove(position);

            playerRigidbody.AddForce(-saveVelocity * saveVelocity.magnitude * flickSens);

            em.canMove = true;
        }
    }

    private void Update() 
    {
        TryBeginClimb();

        if(isClimbing)
            ApplyVelocity();

        TryEndClimb();
    }

    private void TryBeginClimb()
    {
        if(CanClimb() && BeginLocomotion())
        {
            isClimbing = true;
        }
    }

    private void TryEndClimb()
    {
        if(!CanClimb() && EndLocomotion())
        {
            isClimbing = false;
        }
    }

    private bool CanClimb()
    {
        return activeVelocities.Count != 0;
    }

    private void ApplyVelocity()
    {
        Vector3 velocity = CollectControllerVelocity();
        Transform origin = system.xrOrigin.transform;

        velocity = origin.TransformDirection(velocity);
        velocity *= Time.deltaTime;

        origin.position -= velocity;
    }


    private Vector3 CollectControllerVelocity()
    {
        Vector3 totalVelocity = Vector3.zero;

        foreach(VelocityContainer container in activeVelocities)
            totalVelocity += container.Velocity;

        return totalVelocity;
    }

}
