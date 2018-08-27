using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateMeshSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<InputComponent> inputComponents;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        var horizontal = Input.mousePosition.x;

        var Vertical = Input.mousePosition.y;

        for (int i = 0; i < data.Length; i++)
        {
            if (Input.GetMouseButton(0))
            {
                SetMousePosition(horizontal, Vertical, i, data);
            }
            else if (Input.GetMouseButtonUp(0)) {
           
                DrawMesh(i, data);

            }
        }
    }

    private void SetMousePosition(float _Horizontal, float _Vertical, int i, Data _data) {
        Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(_Horizontal, _Vertical, 10.0f));

        if ((temp - _data.inputComponents[i].pervious).magnitude > .5f)
        {
            _data.inputComponents[i].Mousepoints.Add(temp);
            _data.inputComponents[i].pervious = temp;
        }
    }

    private void DrawMesh(int m,Data _data) {
        if (_data.inputComponents[m].Mousepoints.Count > 2)
        {
            //-------------------------------creeatepoints--------------------------------
            for (int j = 0; j < _data.inputComponents[m].Mousepoints.Count-1; j++)
            {
                
                Vector3 Mousepoint1 = _data.inputComponents[m].Mousepoints[j];
                Vector3 Mousepoint2 = _data.inputComponents[m].Mousepoints[j + 1];
                if (j == 0)
                {
                    float point1x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x;
                    float point1y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y;


                    Vector3 point1 = new Vector3(point1x, point1y, 10);
                    _data.inputComponents[m].MeshPoints.Add(point1);

                    float point2x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x;
                    float point2y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y;
                    Vector3 point2 = new Vector3(point2x, point2y, 10);
                    _data.inputComponents[m].MeshPoints.Add(point2);
                }
                float point3x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x + (Mousepoint2.x - Mousepoint1.x);
                float point3y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y + (Mousepoint2.y - Mousepoint1.y);

                Vector3 point3 = new Vector3(point3x, point3y, 10);
                _data.inputComponents[m].MeshPoints.Add(point3);

                float point4x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + Mousepoint1.x + (Mousepoint2.x - Mousepoint1.x);
                float point4y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + Mousepoint1.y + (Mousepoint2.y - Mousepoint1.y);
                Vector3 point4 = new Vector3(point4x, point4y, 10);
                _data.inputComponents[m].MeshPoints.Add(point4);
            }
            //-------------------------------------------------------------------------

            //--------------------------DrawMesh-------------------------------------
            GameObject plane = new GameObject();
            plane.name = "plane";
            //  添加组件
            MeshFilter mfilter = plane.AddComponent<MeshFilter>();
            MeshRenderer render = plane.AddComponent<MeshRenderer>();
            // 添加默认的材质
            render.material = new Material(Shader.Find("Diffuse"));
            // render.material = new Material(Shader.Find("UI/Default"));
            //mfilter.mesh  = new Mesh ();
            Mesh mesh = mfilter.mesh;
            //  设置三个顶点
            for (int j = 0; j < _data.inputComponents[m].Mousepoints.Count; j++)
            {
                mesh.vertices = _data.inputComponents[m].MeshPoints.ToArray();
            }
            //  设置三角形 （面）  这里 三个点的顺序 应该有（顺时针法则）
            int[] VerticesNum = new int[(mesh.vertices.Length - 2) * 3];
            int currentVertices = 0;
            //Debug.Log("VerticesNum length :"+VerticesNum.Length.ToString());

            //Debug.Log("length" +((mesh.vertices.Length - 2) / 2));
            int count = 0;
            for (int i = 0; i < (mesh.vertices.Length - 2) / 2; i++)
            {
                //Debug.Log("i:" + i);

                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count + 1;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count + 2;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count + 3;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count + 2;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                VerticesNum[currentVertices++] = count + 1;
                //Debug.Log("currentVertices" + currentVertices.ToString());
                count = count + 2;

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
            plane.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        for (int j = 0; j < _data.inputComponents[m].Mousepoints.Count; j++)
        {
            _data.inputComponents[m].MeshPoints.Clear();
            _data.inputComponents[m].Mousepoints.Clear();
        }
    }



}
