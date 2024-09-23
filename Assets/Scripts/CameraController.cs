using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O carro que a c�mera vai seguir
    public Vector3 offset = new Vector3(0, 10, -5); // Dist�ncia relativa da c�mera em rela��o ao carro
    public float followSpeed = 5f; // Velocidade para a c�mera seguir o carro

    // Rota��o fixa para corrigir o �ngulo da c�mera (90 graus no eixo Y)
    private Quaternion fixedRotation = Quaternion.Euler(45, 180, 0);

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posi��o desejada da c�mera com base na rota��o do carro
            Vector3 desiredPosition = target.TransformPoint(offset);

            // Movimenta a c�mera suavemente para a posi��o desejada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Faz a c�mera girar junto com o carro, mas com uma rota��o fixa de -90 graus no eixo Y
            Quaternion desiredRotation = target.rotation * fixedRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, followSpeed * Time.deltaTime);
        }
    }
}
