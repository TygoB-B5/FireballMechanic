using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Collidertocamera : MonoBehaviour
{
    public Transform player;
    public Transform vrCamPos;
    public Transform rig;
    public SteamVR_Action_Vector2 joystick;
    public Transform mover;
    public float speed;

    void FixedUpdate()
    {
        Vector2 move = joystick[SteamVR_Input_Sources.Any].delta;
        player.transform.rotation = Quaternion.EulerAngles(player.transform.rotation.x, vrCamPos.rotation.y, player.transform.rotation.z);
        player.transform.Translate(Vector3.forward * speed * move.y);
        rig.transform.position = new Vector3(player.transform.position.x, rig.transform.position.y, player.transform.position.z);

    }
}
