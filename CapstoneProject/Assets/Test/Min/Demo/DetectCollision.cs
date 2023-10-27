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

    void Update()
    {
        CheckDimensionSwitchAvailability();
        CheckItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("Game Over!!!");
        }

        else if (collision.gameObject.name == "JumpPad" ||  collision.gameObject.name == "JumpPad (1)")
        {
            GetComponent<vThirdPersonController>().SuperJump(15f);
        }
    }

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

    private void CheckDimensionSwitchAvailability()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
        layerMask = ~(1 << LayerMask.NameToLayer("Ignore Raycast")) & ~(1 << LayerMask.NameToLayer("Player"));

        Vector3 pos = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z - 20f);
        //Debug.DrawRay(pos, Vector3.forward*40f, Color.red);

        if (Physics.Raycast(pos, Vector3.forward, out RaycastHit hit, 40.0f, layerMask))
        {
            GameManager.instance.CanSwitchDimension = false;
            GameManager.instance.ChangeColor();
        }
        else
        {
            GameManager.instance.CanSwitchDimension = true;
            GameManager.instance.ChangeColor();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                itemText.gameObject.SetActive(true);
            }
        }
        else
        {
            itemText.gameObject.SetActive(false);
        }
        
    }
    
}
