using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Firesummonleft : MonoBehaviour
{
    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Action_Boolean grabGrip;
    public SteamVR_Behaviour_Pose leftHand;
    public Transform leftHandPos;
    private bool trigger;
    public GameObject fireLeft;
    private Spawnfireleft spawnFire;
    public float calc;
    public float maxVel = Mathf.Infinity;
    private bool hasFireLoaded = false;
    private Vector3 before;
    private Vector3 after;
    public Quaternion shootDir;

    void Update()
    {
        if (grabPinch[SteamVR_Input_Sources.LeftHand].stateDown && hasFireLoaded == false)
        {
            hasFireLoaded = true;
            Instantiate(fireLeft, transform.position, transform.rotation);
            spawnFire = FindObjectOfType<Spawnfireleft>();
        }
        

        float x = leftHand.GetVelocity().x;
        float y = leftHand.GetVelocity().y;
        float z = leftHand.GetVelocity().z;
        calc = (x + y + z) / 3;

        if (calc > 1 && calc < maxVel)
        {
            Vector3 dir = after - before;
            Quaternion rot = Quaternion.LookRotation(dir);
            shootDir = rot;

            spawnFire.Shoot();
            hasFireLoaded = false;
            Debug.Log(before);
            Debug.Log(after);


        }
        if (calc < 1)
        {
            before = leftHandPos.transform.position;
        }
        else
        {
            after = leftHandPos.transform.position;
        }
        maxVel = calc;
    }
}
