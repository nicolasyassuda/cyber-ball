using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float speed = 10f; // Velocidade do obstáculo
    public Vector3 direction = new Vector3 (0, 1, 0);
    int reverse = -1;
    void Update()
    {
        if (transform.position.y >= -15)
        {
            reverse = -1;
        }
        else if (transform.position.y <= -17)
        {
            reverse = 1;
        }
        transform.Translate(reverse * direction * speed * Time.deltaTime);

    }
}
