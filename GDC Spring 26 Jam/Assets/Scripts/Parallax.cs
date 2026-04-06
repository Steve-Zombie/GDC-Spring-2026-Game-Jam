using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void Update()
    {
        Vector3 camDelta = cam.position - lastCamPos;

        // only move vertically when camera moves up (camDelta.y > 0)
        float verticalDelta = Mathf.Max(0, camDelta.y);

        transform.position += new Vector3(camDelta.x * horizontalSpeed, verticalDelta * verticalSpeed, 0);

        lastCamPos = cam.position;
    }
}