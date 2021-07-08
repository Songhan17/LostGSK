using UnityEngine;

public class CubeViewDrawLine : MonoBehaviour
{

    private LineRenderer lineLeft;
    private LineRenderer lineRight;
    private Vector3 leftLineEndPoint;
    private Vector3 rightLinEndPoint;

    private void Start()
    {
        //CreateLine(ref lineLeft, "LeftLine");
        //CreateLine(ref lineRight, "RightLine");
    }

    private void Update()
    {
        //GetLineEndPoint(leftLineEndPoint, lineLeft, -40);
        //GetLineEndPoint(rightLinEndPoint, lineRight, 40);
        Ray ray = new Ray(transform.position, new Vector2(-transform.localScale.x,0));

        Debug.DrawRay(ray.origin, ray.direction * 5, Color.black);
        Debug.DrawRay(ray.origin, -ray.direction * 5, Color.blue);

    }

    /// <summary>
    /// 获取范围的两个终点，并画线
    /// </summary>
    /// <param name="v"></param>
    /// <param name="line"></param>
    /// <param name="angle"></param>
    public void GetLineEndPoint(Vector3 v, LineRenderer line, float angle)
    {
        v = (Quaternion.Euler(0, angle, 0) * transform.forward) * 10 + transform.position;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, v);
    }

    /// <summary>
    ///创建LineRenedere并且赋值设置属性，需要传递引用
    /// </summary>
    /// <param name="line"></param>
    /// <param name="name"></param>
    public void CreateLine(ref LineRenderer line, string name)
    {
        GameObject go = new GameObject();
        go.name = name;
        line = go.AddComponent<LineRenderer>();
        line.sharedMaterial = Resources.Load<Material>("Line");
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material.color = Color.red;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position, 5);

    }

}
