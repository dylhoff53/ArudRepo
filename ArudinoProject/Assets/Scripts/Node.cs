using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && other.GetComponent<Enemy>().target == gameObject.transform.position)
        {
            other.GetComponent<Enemy>().targetReached();
        }
    }
}
