using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Target { get; set; }
    private const float speed = 1.5f;

    private void Awake()
    {
        Trail.OnValueUpdated += (value) =>
        {
            Target = new Vector3(value.x - 2.0f, transform.position.y, transform.position.z);
        };
    }

    private void OnDestroy()
    {
        Trail.OnValueUpdated = null;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
    }
}
