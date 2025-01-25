

using UnityEngine;


namespace ClashOfClans
{

    public class CameraControl : MonoBehaviour
    {
        [SerializeField]private Camera _camera = null;
        [SerializeField] private float _moveSpeed = 20f;
        [SerializeField] private float _moveSmooth = 5f;
        private Vector3 _moveVelocity = Vector3.zero;
        private float _zoomVelocity ;


        private bool _isMoving = false;  // 是否控制摄像头移动
        private Vector3 _pointDownPosition ;
        private Vector3 _pointDownWorldPosition;

        public bool isTestZoom;
        public Vector2 testTouch0;
        public bool needMove;
        public Vector3 needMovePos;


        private bool _isZooming = false; // 缩放
        public float _zoom = 5f;   // 相机视口值
        [SerializeField] private float _zoomSmooth = 5f;
        [SerializeField] private float _zoomSpeed = 5f;




        private Transform _root   = null;
        private Transform _pivot   = null;
        private Transform _target = null;

        private Vector3 _center = Vector3.zero;
        public float _left = -10;
        public float _right = 10;
        public float _up = 10;
        public float _down = -10;
        public float _angle = 45;
        public float _zoomMax = 10;
        public float _zoomMin = 1;
        private Vector2 _zoomPositionOnScreen = Vector2.zero;
        private Vector3 _zoomPositionInPlane = Vector3.zero;
        private float _zoomBaseValue = 0;
        private float _zoomBaseDistance = 0;




        private void Awake()
        {

            _root = new GameObject("CamerHelper_Root").transform;
            _pivot = new GameObject("CamerHelper_Pivot").transform;
            _target = new GameObject("CamerHelper_Target").transform;
            _camera.orthographic = true;
            _camera.nearClipPlane = 0; // 近裁剪平面 最近也能看到

        }
        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        void Start()
        {
            Initialize(Vector3.zero, _left, _right, _up, _down, _angle, _zoom, _zoomMax, _zoomMin);

            if (isTestZoom)
            {
                // Draw a yellow sphere at the transform's position
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = _camera.ScreenToWorldPoint(testTouch0);


            }
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

            _root.position = _center;
            _root.eulerAngles = Vector3.zero;

            _pivot.localPosition = Vector3.zero;
            _pivot.localEulerAngles = new Vector3(_angle, 0, 0);

            _target.localPosition = new Vector3(0, 0, -40);
            _target.localEulerAngles = Vector3.zero;



        }

