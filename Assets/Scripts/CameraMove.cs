using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //speed of camera movement
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 1f;
    //speed of camera zooming
    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed = 10;
    [SerializeField] private float _minZoom = 5.598283f;
    [SerializeField] private float _maxZoom = 17.91455f;
    //zone for camera
    [Header("Zone")]
    [SerializeField] private Vector2 allowedZone = new(10f, 10f);
    [SerializeField] private Vector2 offset = Vector2.zero;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private Vector3 _previousCoordinates = Vector3.zero;
    private void Update()
    {
        //move camera
        if (Input.GetMouseButton(2))
        {
            _cam.transform.Translate(_previousCoordinates - Camera.main.ScreenToWorldPoint(Input.mousePosition) * _moveSpeed);

            //check if cam is out of zone
            if(-_cam.transform.position.x > allowedZone.x - offset.x && _cam.transform.position.x - offset.x < 0)
                _cam.transform.Translate(Vector2.left * (_cam.transform.position.x + allowedZone.x - offset.x));

            else if (_cam.transform.position.x > allowedZone.x + offset.x && _cam.transform.position.x - offset.x > 0)
                _cam.transform.Translate(Vector2.right * (-_cam.transform.position.x + allowedZone.x + offset.x));

            if (_cam.transform.position.y > allowedZone.y + offset.y && _cam.transform.position.y - offset.y > 0)
                _cam.transform.Translate(Vector2.up * (-_cam.transform.position.y + allowedZone.y + offset.y));

            else if (-_cam.transform.position.y > allowedZone.y - offset.y && _cam.transform.position.y - offset.y < 0)
                _cam.transform.Translate(Vector2.down * (_cam.transform.position.y + allowedZone.y - offset.y));
        }
        _previousCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        Gizmos.DrawWireCube(offset, allowedZone * 2);
    }
}