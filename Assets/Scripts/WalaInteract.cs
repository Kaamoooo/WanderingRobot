using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalaInteract : MonoBehaviour
{
    public Transform CameraTransform;

    private WalaSound walaSound;

    private void Start()
    {
        walaSound = GetComponent<WalaSound>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit raycastHit;
            Vector3 lookDirection = transform.forward;
            Quaternion quaternion = Quaternion.AngleAxis(CameraTransform.eulerAngles.x, transform.right);
            lookDirection=quaternion*lookDirection;
            if (Physics.Raycast(transform.position,lookDirection, out raycastHit, 1.5f, LayerMask.GetMask("Interactive")))
            {
                GameObject hit = raycastHit.collider.gameObject;
                if (hit.CompareTag("Interactive"))
                {
                    ObjectInteractive objectInteractive;
                    hit.TryGetComponent<ObjectInteractive>(out objectInteractive);
                    if (objectInteractive.Interact())
                    {
                        walaSound.PlayPositiveBeep();
                    }
                    else
                    {
                        walaSound.PlayNegativeBeep();
                    }
                    
                }
            }
            
        }
    }
}