        void Update()
        {
            GetInputSystem();
            if (_isZooming)
            {
                if (!Input.touchSupported  && !isTestZoom)
                {
                    float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
                    if (mouseScroll > 0)
                    {
                        Vector2 centerScreen = Input.mousePosition;
                        Vector3 centerWorld = ScreenPosToWorldPos(centerScreen);
                        _zoom -= Time.deltaTime * 50;

                        Vector3 newCenterWorld = ScreenPosToWorldPos(centerScreen, _zoom);
 
                        needMovePos = newCenterWorld- centerWorld;

                        needMove = true;
                        Debug.Log($"Mouse mouseScroll {mouseScroll}");

                    }
                    else if (mouseScroll < 0)
                    {
                        Vector2 centerScreen = Input.mousePosition;
                        Vector3 centerWorld = ScreenPosToWorldPos(centerScreen);

                        _zoom += Time.deltaTime * 50;

                        Vector3 newCenterWorld = ScreenPosToWorldPos(centerScreen, _zoom);
                        needMovePos = newCenterWorld - centerWorld;

                        needMove = true;

                        Debug.Log($"Mouse mouseScroll {mouseScroll}");
                    }

  

                }
                else
                {
                    if (isTestZoom)
                    {
                        print("zooming");
                        Vector2 touch0 = testTouch0;
                        Vector2 touch1 = Input.mousePosition;

                        Vector2 centerScreen = Vector2.Lerp(touch0, touch1, 0.5f);

                        touch0.x /= Screen.width;
                        touch0.y /= Screen.height;
                        touch1.x /= Screen.width;
                        touch1.y /= Screen.height;

                        float currentDistance = Vector2.Distance(touch1, touch0);
                        float deletDistance = currentDistance - _zoomBaseDistance;
                        _zoom -=  (deletDistance * _zoomSpeed);
                        _zoomBaseDistance = currentDistance;

                        Vector3 centerWorld = ScreenPosToWorldPos(centerScreen);
                        Vector3 newCenterWorld = ScreenPosToWorldPos(centerScreen, _zoom);

                        needMovePos = newCenterWorld - centerWorld;
                        needMove = true;
                    }
                    else
                    {
                        print("zooming");
                        Vector2 touch0 = Input.GetTouch(0).position;
                        Vector2 touch1 = Input.GetTouch(1).position;

                        Vector2 centerScreen = Vector2.Lerp(touch0, touch1, 0.5f);

                        touch0.x /= Screen.width;
                        touch0.y /= Screen.height;
                        touch1.x /= Screen.width;
                        touch1.y /= Screen.height;

                        float currentDistance = Vector2.Distance(touch1, touch0);
                        float deletDistance = currentDistance - _zoomBaseDistance;
                        _zoom -= (deletDistance * _zoomSpeed);
                        _zoomBaseDistance = currentDistance;

                        Vector3 centerWorld = ScreenPosToWorldPos(centerScreen);
                        Vector3 newCenterWorld = ScreenPosToWorldPos(centerScreen, _zoom);

                        needMovePos = newCenterWorld - centerWorld;
                        needMove = true;


                    }

                }
            }
            if (_isMoving)
            {
                Vector2 pointDownScreen = GetMovePoint();

                print($"{pointDownScreen} {_pointDownWorldPosition}  {ScreenPosToWorldPos(pointDownScreen)} ");
                if(_pointDownWorldPosition != ScreenPosToWorldPos(pointDownScreen))
                {
                    Vector3 move = ScreenPosToWorldPos(pointDownScreen) - _pointDownWorldPosition;
                    _root.Translate(-_root.right * move.x , Space.Self);
                    _root.Translate(-_root.forward * (move.y  / Mathf.Sin(45 * Mathf.Deg2Rad)) * (PlaneOrthoGraphicSize() / _camera.orthographicSize), Space.Self);
                    _root.position = new Vector3(Mathf.Clamp(_root.position.x, _left, _right), _root.position.y, Mathf.Clamp(_root.position.z, _down, _up));

                }

            }



            //AdjustBounds();
            // 设置缩放
            if (_camera.orthographicSize != _zoom)
            {
                _camera.orthographicSize = _zoom;// Mathf.SmoothDamp(_camera.orthographicSize, _zoom, ref _zoomVelocity, 0.05f); //平滑移动

                if (needMove)
                {
                    _root.Translate(-_root.right * needMovePos.x, Space.Self);
                    _root.Translate(-_root.forward * (needMovePos.y / Mathf.Sin(45 * Mathf.Deg2Rad)) * (PlaneOrthoGraphicSize() / _camera.orthographicSize), Space.Self);
                    needMove = false;
                }
            }




            // 设置位置
            if (_target.position != _camera.transform.position)
            {
                _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _target.position, ref _moveVelocity, 0.2f); //平滑移动
                _pointDownWorldPosition = ScreenPosToWorldPos(GetMovePoint());


                //_camera.transform.position = _target.position; 

            }
            if (_target.rotation != _camera.transform.rotation)
            {
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _target.rotation, _moveSmooth * Time.deltaTime);


            }






        }

        private void GetInputSystem()
        {
            if (isTestZoom)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 touch0 = testTouch0;
                    Vector2 touch1 = Input.mousePosition;
                    _zoomPositionOnScreen = Vector2.Lerp(touch0, touch1, 0.5f);
                    _zoomPositionInPlane = ScreenPosToPlanePos(_zoomPositionOnScreen);
                    _zoomBaseValue = _zoom;
                    touch0.x /= Screen.width;
                    touch0.y /= Screen.height;
                    touch1.x /= Screen.width;
                    touch1.y /= Screen.height;
                    _zoomBaseDistance = Vector2.Distance(touch1, touch0);
                    _isZooming = true;
                }
                if (Input.GetMouseButtonUp(0) && _isZooming)

                {
                    _isZooming = false;

                }
                return;
            }
            else
            {
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _pointDownWorldPosition = ScreenPosToWorldPos(GetMovePoint());
                        _isMoving = true;
                    }
                    if (Input.GetMouseButtonUp(0) && _isMoving)
                    {
                        _isMoving = false;
                    }

                    _isZooming = true;
                }
                else
                {
                    if(Input.touchCount >= 1)
                    {
                        if(Input.GetTouch(0).phase  == TouchPhase.Began)
                        {
                            _pointDownWorldPosition = ScreenPosToWorldPos(GetMovePoint());
                            _isMoving = true;

                        }

                    }
                    else
                    {
                        _isMoving = false;

                    }
                    if(Input.touchCount >= 2)
                    {
                        if(Input.GetTouch(1).phase  == TouchPhase.Began)
                        {
                            Vector2 touch0 = Input.GetTouch(0).position;
                            Vector2 touch1 = Input.GetTouch(1).position;
                            _zoomPositionOnScreen = Vector2.Lerp(touch0, touch1, 0.5f);
                            _zoomPositionInPlane = ScreenPosToPlanePos(_zoomPositionOnScreen);
                            _zoomBaseValue = _zoom;
                            touch0.x /= Screen.width;
                            touch0.y /= Screen.height;
                            touch1.x /= Screen.width;
                            touch1.y /= Screen.height;
                            _zoomBaseDistance = Vector2.Distance(touch1, touch0);
                            _isZooming = true;
                        }
                        if(Input.GetTouch(1).phase == TouchPhase.Ended  && _isZooming)
                        {
                            _isZooming = false;
                        }

                    }
                    else
                    {
                        _isZooming = false;

                    }
                }
                
            }



        }

        private void AdjustBounds()
        {
            float  h = PlaneOrthoGraphicSize() * 2;
            float w = h * _camera.aspect;
            //if(h > (Mathf.Abs(_up) + Mathf.Abs(_down)))
            //{
            //    _zoom -= _zoomSmooth * Time.deltaTime;  // (Mathf.Abs(_up) + Mathf.Abs(_down)) / 2;
            //}
            //if (w > (Mathf.Abs(_left) + Mathf.Abs(_right)))
            //{
            //    _zoom -= _zoomSmooth * Time.deltaTime;// (Mathf.Abs(_left) + Mathf.Abs(_right)) / 2;
            //}
            //Mathf.Clamp(_zoom, _zoomMin, _zoomMax);

            h = PlaneOrthoGraphicSize();
            w = h * _camera.aspect;

            Vector3 centerRight = _root.transform.position + _root.transform.right * w;
            Vector3 centerLeft = _root.transform.position - _root.transform.right * w;
            Vector3 centerUp = _root.transform.position + _root.transform.forward * h;
            Vector3 centerDown = _root.transform.position - _root.transform.forward * h;

            if (centerRight.x > _center.x + _right)
            {
                _root.position += Vector3.left * Mathf.Abs(centerRight.x - (_center.x + _right));
            }
            if (centerLeft.x < _center.x + _left)
            {
                _root.position += Vector3.right * Mathf.Abs(centerLeft.x - (_center.x + _left));
            }

            if (centerUp.z > _center.z + _up)
            {
                _root.position += Vector3.down * Mathf.Abs(centerUp.z - (_center.z + _up));
            }

            if (centerDown.z < _center.z + _down)
            {
                _root.position += Vector3.up * Mathf.Abs(centerDown.z - (_center.z + _down));
            }







        }


        private float PlaneOrthoGraphicSize()
        {
            return _camera.orthographicSize * 2f / Mathf.Sin(_angle * Mathf.Deg2Rad)/ 2f ;
        }

        private float PlaneOrthoGraphicSize(float orthographicSize)
        {
            return orthographicSize * 2f / Mathf.Sin(_angle * Mathf.Deg2Rad) / 2f;
        }

        private void MoveStarted()
        {
            
            _isMoving = true;

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

        private Vector3 ScreenPosToWorldPos(Vector2 pos, float orthographicSize)
        {

            float cameraScreenWorldPosHeight = orthographicSize * 2f;
            float cameraScreenWorldPosWidth = _camera.aspect * cameraScreenWorldPosHeight;


            Vector3 offsetWorldPos = _camera.transform.right * cameraScreenWorldPosWidth / 2f + _camera.transform.up * cameraScreenWorldPosHeight / 2f;  // 摄像机在屏幕中间

            Vector3 cameraScreenOriginWorldPos = _camera.transform.position - offsetWorldPos;

            float x = pos.x / Screen.width;
            float y = pos.y / Screen.height;


            Vector3 cameraScreenToWorldPos = cameraScreenOriginWorldPos + _camera.transform.right * x * cameraScreenWorldPosWidth + _camera.transform.up * y * cameraScreenWorldPosHeight;


            return cameraScreenToWorldPos;

        }


        public Vector3 ScreenPosToPlanePos(Vector2 pos)
        {
            Vector3 world = ScreenPosToWorldPos(pos);
            float distancePlanePos = (world.y - _root.position.y) / Mathf.Sin(45f * Mathf.Deg2Rad);
            Vector3 planeWorld = world + distancePlanePos * _camera.transform.forward;

            return planeWorld;
               
        }




        public Vector2 GetMovePoint()
        {
            Vector2 pointDownScreen = Vector2.zero;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                pointDownScreen = Input.mousePosition;

            }
            else
            {
                if(Input.touchCount > 0)
                {
                    pointDownScreen = Input.GetTouch(0).position;
                }
                else
                {
                    Debug.Log($"no touch");
                }

            }
            return pointDownScreen;
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
