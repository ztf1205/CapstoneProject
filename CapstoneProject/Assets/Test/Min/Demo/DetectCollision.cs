using DG.Tweening;
using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private TextMeshProUGUI itemText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trigger")
        {
            TwoDimensionCamera td = camera.GetComponent<TwoDimensionCamera>();

            if (td.flag)
                td.ResetCamera();
            else
                td.MoveCamera();

        }
    }

}
