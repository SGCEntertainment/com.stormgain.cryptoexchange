using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class Trail : MonoBehaviour
{
    private Vector3 Target { get; set; }
    private TrailRenderer TrailRenderer { get; set; }
    public static Action<Vector3> OnValueUpdated { get; set; }

    private void Start()
    {
        StartCoroutine(nameof(AddNewPosition));
    }

    private IEnumerator AddNewPosition()
    {
        while(true)
        {
            Vector3 init = transform.localPosition;
            Target = new Vector3(transform.localPosition.x + 0.25f, transform.localPosition.y + Random.Range(-0.4f, 0.4f), transform.localPosition.y);

            float et = 0.0f;
            float duration = 3.0f;

            while(et < duration)
            {
                transform.localPosition = Vector2.Lerp(init, Target, et / duration);

                et += Time.deltaTime;
                yield return null;
            }

            OnValueUpdated?.Invoke(Target);
            yield return new WaitForSeconds(Random.Range(2.5f, 5.0f));
        }
    }

    public void Init(Vector3 initPosition)
    {
        transform.localPosition = initPosition;
    }
}
