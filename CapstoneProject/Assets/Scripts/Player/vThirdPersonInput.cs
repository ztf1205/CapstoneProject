using UnityEngine;
using NaughtyAttributes;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        //public KeyCode strafeInput = KeyCode.Tab;
        //public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        private DimensionManager dimManager;

        private Rigidbody rigidbodyComponent;
        private Vector3 prevVelocity;
        private Vector3 prevPos;
        private Quaternion prevRotation;
        private bool isJumpStop = false;
        private Animator animator;

        [HorizontalLine, SerializeField]
        private GameObject plane;
        [SerializeField]
        private GameObject playerUI;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();

            rigidbodyComponent = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            JumpStopHandle();
            if (isJumpStop)
            {
                rigidbodyComponent.position = prevPos;
                rigidbodyComponent.velocity = Vector3.zero;
            }
            else
            {
                MoveInput();
                JumpInput();

                cc.UpdateAnimator();            // updates the Animator Parameters
            }
            CameraInput();

            // Switch Dimension
            if (Input.GetKeyDown(KeyCode.Q) && dimManager.CanSwitchDimension)
            {
                dimManager.SwitchDimension();
            }
        }

        private void LateUpdate()
        {
            if (isJumpStop)
            {
                transform.rotation = prevRotation;
            }

            //UI 활성화 처리
            if (dimManager.Is2D)
            {
                plane.SetActive(false);
            }
            else
            {
                plane.SetActive(isJumpStop);
            }
            playerUI.SetActive(isJumpStop);
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        private void JumpStopHandle()
        {
            if (Input.GetKeyDown(jumpInput) && cc.isGrounded == false)
            {
                Debug.Log("점프 중에 추가 점프 입력");
                if (isJumpStop)
                {
                    JumpStopActivate(false);
                }
                else
                {
                    JumpStopActivate(true);
                }
            }

            //바닥에 닿아있는 상태에서는 점프 정지 자동 해제
            if(cc.isGrounded && isJumpStop)
            {
                JumpStopActivate(false);
            }
        }

        private void JumpStopActivate(bool isActivate)
        {
            if(isActivate)
            {
                prevVelocity = rigidbodyComponent.velocity;
                prevPos = rigidbodyComponent.position;
                prevRotation = transform.rotation;
                rigidbodyComponent.velocity = Vector3.zero;
                rigidbodyComponent.useGravity = false;

                animator.speed = 0f;

                isJumpStop = true;
            }
            else
            {
                if(dimManager.Is2D)
                {
                    prevVelocity.z = 0f;
                }
                rigidbodyComponent.velocity = prevVelocity;
                rigidbodyComponent.useGravity = true;

                animator.speed = 1f;

                isJumpStop = false;
            }
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            //SprintInput();
            //StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        //protected virtual void StrafeInput()
        //{
        //    if (Input.GetKeyDown(strafeInput))
        //        cc.Strafe();
        //}

        //protected virtual void SprintInput()
        //{
        //    if (Input.GetKeyDown(sprintInput))
        //        cc.Sprint(true);
        //    else if (Input.GetKeyUp(sprintInput))
        //        cc.Sprint(false);
        //}

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }

        #endregion       
    }
}