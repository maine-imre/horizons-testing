using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    //list of meshrenderers for shapes
    //meshrenderer for active shape
    public List<MeshRenderer> shapes;
    public MeshRenderer cShape;

    public void Start()
    {
        //loop through list and make each shape invisible
        //initialize first shape to visible
        for (int i = 0; i < shapes.Count; i++)
            shapes[i].enabled = false;
        cShape = shapes[0];
        cShape.enabled = true;
    }

    public void Update()
    {
        //if right arrow pressed, go down list (wrap around at end)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cShape = shapes[(shapes.IndexOf(cShape) + 1) % shapes.Count];
            shapes.ForEach(s => s.enabled = false);
            cShape.enabled = true;
        }

        //if left arrow pressed, go back up list
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cShape = shapes[(shapes.IndexOf(cShape) - 1) % shapes.Count];
            shapes.ForEach(s => s.enabled = false);
            cShape.enabled = true;
        }
    }   

}
