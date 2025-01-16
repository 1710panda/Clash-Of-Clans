
using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;




namespace ClashOfClans
{

    public class CameraControl : MonoBehaviour
    {
        [SerializeField]private Camera _camera = null;
        [SerializeField] private float _moveSpeed = 20f;
        [SerializeField] private float _moveSmooth = 5f;


        private Controls _inputs = null;  // 输入控制
        private bool _isMoving = false;  // 是否控制摄像头移动

        private bool _isZooming = false; // 缩放
        private float _zoom = 5f;   // 相机视口值
        [SerializeField] private float _zoomSmooth = 5f;
        [SerializeField] private float _zoomSpeed = 5f;




        private Transform _root   = null;
        private Transform _pivot   = null;
        private Transform _target = null;

        private Vector3 _center = Vector3.zero;
        private float _left = -10;
        private float _right = 10;
        private float _up = 10;
        private float _down = -10;
        private float _angle = 45;
        private float _zoomMax = 10;
        private float _zoomMin = 1;
        private Vector2 _zoomPositionOnScreen = Vector2.zero;
        private Vector3 _zoomPositionInWorld = Vector3.zero;
        private float _zoomBaseValue = 0;
        private float _zoomBaseDistance = 0;




        private void Awake()
        {
            _inputs = new Controls();
            _root = new GameObject("CamerHelper_Root").transform;
            _pivot = new GameObject("CamerHelper_Pivot").transform;
            _target = new GameObject("CamerHelper_Target").transform;
            _camera.orthographic = true;
            _camera.nearClipPlane = 0; // 近裁剪平面 最近也能看到

        }
        private void OnEnable()
        {
            _inputs.Main.Move.started += _ => MoveStarted();         // 开始-按下
            _inputs.Main.Move.canceled += _ => MoveCanceled();  // 取消-按下 = 抬起
            _inputs.Main.TouchZoom.started += _ => ZoomStarted();         
            _inputs.Main.TouchZoom.canceled += _ => ZoomCanceled();  
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Main.Move.started -= _ => MoveStarted();         // 开始-按下
            _inputs.Main.Move.canceled -= _ => MoveCanceled();  // 取消-按下 = 抬起

            _inputs.Main.TouchZoom.started -= _ => ZoomStarted();
            _inputs.Main.TouchZoom.canceled -= _ => ZoomCanceled();
            _inputs.Disable();

        }

        void Start()
        {
            Initialize(Vector3.zero, -10, 10, 10, -10, 45, 5, 10, 1);

        }

        public void Initialize(Vector3 center, float left, float right, float up, float down, float angle, float zoom, float zoomMax, float zoomMin)
        {
            _center = center;
            _left = left;
            _right = right;
            _up = up;
            _down = down;
            _angle = angle;
            _isMoving = false;
            _isZooming = false;
            _zoom = zoom;
            _zoomMax = zoomMax;
            _zoomMin = zoomMin;


            _camera.orthographicSize = _zoom;


            _pivot.SetParent(_root);
            _target.SetParent(_pivot);

            _root.position = center;
            _root.eulerAngles =Vector3.zero;

            _pivot.localPosition = Vector3.zero;
            _pivot.localEulerAngles = new Vector3(angle, 0, 0);

            _target.localPosition = new Vector3(0, 0, -10);
            _target.localEulerAngles = Vector3.zero;



        }

