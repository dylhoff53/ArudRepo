using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 launchVector;
    public Rigidbody rig;
    public float force;
    public float maxLife;
    public float lifeCounter;
    public float multi;
    public int damage;
    public bool charged;
    public float chargebuffer;
    public bool walled;
    public bool wasCharged;


    private void Update()
    {
        lifeCounter += Time.deltaTime;
        if(lifeCounter >= maxLife)
        {
            Die();
        }
    }

    private void Start()
    {
        rig.AddForce(launchVector * force * multi);
        if(multi >= chargebuffer)
        {
            charged = true;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(wasCharged == true)
            {
                other.transform.GetComponent<Enemy>().charged = true;
            } 
            
            if(walled == true)
            {
                other.transform.GetComponent<Enemy>().gotWalled = true;
            }

            other.transform.GetComponent<Enemy>().Hit(damage);
            if (charged == true)
            {
                charged = false;
            }
            else
            {
                Die();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            walled = true;
        }
    }
}
