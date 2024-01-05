using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] destinations;
    public int nodeCounter;
    public Vector3 target;
    public int health;
    public GameManager gm;
    public int scoreValue;
    public bool gotWalled;
    public int wallValue;
    public bool charged;

    private void Start()
    {
        target = destinations[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target);
    }

    public void targetReached()
    {
        nodeCounter++;
        if (nodeCounter >= destinations.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            target = destinations[nodeCounter].position;
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if(charged == true)
            {
                scoreValue += wallValue;
            }
            
            if(gotWalled == true)
            {
                scoreValue += wallValue;
            }
            gm.ChangeScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
