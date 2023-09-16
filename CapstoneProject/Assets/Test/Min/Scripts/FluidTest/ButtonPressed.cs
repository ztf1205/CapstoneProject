using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonPressed : MonoBehaviour
{
    public bool pressed = false;

    public float lerpTime;
    public float timeStartedLerping;

    private Vector3 endPosition;
    private Vector3 startPosition;

  

    // Start is called before the first frame update
    void Start()
    {
        startPosition = GetComponent<Transform>().position;
        endPosition = new Vector3(startPosition.x, startPosition.y, -0.4f);
        StartLerping();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
        }
    }

    private void StartLerping()
    {
        timeStartedLerping = Time.deltaTime;

        Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
    }

    private Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}
