using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnfireright : MonoBehaviour
{
    private ParticleSystem fireP;
    private Transform fireT;
    private GameObject hand;
    private Transform handSpawnedOn;
    private GameObject fireHand_;
    private Transform fireHand;
    private bool launched;
    private bool launched_;
    private float upScale;
    private Firesummonright fireSummon;
    private float shootVel;
    private AudioSource fireAud;
    private float t;
    void Start()
    {
        fireSummon = FindObjectOfType<Firesummonright>();
        upScale = 0.2f;
        fireHand_ = GameObject.FindGameObjectWithTag("FireHandRight");
        fireAud = GetComponent<AudioSource>();
        fireHand = fireHand_.GetComponent<Transform>();
        hand = GameObject.FindGameObjectWithTag("RightHand");
        handSpawnedOn = hand.GetComponent<Transform>();
        fireP = GetComponent<ParticleSystem>();
        fireT = GetComponent<Transform>();
        fireP.Play();
        fireP.startSpeed = 0;
        fireT.localScale = new Vector3(0, 0, 0);
        fireT.transform.rotation = Quaternion.Euler(-90, 0, 0);
        launched = false;
        launched_ = false;
        t = 0;
    }

    void FixedUpdate()
    {
        ScaleFire();
        ScaleEmittion();


        if (launched == true)
        {
            fireT.localRotation = fireSummon.shootDir;

            upScale = 3;
            fireAud.Play();
            Destroy(gameObject, 10);
            launched_ = true;
            launched = false;
        }
        if (t < 3 && launched_ == false)
        {
            t += 1 * Time.deltaTime;
        }
        if (launched_ == true)
        {

            transform.Translate(Vector3.forward * shootVel * 0.1f * t);
        }
        else
        {
            transform.position = fireHand.position;
        }
    }
    void ScaleFire()
    {
        if (fireT.localScale.x < upScale + 0.3f)
        {
            fireT.localScale = new Vector3(fireT.localScale.x + upScale * Time.deltaTime, fireT.localScale.y + upScale * Time.deltaTime, fireT.localScale.z + upScale * Time.deltaTime);
        }
    }

    void ScaleEmittion()
    {
        if (fireP.startSpeed < 1.5f)
        {
            fireP.startSpeed += 1.5f * Time.deltaTime;
        }

    }

    public void Shoot()
    {
        shootVel = fireSummon.maxVel;
        launched = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Detonate();
            fireT.localScale = new Vector3(fireT.localScale.x + 3 * Time.deltaTime, fireT.localScale.y + 3 * Time.deltaTime, fireT.localScale.z + 3 * Time.deltaTime);
            Destroy(gameObject);
        }
    }
    void Detonate()
    {
        Vector3 explosionPosition = fireT.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, t * 3);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(t * 2.5f, explosionPosition, t * t + 3, 1.5f, ForceMode.Impulse);
            }
        }
    }
}