using UnityEngine;

public class RopeController : MonoBehaviour
{
    //setting up targets and renderer for rope
    public GameObject target1; 
    public GameObject target2;
    public LineRenderer lineRenderer; //renders rope

    //Offsetting rope so its not connect to knights masks
    public Vector3 offset = new Vector3(0, -0.5f, 0); 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setting up 2 positions for the rope
        lineRenderer.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        //rope will update with playerspositions as they move
        lineRenderer.SetPosition(0, target1.transform.position + offset);
        lineRenderer.SetPosition(1, target2.transform.position + offset);
    }
}
