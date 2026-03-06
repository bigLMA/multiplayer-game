using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private float baseAspect = 16f / 9f;
    private float fov;

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        fov = camera.fieldOfView;

        float currectAspect = camera.aspect;
        camera.fieldOfView = fov*(baseAspect/currectAspect);
    }
}
