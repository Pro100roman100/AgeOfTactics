using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //speed of camera movement
    [SerializeField] private float _moveSpeed = 1f;
    //speed of camera zooming
    [SerializeField] private float _zoomSpeed = 10;
    [SerializeField] private float _minZoom = 5.598283f;
    [SerializeField] private float _maxZoom = 17.91455f;
    //zone for camera
    [SerializeField] private Vector2 allowedZone = new(10f, 10f);

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
            
            //check if cam is out of zone
            if(-_cam.transform.position.x > allowedZone.x && _cam.transform.position.x < 0)
                _cam.transform.Translate(Vector2.left * (_cam.transform.position.x + allowedZone.x));
            else if (_cam.transform.position.x > allowedZone.x && _cam.transform.position.x > 0)
                _cam.transform.Translate(Vector2.right * (-_cam.transform.position.x + allowedZone.x));
            if (_cam.transform.position.y > allowedZone.x && _cam.transform.position.y > 0)
                _cam.transform.Translate(Vector2.up * (-_cam.transform.position.y + allowedZone.y));
            else if (-_cam.transform.position.y > allowedZone.x && _cam.transform.position.y < 0)
                _cam.transform.Translate(Vector2.down * (_cam.transform.position.y + allowedZone.y));
        }

        //zoom camera
        _cam.orthographicSize -= (float)Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        if (_cam.orthographicSize < _minZoom)
            _cam.orthographicSize = _minZoom;
        else if (_cam.orthographicSize > _maxZoom)
            _cam.orthographicSize = _maxZoom;
    }


    private void OnDrawGizmos()
    {
        //draw zone in scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector2.zero, allowedZone * 2);
    }
}