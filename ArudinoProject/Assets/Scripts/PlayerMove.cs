using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 mouseLook;
    public LayerMask mask;
    public Vector3 target;
    public Camera mainCam;
    public float speed;
    public bool readyToFire;
    public Vector2 lastInput;
    public int checkCount;
    public bool fire;
    public float chargeTimer;
    public float maxCharge;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool aimingLeft;
    public bool aimingRight;
    public float sideAimTimer;
    public float maxAimTimer;
    public Transform centerAim;
    public Transform leftAim;
    public Transform rightAim;
    public float sideBuffer;
    public bool inputCheck;
    public float bufferTimer;
    public bool ableToFire;
    public float fireTimer;
    public float maxBuffer;

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnReadyPress()
    {
        inputCheck = !inputCheck;
        if(inputCheck == false)
        {
            if (readyToFire != true)
            {
                readyToFire = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(fire == true)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= 1f)
            {
                fireTimer = 0.0f;
                fire = false;
            }
        }
        if (readyToFire == true && fire == false)
        {
            chargeTimer += Time.deltaTime;

            CheckMouse();
        }
    }

    public void CheckMouse()
    {
        Vector2 currentInput = mouseLook;

        bufferTimer += Time.deltaTime;
        if(bufferTimer >= maxBuffer)
        {
            ableToFire = true;
        }

        if(checkCount < 1)
        {
            checkCount++;
            lastInput = currentInput;
        }
        else
        {
            if(lastInput.y <= currentInput.y || chargeTimer >= maxCharge && ableToFire == true)
            {
                Fire();
                checkCount = 0;
                chargeTimer = 0f;
                fire = true;
                readyToFire = false;
                aimingLeft = false;
                aimingRight = false;
                inputCheck = false;
                ableToFire = false;
                bufferTimer = 0.0f;
            }
            else
            {
                if (lastInput.x - currentInput.x >= sideBuffer)
                {
                    aimingRight = true;
                    aimingLeft = false;
                    sideAimTimer = 0f;
                } else if(lastInput.x - currentInput.x <= -sideBuffer)
                {
                    aimingRight = false;
                    aimingLeft = true;
                    sideAimTimer = 0f;
                } else if(aimingLeft == true || aimingRight == true)
                {
                    sideAimTimer += Time.deltaTime;
                    if(sideAimTimer >= maxAimTimer)
                    {
                        aimingRight = false;
                        aimingLeft = false;
                    }
                }
                lastInput = currentInput;
            }
        }
    }

    public void Fire()
    {
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Vector3 launchVector = centerAim.position;

        launchVector.x -= (lastInput.x - 365f) / 6f;
        /*
        if(aimingLeft == true)
        {
            launchVector = leftAim.position;
        } else if(aimingRight == true)
        {
            launchVector = rightAim.position;
        }
        */
        launchVector -= bullet.transform.position;
        bullet.GetComponent<Bullet>().launchVector = launchVector;
        bullet.GetComponent<Bullet>().multi = chargeTimer * 4;
    }
}
