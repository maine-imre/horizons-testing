using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public List<MeshRenderer> shapes;
    public MeshRenderer cShape;

    public void Start()
    {
        for (int i = 0; i < shapes.Count; i++)
            shapes[i].enabled = false;
        cShape = shapes[0];
        cShape.enabled = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cShape = shapes[(shapes.IndexOf(cShape) + 1) % shapes.Count];
            shapes.ForEach(s => s.enabled = false);
            cShape.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cShape = shapes[(shapes.IndexOf(cShape) - 1) % shapes.Count];
            shapes.ForEach(s => s.enabled = false);
            cShape.enabled = true;
        }
    }   

}
