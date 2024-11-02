using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    [SerializeField] private float parallaxMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;
    }
}
