using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{
    private LineRenderer LineRenderer { get; set; } = null;
    private List<Vector3> Positions { get; set; } = new List<Vector3>();

    private void Awake()
    {
        Bake();
    }

    private void Bake()
    {
        LineRenderer = GetComponent<LineRenderer>();

        int bakeCount = 25;
        for(int i = 0; i < bakeCount; i++)
        {
            Positions.Add(GetPosition(i));
        }

        LineRenderer.positionCount = Positions.Count;
        LineRenderer.SetPositions(Positions.ToArray());

        Instantiate(Resources.Load<Trail>("trail")).Init(Positions[Positions.Count - 1]);

        FindObjectOfType<CameraController>().transform.position = Positions[Positions.Count - 1];
        FindObjectOfType<CameraController>().Target = Positions[Positions.Count - 1];
    }

    private Vector3 GetPosition(int lastXPos)
    {
        return new Vector3(lastXPos / 4.0f, Random.Range(-0.5f, 0.5f), 0);
    }
}
