using UnityEngine;
using System.Collections;

public class CreateMesh : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        CreatePlane();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreatePlane()
    {
        GameObject plane = new GameObject();
        plane.name = "plane";
        //  添加组件
        MeshFilter mfilter = plane.AddComponent<MeshFilter>();
        MeshRenderer render = plane.AddComponent<MeshRenderer>();
        // 添加默认的材质
        render.material = new Material(Shader.Find("Diffuse"));
        //mfilter.mesh  = new Mesh ();
        Mesh mesh = mfilter.mesh;
        //  设置三个顶点
        mesh.vertices = new Vector3[] {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (1, 1, 0)
        };
        //  设置三角形 （面）  这里 三个点的顺序 应该有（顺时针法则）
        mesh.triangles = new int[]{
            0,1,2
        };
        // 重新计算法线
        mesh.RecalculateNormals();
    }
}
