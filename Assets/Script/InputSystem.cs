using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class InputSystem : ComponentSystem
{

    private struct Data
    {
        public readonly int Length;
        public ComponentArray<InputComponent> InputComponents;
    }

    [Inject] private Data _data;

    protected override void OnUpdate()
    {
        var horizontal = Input.mousePosition.x;

        var Vertical = Input.mousePosition.y;

        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < _data.Length; i++)
            {
                Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(horizontal, Vertical, 10.0f));


                _data.InputComponents[i].Mousepoints.Add(temp);

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < _data.Length; i++)
            {
                createpoint(i);
            }





            //GameObject plane = new GameObject();
            //plane.name = "plane";
            ////  添加组件
            //MeshFilter mfilter = plane.AddComponent<MeshFilter>();
            //MeshRenderer render = plane.AddComponent<MeshRenderer>();
            //// 添加默认的材质
            //render.material = new Material(Shader.Find("Diffuse"));
            ////mfilter.mesh  = new Mesh ();
            //Mesh mesh = mfilter.mesh;
            ////  设置三个顶点
            //for (int i = 0; i < _data.Length; i++)
            //{
            //    mesh.vertices = _data.InputComponents[i].MeshPoints.ToArray();
            //}
            ////  设置三角形 （面）  这里 三个点的顺序 应该有（顺时针法则）
            //int[] temp = new int[Mathf.FloorToInt(mesh.vertices.Length / 3) * 3];

            //for (int i = 0; i < Mathf.FloorToInt(mesh.vertices.Length/3)*3; i++)
            //{
            //    temp[i] = i;
            //}
            //mesh.triangles = temp;
            //// 重新计算法线
            //mesh.RecalculateNormals();


            // clean List
            for (int i = 0; i < _data.Length; i++)
            {
                foreach (var item in _data.InputComponents[i].MeshPoints)
                {
                   GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.position =item;
                }

                _data.InputComponents[i].MeshPoints.Clear();
                _data.InputComponents[i].Mousepoints.Clear();
            }
        }


    }


    void createpoint(int i)
    {
        if (_data.InputComponents[i].Mousepoints.Count > 2)
        {
            for (int j = 0; j < _data.InputComponents[i].Mousepoints.Count - 1; j++)
            {

                Vector3 Mousepoint1 = _data.InputComponents[i].Mousepoints[j];
                Vector3 Mousepoint2 = _data.InputComponents[i].Mousepoints[j + 1];

                if (Mousepoint2.magnitude - Mousepoint1.magnitude > .01)
                {
                    float point1x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y);
                    float point1y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x);
                    Vector3 point1 = new Vector3(point1x, point1y, 10);
                    _data.InputComponents[i].MeshPoints.Add(point1);

                    float point2x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y);
                    float point2y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x);
                    Vector3 point2 = new Vector3(point2x, point2y, 10);
                    _data.InputComponents[i].MeshPoints.Add(point2);

                    float point3x = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + (Mousepoint2.x - Mousepoint1.x);
                    float point3y = ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + (Mousepoint2.y - Mousepoint1.y);
                    Vector3 point3 = new Vector3(point3x, point3y, 10);
                    _data.InputComponents[i].MeshPoints.Add(point3);

                    float point4x = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.y - Mousepoint1.y) + (Mousepoint2.x - Mousepoint1.x);
                    float point4y = -ValueSheet.DrawWidth / ((Mousepoint2 - Mousepoint1).magnitude) * (Mousepoint2.x - Mousepoint1.x) + (Mousepoint2.y - Mousepoint1.y);
                    Vector3 point4 = new Vector3(point4x, point4y, 10);
                    _data.InputComponents[i].MeshPoints.Add(point4);
                }
            }
        }
    }
}
