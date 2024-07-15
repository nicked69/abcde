using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private bool isDragging = false;
    private Rigidbody rb;
    public float draggingForce = 700f; // �巡�� �� ����
    public float yOffset = 0.5f; // �ٴڰ��� �Ÿ� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = 10; // ���� ���� ����
        rb.constraints = RigidbodyConstraints.FreezeRotation; // ȸ�� ����
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPos() + mOffset;
            targetPosition.y = Mathf.Max(targetPosition.y, yOffset); // �ּ� ���� ����
            Vector3 force = (targetPosition - rb.position) * draggingForce;
            rb.AddForce(force);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}