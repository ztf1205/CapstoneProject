using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    public GameObject start;
    public GameObject end;

    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        line.SetPosition(0, start.transform.position);
        line.SetPosition(1, end.transform.position);
    }
}
