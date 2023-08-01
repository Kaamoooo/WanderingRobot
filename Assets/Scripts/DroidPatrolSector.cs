using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidPatrolSector  : MonoBehaviour
{
    public bool IsWalaInSector=true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsWalaInSector = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsWalaInSector = false;
        }
    }
}
