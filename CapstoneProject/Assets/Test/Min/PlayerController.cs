using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;

    private bool isGround = false;

    [SerializeField] private float lookSensitivity;

    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX = 0f;

    [SerializeField] private Camera theCamera;

    private CapsuleCollider capsuleCollider;
    private Rigidbody myRigid;

    // 임시
    private float hungerGage = 100.0f;
    [SerializeField] private TextMeshProUGUI hungerGagaeText;

    void Start()
    {
        // 마우스 커서
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        IsGround();
        TryJump();
        CharacterRotation();    // 좌우 캐릭터 회전
        CameraRotation();       // 상하 카메라 회전

        // 임시
        if (hungerGage > 0f)
        {
            hungerGage -= 0.001f;
            hungerGagaeText.text = "포만감:" + (int)hungerGage + "/100";
        }
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    private void CameraRotation() 
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
