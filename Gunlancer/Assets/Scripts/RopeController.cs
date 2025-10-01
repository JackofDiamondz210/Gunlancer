using UnityEngine;

public class RopeController : MonoBehaviour
{

    public GameObject target1; 
    public GameObject target2;
    public LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, target1.transform.position);
        lineRenderer.SetPosition(1, target2.transform.position);
    }
}
