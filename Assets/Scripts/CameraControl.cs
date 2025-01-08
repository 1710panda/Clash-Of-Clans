using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;
using UnityEngine.Windows;


namespace ClashOfClans
{

    public class CameraControl : MonoBehaviour
    {
        [SerializeField]private Camera _camera = null;
        private float _moveSpeed = 20f;
        private float _moveSmooth = 5f;


         private Controls _inputs = null;  // 输入控制
        private bool _isMoving = false;  // 是否控制摄像头移动

        private Transform _root   = null;
        private Transform _pivot   = null;
        private Transform _target = null;

        private Vector3 _center = Vector3.zero;
        private float _left = -10;
        private float _right = 10;
        private float _up = 10;
        private float _down = -10;
        private float _angle = 45;



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

            _inputs.Enable();
            _inputs.Main.Move.started += _ => MovePressed();         // 开始-按下
           
            _inputs.Main.Move.canceled += _ => MoveCanceled();  // 取消-按下 = 抬起

        }

        private void OnDisable()
        {


            _inputs.Main.Move.started -= _ => MovePressed();         // 开始-按下
            _inputs.Main.Move.canceled -= _ => MoveCanceled();  // 取消-按下 = 抬起
            _inputs.Disable();

        }

        void Start()
        {
            Initialize(_center, _left, _right, _up, _down, _angle);

        }

        public void Initialize(Vector3 center, float left, float right, float up, float down, float angle)
        {
            _center = center;
            _left = left;
            _right = right;
            _up = up;
            _down = down;
            _angle = angle;
            _isMoving = false;
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

            if (_isMoving) 
            {
                Vector2 move = _inputs.Main.MoveDelta.ReadValue<Vector2>();
                if(move != Vector2.zero )
                {
                    // 归一化，防止不同分辨率下移动速率差异
                    move.x /= Screen.width;
                    move.y /= Screen.height;
                    _root.position -= _root.right * move.x * _moveSpeed;
                    _root.position -= _root.forward * move.y * _moveSpeed;
                    print(_root.position);
                    _root.position = new Vector3(Mathf.Clamp(_root.position.x, _left, _right), _root.position.y, Mathf.Clamp(_root.position.z, _down, _up));
                }
            } 
            else
            {

            }

            if( _target.position != _camera.transform.position)
            {
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth*Time.deltaTime);
            }
            if(_target.rotation != _camera.transform.rotation)
            {
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _target.rotation, _moveSmooth * Time.deltaTime);
            }
        }


        private void MovePressed()
        {
            _isMoving = true;
            Debug.Log(111);
        }
        private void MoveCanceled()
        {
            _isMoving = false;

        }


    }
}
