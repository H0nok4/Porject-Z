using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraController : Singleton<CameraController>
{
    public Camera MainCamera => Camera.main;
    public float maxX = 14; // ���X��Χ
    public float maxY = 9; // ���Y��Χ
    private float _cameraMoveSpeed = 20f; // �ƶ��ٶ�
    private float _zoomSpeed = 5f; // �����ٶ�
    private float _panBorderThickness = 10f; // ����ƶ�����Ļ��Ե�Ĵ�������
    public void HandleUpdate()
    {
        MoveCameraByKeyboardInput();
        ScrollCameraByMouseScrollWheel();
        MoveCameraByPadding();
        CameraRangeLimit();
    }

    public void MoveCameraByKeyboardInput()
    {
        // �ƶ�����ͷ
        Vector3 moveDirection = new Vector3(InputUtility.GetHorizontalMoveInput(), InputUtility.GetVerticalMoveInput(), 0);
        MainCamera.transform.Translate(moveDirection * _cameraMoveSpeed * Time.deltaTime);
    }

    public void CameraRangeLimit()
    {
        // ���Ʒ�Χ
        Vector3 newPosition = MainCamera.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, 0f, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, maxY);
        MainCamera.transform.position = newPosition;
    }

    public void ScrollCameraByMouseScrollWheel()
    {
        // ��������ͷ
        float scroll = InputUtility.GetMouseScrollWheelInput();
        MainCamera.orthographicSize -= scroll * _zoomSpeed;
    }

    public void MoveCameraByPadding()
    {
        // ����ƶ�����Ļ��Եʱ�ƶ�����ͷ
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
