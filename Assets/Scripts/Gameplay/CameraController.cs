using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraController : Singleton<CameraController>
{
    public Camera MainCamera => Camera.main;
    public float maxX = 14; // 最大X范围
    public float maxY = 9; // 最大Y范围
    private float _cameraMoveSpeed = 20f; // 移动速度
    private float _zoomSpeed = 5f; // 缩放速度
    private float _panBorderThickness = 10f; // 鼠标移动到屏幕边缘的触发距离
    public void HandleUpdate()
    {
        MoveCameraByKeyboardInput();
        ScrollCameraByMouseScrollWheel();
        MoveCameraByPadding();
        CameraRangeLimit();
    }

    public void MoveCameraByKeyboardInput()
    {
        // 移动摄像头
        Vector3 moveDirection = new Vector3(InputUtility.GetHorizontalMoveInput(), InputUtility.GetVerticalMoveInput(), 0);
        MainCamera.transform.Translate(moveDirection * _cameraMoveSpeed * Time.deltaTime);
    }

    public void CameraRangeLimit()
    {
        // 限制范围
        Vector3 newPosition = MainCamera.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, 0f, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, maxY);
        MainCamera.transform.position = newPosition;
    }

    public void ScrollCameraByMouseScrollWheel()
    {
        // 缩放摄像头
        float scroll = InputUtility.GetMouseScrollWheelInput();
        MainCamera.orthographicSize -= scroll * _zoomSpeed;
    }

    public void MoveCameraByPadding()
    {
        // 鼠标移动到屏幕边缘时移动摄像头
        if (Input.mousePosition.x < _panBorderThickness)
            MainCamera.transform.Translate(Vector3.left * _cameraMoveSpeed * Time.deltaTime);
        if (Input.mousePosition.x > Screen.width - _panBorderThickness)
            MainCamera.transform.Translate(Vector3.right * _cameraMoveSpeed * Time.deltaTime);
        if (Input.mousePosition.y < _panBorderThickness)
            MainCamera.transform.Translate(Vector3.down * _cameraMoveSpeed * Time.deltaTime);
        if (Input.mousePosition.y > Screen.height - _panBorderThickness)
            MainCamera.transform.Translate(Vector3.up * _cameraMoveSpeed * Time.deltaTime);
    }

}
