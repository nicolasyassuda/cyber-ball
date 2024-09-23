using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float realMovementX;
    private bool isGrounded = true;
    private float movementY;
    public TextMeshProUGUI timerText;

    private float timer;
    private bool isRunning = true; 

    public float speed = 0;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject tryAgainButton;

    public AudioSource audioSource;

    public Transform[] frontWheels;
    public Transform[] rearWheels;

    public float steeringSpeed = 100f;
    public float maxSteeringAngle = 30f;
    private float currentSteeringAngle = 0f;
    public float groundCheckDistance = 1.5f;

    public int milliseconds;
    public int seconds;
    public int minutes;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
        timer = 0f;
        winTextObject.SetActive(false);

    }
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;

            minutes = Mathf.FloorToInt(timer / 60F);
            seconds = Mathf.FloorToInt(timer % 60F);
            milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

            if (rb.position.y <= -16)
            {
                setTryAgain();
            }
        }
    }
    private void FixedUpdate()
    {

        CheckGrounded();

        if (isGrounded)
        {
            Vector3 movement = -frontWheels[0].forward * movementY;

            rb.AddForce(movement * speed);

        }

        RotateCar();
        RotateWheels();

        AlignToSurface();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        realMovementX = movementVector.x;

        if (movementVector.y > -0.5f)
        {
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        else
        {
            movementX = -movementVector.x;
            movementY = movementVector.y;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();


    }

    void setTryAgain()
    {
        tryAgainButton.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count = count + 1;

            audioSource.Play();

            other.gameObject.SetActive(false);
            SetCountText();
            timer -= 1;
        }
    }

    void RotateCar()
    {
        float turnAngle = movementX * steeringSpeed * Time.deltaTime;

        Quaternion deltaRotation = Quaternion.Euler(0f, turnAngle*0.75f, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }


    void RotateWheels()
    {
        if (movementX != 0)
        {
            
            currentSteeringAngle += realMovementX * steeringSpeed * Time.deltaTime;
            currentSteeringAngle = Mathf.Clamp(currentSteeringAngle, -maxSteeringAngle, maxSteeringAngle);
        }
        else
        {
            currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, 0, Time.deltaTime * steeringSpeed*0.1f);
        }

        foreach (Transform wheel in frontWheels)
        {
            wheel.localRotation = Quaternion.Euler(0, currentSteeringAngle, 0);
        }
    }

    void AlignToSurface()
    {
        RaycastHit hit;
        Vector3[] raycastOrigins = new Vector3[]
        {
        frontWheels[0].position + Vector3.up * 0.5f,
        frontWheels[1].position + Vector3.up * 0.5f,
        rearWheels[0].position + Vector3.up * 0.5f,
        rearWheels[1].position + Vector3.up * 0.5f   
        };

        Vector3 averageNormal = Vector3.zero;
        int hitCount = 0;

        foreach (Vector3 origin in raycastOrigins)
        {
            if (Physics.Raycast(origin, Vector3.down, out hit, 2f))
            {
                averageNormal += hit.normal;
                hitCount++;
            }
        }

        if (hitCount > 0)
        {
            averageNormal /= hitCount;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, averageNormal) * transform.rotation;

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 5f));
        }
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.collider.CompareTag("Win"))
            {
                winTextObject.SetActive(true);
                tryAgainButton.SetActive(true);
                PauseTimer();
            }
            else
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        timer = 0f;
        isRunning = true;
    }

}
