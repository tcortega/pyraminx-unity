using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class CriaTriangulo : MonoBehaviour
{
    public bool verticesCompartilhados = false;

    // Vértices do tetraedro
    Vector3 v0 = new Vector3(0, 0, 0);
    Vector3 v1 = new Vector3(1, 0, 0);
    Vector3 v2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
    Vector3 v3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);

    // Cria um novo objeto Mesh
    Mesh mesh;

    // Retorna um array de vértices
    public Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[] { v0, v1, v2, v3 };
        return vertices;
    }

    public void Start()
    {
        // Obtém o componente MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // Verifica se o MeshFilter foi encontrado
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter não encontrado!");
            return;
        }

        // Verifica se a mesh existe
        if (mesh == null)
        {
            // Cria uma nova mesh
            meshFilter.mesh = new Mesh();
        }

        // Define que as modificações serão refletidas em todas as instâncias da mesh
        mesh = meshFilter.sharedMesh;

        // Limpa todos os dados da mesh
        mesh.Clear();

        // Se compartilhar vértices, cria uma topologia diferente
        if (verticesCompartilhados)
        {
            mesh.vertices = new Vector3[] { v0, v1, v2, v3 };

            mesh.triangles = new int[]{
                0, 1, 2,
                0, 2, 3,
                2, 1, 3,
                0, 3, 1
            };

            mesh.uv = new Vector2[]{
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
            };
        }
        else
        {
            mesh.vertices = new Vector3[]{
                v0, v1, v2,
                v0, v2, v3,
                v2, v1, v3,
                v0, v3, v1
            };

            // 3 Vértices por face
            mesh.triangles = new int[]{
                0, 1, 2,
                3, 4, 5,
                6, 7, 8,
                9, 10, 11
            };

            Vector2 uv0 = new Vector2(0, 0);
            Vector2 uv1 = new Vector2(1, 0);
            Vector2 uv2 = new Vector2(0.5f, 1);

            mesh.uv = new Vector2[]{
                uv0, uv1, uv2,
                uv1, uv0, uv1,
                uv2, uv1, uv0,
                uv1, uv2, uv0
            };
        }

        // Define as cores para cada vértice
        Color[] color = new Color[mesh.vertices.Length];

        color[0] = new Color32(255, 0, 0, 255); // Vermelho
        color[1] = new Color32(255, 0, 0, 255);
        color[2] = new Color32(255, 0, 0, 255);

        color[3] = new Color32(0, 0, 255, 255); // Azul
        color[4] = new Color32(0, 0, 255, 255);
        color[5] = new Color32(0, 0, 255, 255);

        color[6] = new Color32(0, 255, 0, 255); // Verde
        color[7] = new Color32(0, 255, 0, 255);
        color[8] = new Color32(0, 255, 0, 255);

        color[9] = new Color32(255, 165, 0, 255); // Laranja
        color[10] = new Color32(255, 165, 0, 255);
        color[11] = new Color32(255, 165, 0, 255);

        mesh.colors = color;

        // Recalcula as normais e os limites da mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Otimiza a mesh para melhorar o desempenho
        mesh.Optimize();
    }
}
