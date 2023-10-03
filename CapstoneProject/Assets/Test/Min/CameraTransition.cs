using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTransition : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] Transform transform2D;
    [SerializeField] Transform transform3D;

    private Transform targetTransform;
    private float targetFOV;
    private float moveDuration = 2.0f;

    private bool is2D = false;

    private bool isMoving = false;

    public GameObject wall;
    public GameObject particle1;
    public GameObject particle2;

    private void Start()
    {
        DOTween.Init();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
            // 3D -> 2D
            if (!is2D)
            {
                SwitchTo2D();
            }
            // 2D -> 3D
            else
            { 
                SwitchTo3D();
            }  
            isMoving = false;
        }
    }

    private void SwitchTo2D()
    {
        is2D = true;
        targetTransform = transform2D;
        targetFOV = 22f;

        mainCamera.transform.DOMove(targetTransform.position, moveDuration)
       .OnUpdate(() =>
       {
           mainCamera.transform.DORotateQuaternion(targetTransform.rotation, moveDuration)
           .OnUpdate(() =>
           {
               mainCamera.DOFieldOfView(targetFOV, 1.0f)
               .OnComplete(() =>
               {
                   mainCamera.orthographic = is2D;
                   wall.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 10f);
               });
           });
       });

       
    }

    private void SwitchTo3D()
    {
        is2D = false;
        targetTransform = transform3D;
        targetFOV = 60f;

        mainCamera.orthographic = is2D;
        wall.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        mainCamera.transform.DOMove(targetTransform.position, moveDuration)
       .OnUpdate(() =>
       {
           mainCamera.transform.DORotateQuaternion(targetTransform.rotation, moveDuration)
           .OnComplete(() =>
           {
               mainCamera.DOFieldOfView(targetFOV, 1.0f);
               particle1.GetComponent<ParticleSystem>().Play();
               particle2.GetComponent<ParticleSystem>().Play();
           });
       });

 
    }
}


