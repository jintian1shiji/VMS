using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelector : MonoBehaviour {

    Material _mat;
    bool _select = false;
    Vector3 _p1;
    Vector3 _p2;
    GameObject _cubeObj;
    Color _srcColor;
    Renderer _render;

    // Use this for initialization
    void Start()
    {
        if (!_mat)
        {
            //_mat = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + " ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
            _mat = new Material(Shader.Find("Transparent/Diffuse"));
            _mat.color = Color.green;
            //material.SetVector("_Color",new Vector4(1,1,1,1));
            GetComponent<Renderer>().material = _mat;
        }

        _cubeObj = GameObject.Find("BoxSelect");
        _render = _cubeObj.GetComponent<Renderer>();
        _srcColor = _render.material.color;

        Bounds bounds = _cubeObj.GetComponent<BoxCollider>().bounds;
    }

    void OnDestroy()
    {
        Destroy(_mat);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _select = true;
            _p1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _p2 = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _select = false;
            Vector3 center = (_p1 + _p2) * 0.5f;
            Vector3 size = new Vector3(Mathf.Abs(_p1.x - _p2.x), Mathf.Abs(_p1.y - _p2.y), 0);
            Bounds bounds = new Bounds(center, size);

            BoxTest(bounds);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ray = new Vector3(Input.mousePosition.x, Input.mousePosition.y, (_cubeObj.transform.position - Camera.main.transform.position).z);
            Vector3 pos = Camera.main.ScreenToWorldPoint(ray);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject != _cubeObj)
                {
                    _render.material.color = _srcColor;
                }
            }
            else
            {
                _render.material.color = _srcColor;
            }
        }
    }

    void DrawBox(Vector3 p1, Vector3 p2)
    {
        _mat.SetPass(0);
        GL.PushMatrix();
        GL.LoadPixelMatrix();
        GL.Begin(GL.LINES);
        GL.Vertex(p1);
        GL.Vertex3(p1.x, p2.y, 0);
        GL.Vertex3(p1.x, p2.y, 0);
        GL.Vertex(p2);
        GL.Vertex(p2);
        GL.Vertex3(p2.x, p1.y, 0);
        GL.Vertex3(p2.x, p1.y, 0);
        GL.Vertex(p1);
        GL.End();
        GL.PopMatrix();
    }


    void OnPostRender()
    {
        if (_select)
        {
            DrawBox(_p1, _p2);
        }
    }

    void BoxTest(Bounds bounds)
    {
        if (_cubeObj)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(_cubeObj.transform.position);
            pos.z = bounds.center.z;
            if (bounds.Contains(pos))
            {
                _render.material.color = Color.blue;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (_cubeObj != null)
        {
            Bounds bounds = _cubeObj.GetComponent<BoxCollider>().bounds;
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(_cubeObj.transform.position, bounds.size);
        }
    }
}
