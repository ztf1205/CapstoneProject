using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Build;

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
        private Quaternion prevRotation;
        private bool isMoveStop = false;
        private Animator animator;
        private bool isCollisionCheckState = false;
        private bool isDollyZoom = false;
        private bool isWalking = false;
        private bool prevIsGround = false;

        [HorizontalLine, SerializeField]
        private GameObject plane;
        [SerializeField]
        private GameObject playerUI;
        [SerializeField]
        private bool canUseSwitchDimensionInput = true;
        [SerializeField]
        private bool canCollisionCheck = false;

        #endregion

        private void Awake()
        {
            EventManager.Subscribe("OnStartDollyZoom", OnStartDollyZoom);
            EventManager.Subscribe("OnEndDollyZoom", OnEndDollyZoom);
            EventManager.Subscribe("OnGainSkillItem", OnGainSkillItem);
        }

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();

            rigidbodyComponent = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            CursorActivate(false);
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            MoveStopHandle();
            if (isMoveStop == false)
            {
                MoveInput();
                JumpInput();

                cc.UpdateAnimator();            // updates the Animator Parameters
            }
            CameraInput();

            // 2d, 3d 전환 처리
            SwitchDimensionHandle();

            // 마우스 커서 표시, 숨기기 처리
            CursorHandle();
        }

        private void LateUpdate()
        {
            if (isMoveStop)
            {
                transform.rotation = prevRotation;
            }

            // UI 활성화 처리
            UIHandle();

            // 걷는 상태에 대한 이벤트 처리
            WalkHandle();

            // 착지에 대한 이벤트 처리
            LandingHandle();
        }

        private void OnDestroy()
        {
            CursorActivate(true);

            EventManager.Unsubscribe("OnStartDollyZoom", OnStartDollyZoom);
            EventManager.Unsubscribe("OnEndDollyZoom", OnEndDollyZoom);
            EventManager.Unsubscribe("OnGainSkillItem", OnGainSkillItem);
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        private void OnStartDollyZoom()
        {
            isDollyZoom = true;
            if (dimManager.Is2D)
            {
                EventManager.TriggerEvent("DollyZoom3D");
            }
            else
            {
                EventManager.TriggerEvent("DollyZoom2D");
            }
            MoveStopActivate(true);
        }

        private void OnEndDollyZoom()
        {
            isDollyZoom = false;
            if (Input.GetKey(KeyCode.Mouse1) == false)
            {
                MoveStopActivate(false);
            }
        }

        private void OnGainSkillItem()
        {
            canUseSwitchDimensionInput = true;
        }

        private void WalkHandle()
        {
            bool isMoving;

            if (dimManager.Is2D)
            {
                if (Input.GetAxisRaw(horizontalInput) == 0f)
                {
                    isMoving = false;
                }
                else
                {
                    isMoving = true;
                }
            }
            else
            {
                if (Input.GetAxisRaw(horizontalInput) == 0f && Input.GetAxisRaw(verticallInput) == 0f)
                {
                    isMoving = false;
                }
                else
                {
                    isMoving = true;
                }
            }

            if (isWalking)
            {
                // 걷고 있던 상태인데, 걷지 않으면 신호
                if (isMoving == false || cc.isGrounded == false || isMoveStop)
                {
                    isWalking = false;
                    EventManager.TriggerEvent("OnPlayerWalkEnd");
                }
            }
            else
            {
                // 걷지 않고 있는 상태인데, 걸으면 신호
                if (isMoving && cc.isGrounded && isMoveStop == false)
                {
                    isWalking = true;
                    EventManager.TriggerEvent("OnPlayerWalkStart");
                }
            }
        }

        private void LandingHandle()
        {
            if(cc.isGrounded == true && prevIsGround == false)
            {
                EventManager.TriggerEvent("OnPlayerLanding");
            }

            prevIsGround = cc.isGrounded;
        }

        private void UIHandle()
        {
            if (dimManager.Is2D == false && isDollyZoom == false)
            {
                plane.SetActive(isMoveStop);
            }
            else
            {
                plane.SetActive(false);
            }
            //playerUI.SetActive(isMoveStop);
            playerUI.SetActive(false);
        }

        private void SwitchDimensionHandle()
        {
            if(isDollyZoom || canUseSwitchDimensionInput == false)
            {
                return;
            }


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (dimManager.CanSwitchDimension == false && canCollisionCheck == false)
                {
                    EventManager.TriggerEvent("OnSwitchDimensionFail");
                    EventManager.TriggerEvent("SwitchDimensionFailSound");
                }
                else
                {
                    SwitchDimension();
                    EventManager.TriggerEvent("StandardCamera");
                }
            }
        }

        private void SwitchDimension()
        {
            if (isCollisionCheckState)
            {
                isCollisionCheckState = false;
                if (Input.GetKey(KeyCode.Mouse1) == false)
                {
                    MoveStopActivate(false);
                }
            }
            else
            {
                if (dimManager.CanSwitchDimension == false)
                {
                    MoveStopActivate(true);
                    isCollisionCheckState = true;
                }
            }
            dimManager.SwitchDimension(false);
        }

        private void CursorHandle()
        {
            // 게임 중에 ESC 키를 누르면 마우스 커서를 풀어줍니다.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorActivate(true);
            }

            // 입력이 들어오면 숨기기
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                CursorActivate(false);
            }
        }

        private void CursorActivate(bool isActivate)
        {
            if(isActivate)
            {
                // 커서 보여주고 움직이게 해주기
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // 마우스 커서를 숨기고 보이지 않도록 설정
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void MoveStopHandle()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EventManager.TriggerEvent("TimeStop");
                MoveStopActivate(true);
                SetActiveUI(true);
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && isCollisionCheckState == false)
            {
                EventManager.TriggerEvent("OffTimeStop");
                MoveStopActivate(false);
                SetActiveUI(false);
            }
        }

        private void SetActiveUI(bool isActive)
        {
            if (dimManager.Is2D)
            {
                plane.SetActive(false);
            }
            else
            {
                plane.SetActive(isActive);
            }
            playerUI.SetActive(isActive);
        }

        private void MoveStopActivate(bool isActivate)
        {
            if(isActivate)
            {
                prevVelocity = rigidbodyComponent.velocity;
                prevRotation = transform.rotation;
                rigidbodyComponent.velocity = Vector3.zero;

                animator.speed = 0f;

                isMoveStop = true;
            }
            else
            {
                if(dimManager.Is2D)
                {
                    prevVelocity.z = 0f;
                }
                rigidbodyComponent.velocity = prevVelocity;

                animator.speed = 1f;

                isMoveStop = false;
            }
            rigidbodyComponent.isKinematic = isMoveStop;
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

            if (cameraMain && isMoveStop == false)
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
            {
                cc.Jump();
                EventManager.TriggerEvent("OnPlayerJump");
            }
        }

        #endregion       
    }
}