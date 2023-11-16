using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private static int count;
    [SerializeField] private Transform cubeHolder;

    public int Count() { return count; }
    public void IncreaseCount() { count++; }
    public void DecreaseCount() { count--; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncreaseCount();
            MoveCube();
            gameObject.SetActive(false);
        }
    }

    private void MoveCube()
    {
        float cubeY = transform.localScale.y / 2;
        float holderY = cubeHolder.localScale.y / 2;
        float targetY = cubeHolder.position.y + cubeY + holderY;
        transform.position = new Vector3(cubeHolder.position.x, targetY, cubeHolder.position.z);
    }
}
