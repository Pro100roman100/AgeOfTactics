using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //speed of camera movement
    [SerializeField] private float _moveSpeed = 1f;
    //speed of camera zooming
    [SerializeField] private float _zoomSpeed = 10;
    [SerializeField] private float _minZoom = 1;
    [SerializeField] private float _maxZoom = 15;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        //move camera
        if (Input.GetMouseButton(2))
        {
            _cam.transform.Translate(new Vector3(
                -Input.GetAxis("Mouse X") * _moveSpeed * _cam.orthographicSize * .2f,
                -Input.GetAxis("Mouse Y") * _moveSpeed * _cam.orthographicSize * .2f
                ));
        }

        //zoom camera
        _cam.orthographicSize -= (float)Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        if (_cam.orthographicSize < _minZoom)
            _cam.orthographicSize = _minZoom;
        else if (_cam.orthographicSize > _maxZoom)
            _cam.orthographicSize = _maxZoom;
    }
}