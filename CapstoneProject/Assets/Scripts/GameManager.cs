using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Camera threeDimensionCam;
    [SerializeField] private Camera twoDimensionCam;

    [SerializeField] private GameObject virtualObstacles;
    [SerializeField] private GameObject resizableObjects;

    public bool Is2D { get; set; } = false;
    public bool CanSwitchDimension { get; set; } = true;

    //temp
    public Image image;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Init();
    }

    private void Init()
    {
        SwitchCamera();
        ChangeVirtualObstaclesState();
        ResizeColliders();
    }

    public void SwitchDimension()
    {
        Is2D = !Is2D;
        SwitchCamera();
        ChangeVirtualObstaclesState();
        ResizeColliders();
    }

    private void SwitchCamera()
    {
        twoDimensionCam.enabled = Is2D;
        threeDimensionCam.enabled = !Is2D;
    }

    private void ChangeVirtualObstaclesState()
    {
        virtualObstacles.SetActive(Is2D);
    }

    private void ResizeColliders()
    {
        foreach (Transform ob in resizableObjects.transform)
        {
            BoxCollider boxCollider = ob.GetComponent<BoxCollider>();
            boxCollider.size = Is2D ? new Vector3(1f, 1f, 20f) : new Vector3(1f, 1f, 1f);
        }
    }

    public void ChangeColor()
    {
        if (CanSwitchDimension)
            image.color = Color.green;
        else 
            image.color = Color.red;
    }


    public void OnGameClear()
    {

    }

    public void OnPlayerDead()
    {

    }
}
