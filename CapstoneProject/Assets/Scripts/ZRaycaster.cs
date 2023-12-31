using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZRaycaster : MonoBehaviour
{
    private DimensionManager dimManager;

    [SerializeField]
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDimensionSwitchAvailability();
    }

    private void CheckDimensionSwitchAvailability()
    {
        // Ray 발사 위치
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z - 20f);
        
        Debug.DrawRay(pos, Vector3.forward*40f, Color.red);

        if (Physics.Raycast(pos, Vector3.forward, out RaycastHit hit, 40.0f, layerMask))
        {
            dimManager.CanSwitchDimension = false;
        }
        else
        {
            dimManager.CanSwitchDimension = true;
        }
    }
}
