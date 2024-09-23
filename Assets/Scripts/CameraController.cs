using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O carro que a câmera vai seguir
    public Vector3 offset = new Vector3(0, 10, -5); // Distância relativa da câmera em relação ao carro
    public float followSpeed = 5f; // Velocidade para a câmera seguir o carro

    // Rotação fixa para corrigir o ângulo da câmera (90 graus no eixo Y)
    private Quaternion fixedRotation = Quaternion.Euler(45, 180, 0);

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição desejada da câmera com base na rotação do carro
            Vector3 desiredPosition = target.TransformPoint(offset);

            // Movimenta a câmera suavemente para a posição desejada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Faz a câmera girar junto com o carro, mas com uma rotação fixa de -90 graus no eixo Y
            Quaternion desiredRotation = target.rotation * fixedRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, followSpeed * Time.deltaTime);
        }
    }
}
