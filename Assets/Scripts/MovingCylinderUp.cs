using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCylinderUp : MonoBehaviour
{
    public float speed = 10f; // Velocidade do obst�culo
    public Vector3 direction = Vector3.up; // Dire��o do movimento
    int reverse = 1;
    void Update()
    {
        if(transform.position.y >= 0.8) {
            reverse = -1;
        }
        else if (transform.position.y <= -5)
        {
            reverse = 1;
        }
        transform.Translate(reverse*direction * speed * Time.deltaTime);

    }
}
