using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TCreateMesh : MonoBehaviour {
    public List<Vector3> Mousepoints = new List<Vector3>();
    public List<Vector3> MeshPoints = new List<Vector3>();

    Vector3 pervious;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ValueSheet.b_CreateMesh) {
            var horizontal = Input.mousePosition.x;

            var Vertical = Input.mousePosition.y;


            if (Input.GetMouseButton(0))
            {
                Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(horizontal, Vertical, 10.0f));
                if ((temp - pervious).magnitude > .5f)
                {
                    Mousepoints.Add(temp);
                    pervious = temp;
                }
            }

            else if (Input.GetMouseButtonUp(0))
            {
                if (Mousepoints.Count >= 2)
                {
                    createpoint();
                    //---------------------------------DrawCheckSphere
                    //int num = 0;
                    //foreach (var item in MeshPoints)
                    //{
                    //    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //    gameObject.transform.localScale = .2f * Vector3.one;
                    //    gameObject.transform.position = item;
                    //    gameObject.name = num.ToString();
                    //    num++;
                    //}
                    //----------------------------------
                    DrawMesh();
                }

                MeshPoints.Clear();
                Mousepoints.Clear();
                ValueSheet.b_CreateMesh = false;
            }
        }  
    }

    void DrawMesh() {
        GameObject plane = new GameObject();
        plane.name = "plane";
        plane.layer = LayerMask.NameToLayer("Collision");

        //  添加组件
        MeshFilter mfilter = plane.AddComponent<MeshFilter>();
        MeshRenderer render = plane.AddComponent<MeshRenderer>();
        // 添加默认的材质
        render.material = new Material(Shader.Find("Diffuse"));
       // render.material = new Material(Shader.Find("UI/Default"));
        //mfilter.mesh  = new Mesh ();
        Mesh mesh = mfilter.mesh;
        //  设置三个顶点

        mesh.vertices =MeshPoints.ToArray();

        //  设置三角形 （面）  这里 三个点的顺序 应该有（顺时针法则）
        int[] VerticesNum = new int[(mesh.vertices.Length - 2) * 3];
        int currentVertices = 0;
        //Debug.Log("VerticesNum length :"+VerticesNum.Length.ToString());

        //Debug.Log("length" +((mesh.vertices.Length - 2) / 2));
        int  j = 0;
        for (int i = 0; i < (mesh.vertices.Length -2)/2; i++)
        {
            //Debug.Log("i:" + i);

            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] = j;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] =j + 1;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] =j + 2;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] = j + 3;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] = j + 2;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            VerticesNum[currentVertices++] = j+ 1;
            //Debug.Log("currentVertices" + currentVertices.ToString());
            j = j + 2;

        }
        mesh.triangles = VerticesNum;
        // 重新计算法线
       mesh.RecalculateNormals();
     
        List<Vector2> tempPoints = new List<Vector2>();
        List<Vector2> EvenPoints = new List<Vector2>();
        List<Vector2> OddPoints = new List<Vector2>();

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if (i % 2 == 0)
            {
                EvenPoints.Add(new Vector2(mesh.vertices[i].x, mesh.vertices[i].y));
            }
            else
            {
                OddPoints.Add(new Vector2(mesh.vertices[i].x, mesh.vertices[i].y));
            }
        }
        OddPoints.Reverse();

        tempPoints.AddRange(EvenPoints);
        tempPoints.AddRange(OddPoints);



        plane.AddComponent<PolygonCollider2D>().points = tempPoints.ToArray();
        plane.AddComponent<Rigidbody2D>();
        plane.AddComponent<DestoryObject>();
        plane.GetComponent<DestoryObject>().EmitMesh = mesh;
        plane.GetComponent<Rigidbody2D>().isKinematic = true;

    }


    void createpoint()
    {
        if (Mousepoints.Count > 2)
        {
            for (int j = 0; j <Mousepoints.Count - 1; j++)
            {

                Vector3 Mousepoint1 =Mousepoints[j];
                Vector3 Mousepoint2 =Mousepoints[j + 1];
                if (j == 0) {
                    float point1x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x;
                    float point1y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y;


                    Vector3 point1 = new Vector3(point1x, point1y, 10);
                    MeshPoints.Add(point1);

                    float point2x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x;
                    float point2y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y;
                    Vector3 point2 = new Vector3(point2x, point2y, 10);
                    MeshPoints.Add(point2);
                }
                    float point3x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x + (Mousepoint2.x - Mousepoint1.x);
                  float point3y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y + (Mousepoint2.y - Mousepoint1.y);

                    Vector3 point3 = new Vector3(point3x, point3y, 10);
                   MeshPoints.Add(point3);

                    float point4x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x + (Mousepoint2.x - Mousepoint1.x);
                float point4y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y + (Mousepoint2.y - Mousepoint1.y);
                Vector3 point4 = new Vector3(point4x, point4y, 10);
                  MeshPoints.Add(point4);

            }
        }
    }
}
