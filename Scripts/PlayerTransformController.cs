using UnityEngine;

public class PlayerTransformController : MonoBehaviour
{
    [Header("Movement (W A S D)")]
    public float moveSpeed = 5f;

    [Header("Rotation (X)")]
    public float rotateSpeed = 90f;

    [Header("Scale (Y)")]
    public float scaleSpeed = 0.5f;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    public Vector3 StartPosition { get; private set; }
    public Vector3 StartScale { get; private set; }
    public float TotalRotation { get; private set; }
    public float TotalDistance { get; private set; }
    public bool Growing { get; private set; } = true;

    Quaternion startRotation;

    void Start()
    {
        StartPosition = transform.position;
        StartScale = transform.localScale;
        startRotation = transform.rotation;
    }

    void Update()
    {
        HandleMove();
        HandleRotate();
        HandleScale();

        if (Input.GetKeyDown(KeyCode.R)) ResetTransform();
    }

    void HandleMove()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        Vector3 delta = dir * moveSpeed * Time.deltaTime;
        transform.position += delta;
        TotalDistance += delta.magnitude;
    }

    void HandleRotate()
    {
        if (!Input.GetKey(KeyCode.X)) return;
        float step = rotateSpeed * Time.deltaTime;
        transform.Rotate(0f, step, 0f);
        TotalRotation += step;
    }

    void HandleScale()
    {
        if (Input.GetKeyDown(KeyCode.Y)) Growing = !Growing;
        if (!Input.GetKey(KeyCode.Y)) return;

        float factor = transform.localScale.x / StartScale.x;
        factor += (Growing ? 1f : -1f) * scaleSpeed * Time.deltaTime;
        factor = Mathf.Clamp(factor, minScale, maxScale);
        transform.localScale = StartScale * factor;
    }

    public float ScaleFactor => transform.localScale.x / StartScale.x;

    public void ResetTransform()
    {
        transform.position = StartPosition;
        transform.rotation = startRotation;
        transform.localScale = StartScale;
        TotalRotation = 0f;
        TotalDistance = 0f;
        Growing = true;
    }
}