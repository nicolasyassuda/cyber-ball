using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CurvedTrackGenerator : MonoBehaviour
{
    public int segments = 20; // N�mero de segmentos para definir a curvatura da pista
    public float radius = 5f; // Raio da curva da pista
    public float trackWidth = 2f; // Largura da pista
    public float trackHeight = 0.1f; // Altura da pista

    void Update()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Definindo os v�rtices da pista
        Vector3[] vertices = new Vector3[(segments + 1) * 4]; // Segmentos + 1, 4 v�rtices por segmento (2 para parte inferior e 2 para superior)

        // Definindo os tri�ngulos da pista
        int[] triangles = new int[segments * 6]; // Cada segmento tem 2 tri�ngulos (3 v�rtices cada)

        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI / 2; // �ngulo para definir a curva (ajustar para mais curvatura)

            // Calcula a posi��o de cada v�rtice
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Define os v�rtices para a linha externa e interna
            vertices[vertIndex] = new Vector3(x, 0, z); // Linha externa inferior
            vertices[vertIndex + 1] = new Vector3(x, trackHeight, z); // Linha externa superior
            vertices[vertIndex + 2] = new Vector3(x - Mathf.Cos(angle) * trackWidth, 0, z - Mathf.Sin(angle) * trackWidth); // Linha interna inferior
            vertices[vertIndex + 3] = new Vector3(x - Mathf.Cos(angle) * trackWidth, trackHeight, z - Mathf.Sin(angle) * trackWidth); // Linha interna superior

            // Define os tri�ngulos
            if (i < segments)
            {
                // Parte superior da pista
                triangles[triIndex] = vertIndex;
                triangles[triIndex + 1] = vertIndex + 2;
                triangles[triIndex + 2] = vertIndex + 1;

                triangles[triIndex + 3] = vertIndex + 1;
                triangles[triIndex + 4] = vertIndex + 2;
                triangles[triIndex + 5] = vertIndex + 3;

                triIndex += 6;
            }

            vertIndex += 4;
        }

        // Aplicando v�rtices e tri�ngulos ao mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
