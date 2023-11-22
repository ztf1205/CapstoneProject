using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int HP { get; private set; } = 3000;

    public void ReduceHP(int amount)
    {
        HP -= amount;
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
