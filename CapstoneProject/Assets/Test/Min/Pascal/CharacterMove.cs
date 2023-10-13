using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private bool isGround = false;

    private CapsuleCollider capsuleCollider;
    private Rigidbody myRigid;

   
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
        IsGround();
        TryJump();
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        Vector3 moveDirection = new Vector3(moveDirX, 0f, 0f);

        myRigid.velocity = new Vector3(moveDirection.x * moveSpeed, myRigid.velocity.y, 0f);

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
}
