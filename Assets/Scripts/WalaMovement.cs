using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalaMovement : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MouseSensitivity = 10.0f;
    public GameObject Camera;
    public float CapturedRotationSpeed=0.5f;

    private WalaSleep walaSleep;
    private Rigidbody _rb;
    private Vector3 captureCameraRotateDir;
    private Quaternion captureCameraRotation;
    private Vector3 captureRotateDir;
    private Quaternion captureRotation;
    private bool startRotate=false;
    private WalaSound sound;
    Vector3 moveDir;
    void Start()
    {
        walaSleep = GetComponent<WalaSleep>();
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Camera.transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,0);
        sound=GetComponent<WalaSound>();
    }

    private void Update()
    {
        if(GameController.IsGameOver||!GameController.StartMoving) { return; }
        if(GameController.hasBeenCaptured&&!startRotate&& GameController.CaptureDroid!=null)
        {
            sound.PlayCapturedSound();
            _rb.velocity = Vector3.zero;
            Vector3 droidPosition= GameController.CaptureDroid.transform.position;
            captureCameraRotateDir=(droidPosition+Vector3.up*1f- Camera.transform.position).normalized;
            captureCameraRotation=Quaternion.LookRotation(captureCameraRotateDir);
            captureRotateDir = (droidPosition- Camera.transform.position).normalized;
            captureRotateDir=new Vector3(captureRotateDir.x,0,captureRotateDir.z);
            captureRotation = Quaternion.LookRotation(captureRotateDir);
            startRotate=true;
        }
        if(GameController.hasBeenCaptured&&startRotate)
        {
            Camera.transform.rotation = Quaternion.Slerp(Camera.transform.rotation, captureCameraRotation, CapturedRotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, captureRotation, CapturedRotationSpeed * Time.deltaTime);
            return;
        }


        if (!walaSleep.IsSleeping)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
            float x = Camera.transform.eulerAngles.x;
            transform.Rotate(0, mouseX, 0);
            if (x - mouseY < 310 && x - mouseY > 40) return;
            Camera.transform.Rotate(-mouseY,0,0);
        }

 
    }

    public void ResetStatus ()
    {
        startRotate = false;
        captureRotateDir = Vector3.zero;
        captureRotation= Quaternion.identity;
        captureCameraRotateDir= Vector3.zero;
        captureCameraRotation = Quaternion.identity;

    }
    void FixedUpdate()
    {
        if (GameController.IsGameOver||GameController.hasBeenCaptured||!GameController.StartMoving) { return; }
        if (walaSleep.IsSleeping)
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        float horizontal=Input.GetAxis("Horizontal");
        float vertical=Input.GetAxis("Vertical");
        moveDir= Vector3.zero;
        Transform cameraTranform = Camera.transform;
        if (Mathf.Abs(horizontal) > 0)
        {
            moveDir += cameraTranform.right * (horizontal > 0 ? 1 : -1);
        }
        if (Mathf.Abs(vertical) > 0)
        {
            moveDir += new Vector3(cameraTranform.forward.x,0,cameraTranform.forward.z).normalized* (vertical> 0 ? 1 : -1);
        }
        _rb.velocity = moveDir*Speed;
    }
}
