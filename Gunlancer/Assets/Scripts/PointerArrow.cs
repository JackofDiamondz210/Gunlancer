using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    //player, target, and offset above players head
    public Transform player;     
    public Transform target;      
    public Vector3 offset = new Vector3(0, 2f, 0); 

    public RectTransform canvasRect; 
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {

        //convert world position to canvas position
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.position + offset);
        Vector2 canvasPos = new Vector2(
            (viewportPos.x - 0.5f) * canvasRect.sizeDelta.x,
            (viewportPos.y - 0.5f) * canvasRect.sizeDelta.y);

        rectTransform.anchoredPosition = canvasPos;

        //rotate arrow to point at target
        Vector2 direction = target.position - (player.position + offset); //getting targets direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //getting angle of target
        rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90f); //adjusting arrow to point at target
    }
}