        void Update()
        {

            if (!Input.touchSupported)
            {
                float mouseScroll = _inputs.Main.MouseScroll.ReadValue<float>();
                if (mouseScroll > 0)
                {
                    _zoom -= Time.deltaTime * 10;
                    Debug.Log($"Mouse mouseScroll {mouseScroll}");

                }
                else if(mouseScroll < 0)
                {
                    _zoom += Time.deltaTime * 10;

                    Debug.Log($"Mouse mouseScroll {mouseScroll}");
                }
            }
            if (_isZooming)
            {
                Vector2 touch0 = _inputs.Main.TouchPositon0.ReadValue<Vector2>();
                Vector2 touch1 = _inputs.Main.TouchPositon1.ReadValue<Vector2>();
 
                touch0.x /= Screen.width;
                touch0.y /= Screen.height;
                touch1.x /= Screen.width;
                touch1.y /= Screen.height;

                float currentDistance = Vector2.Distance(touch1, touch0);
                float deletDistance = currentDistance - _zoomBaseDistance;
                _zoom = _zoomBaseDistance - (deletDistance * _zoomSpeed);


                Vector3 zoomCenter = ScreenPosToPlanePos(_zoomPositionOnScreen);


                _root.position += (_zoomPositionInWorld - zoomCenter) * _moveSpeed;
            }
            else
            {
                if (_isMoving)
                {
                    Vector2 move = _inputs.Main.MoveDelta.ReadValue<Vector2>();
                    if (move != Vector2.zero)
                    {
                        // 归一化，防止不同分辨率下移动速率差异
                        move.x /= Screen.width;
                        move.y /= Screen.height;
                        _root.position -= _root.right * move.x * _moveSpeed;
                        _root.position -= _root.forward * move.y * _moveSpeed;
                        _root.position = new Vector3(Mathf.Clamp(_root.position.x, _left, _right), _root.position.y, Mathf.Clamp(_root.position.z, _down, _up));
                    }
                }
                else
                {

                }


                AdjustBounds();

                // 设置位置
                if (_target.position != _camera.transform.position)
                {
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth * Time.deltaTime); //平滑移动
                }
                if (_target.rotation != _camera.transform.rotation)
                {
                    _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _target.rotation, _moveSmooth * Time.deltaTime);
                }

            }

           

            // 设置缩放
            if (_camera.orthographicSize != _zoom)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize,_zoom, _zoomSmooth * Time.deltaTime); //平滑移动
            }
            

            
        }

        private void AdjustBounds()
        {
            throw new NotImplementedException();
        }

        private void MoveStarted()
        {
            Vector2 touch0 = _inputs.Main.TouchPositon0.ReadValue<Vector2>();
            Vector2 touch1 = _inputs.Main.TouchPositon1.ReadValue<Vector2>();
             _zoomPositionOnScreen = Vector2.Lerp(touch0 , touch1,0.5f) ;
            _zoomPositionInWorld = ScreenPosToPlanePos(_zoomPositionOnScreen);
            _zoomBaseValue = _zoom;


            touch0.x /= Screen.width;
            touch0.y /= Screen.height;
            touch1.x /= Screen.width;
            touch1.y /= Screen.height;

            _zoomBaseDistance =  Vector2.Distance(touch1 , touch0);


            _isMoving = true;
            print(_inputs.Main.MousePosition.ReadValue<Vector2>());
        }
        private void MoveCanceled()
        {
            _isMoving = false;
        }

        private void ZoomStarted()
        {
            _isZooming = true;

        }
        private void ZoomCanceled()
        {
            _isZooming = false;



        }

 
        private  Vector3 ScreenPosToWorldPos(Vector2 pos)
        {
            float cameraScreenWorldPosHeight = _camera.orthographicSize * 2f;
            float cameraScreenWorldPosWidth = _camera.aspect * cameraScreenWorldPosHeight;


            Vector3 offsetWorldPos = _camera.transform.right * cameraScreenWorldPosWidth / 2f + _camera.transform.up * cameraScreenWorldPosHeight / 2f;  // 摄像机在屏幕中间

            Vector3 cameraScreenOriginWorldPos = _camera.transform.position - offsetWorldPos;

            float x = pos.x / Screen.width;
            float y = pos.y / Screen.height;


            Vector3 cameraScreenToWorldPos = cameraScreenOriginWorldPos + _camera.transform.right * x  * cameraScreenWorldPosWidth + _camera.transform.up * y * cameraScreenWorldPosHeight;


            return cameraScreenToWorldPos;

        }


        public Vector3 ScreenPosToPlanePos(Vector2 pos)
        {
            Vector3 world = ScreenPosToWorldPos(pos);
            float distancePlanePos = (world.y - _root.position.y) / Mathf.Sin(45f * Mathf.Deg2Rad);

            Vector3 planeWorld = world + distancePlanePos * _camera.transform.forward;

            return planeWorld;
               
        }

        [ContextMenu("testMethod")]
        public void TestMethod()
        {
            float h = _camera.orthographicSize * 2f;
            float w = _camera.aspect * h;

            print($"{h} {w}");

            Vector3 camreaCameraScreenPos = _camera.transform.right.normalized * w / 2f + _camera.transform.up.normalized * h / 2f;
            print(camreaCameraScreenPos);
        }


    }
    
}
