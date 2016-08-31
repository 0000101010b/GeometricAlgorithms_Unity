using UnityEngine;
using System.Collections;

public class SierpinskiTriangle : MonoBehaviour {

    public int size;
    public int n;
    SierpinskiTriangleElement root;


    // Use this for initialization
    void Start () {
        root = new SierpinskiTriangleElement(n, transform.position, size);
	}

    void OnDrawGizmos()
    {
        if(root!=null)
            Draw(root);
    }
    void Draw(SierpinskiTriangleElement t)
    {
        //Gizmos.DrawSphere(t.center, t.size);
        Gizmos.DrawLine(t.center+new Vector2(0,(t.size / 2)),new Vector2(-t.size/2,-t.size/2)+t.center);
        Gizmos.DrawLine(t.center + new Vector2(0, (t.size / 2)), new Vector2(t.size / 2, -t.size / 2) + t.center);
        Gizmos.DrawLine(new Vector2(-t.size / 2, -t.size / 2) + t.center, new Vector2(t.size / 2, -t.size / 2) + t.center);
        if (t.left != null)
        {
            Draw(t.left);
            Draw(t.right);
            Draw(t.top);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
public class SierpinskiTriangleElement
{
    public Vector2 center;
    public float size;

    public SierpinskiTriangleElement left;
    public SierpinskiTriangleElement right;
    public SierpinskiTriangleElement top;

    public SierpinskiTriangleElement(int n,Vector2 _center,float _size)
    {
        center = _center;
        size=_size;
        if(n>0)
        {
            n--;
            _size = _size/2;
            left = new SierpinskiTriangleElement(n, new Vector2(-_size/2,-_size/2)+center, _size);
            right = new SierpinskiTriangleElement(n, new Vector2(_size/2, -_size/2) + center,_size);
            top = new SierpinskiTriangleElement(n, new Vector2(0, _size/2) + center, _size);

        }
    }
}
