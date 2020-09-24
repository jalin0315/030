using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem : MonoBehaviour
{
    public static CameraSystem _Instance;
    public enum Mode
    {
        Disable,
        Enable
    }
    public Mode _Mode;
    public enum CameraMode
    {
        Local,
        Spectator,
        Scene
    }
    public CameraMode _CameraMode;

    public Text _SwitchButtonText;
    public Button _LeftButton;
    public Button _RightButton;
    public Text _SpectatorsName;
    public static Transform _LocalPlayer;
    public Transform _LookAt;
    [SerializeField] public List<Transform> _SpectatorPlayerList;
    [SerializeField] public List<Transform> _SceneCameraList;
    public Transform _Pivot;
    public float _Y_Offset;
    public float _Distance;
    private Vector3 _Offset;
    private Vector3 _DesiredPosition;
    public float _SmoothSpeed;

    public FixedTouchField _FixedTouchField;
    public float _Y_SpectatorOffset;
    public float _SpectatorSmoothSpeed;
    private float _CameraAngle_Y;
    public float _CameraAngleSpeed;
    private float _CameraPos_Y;
    public float _CameraPosSpeed;

    private int _SceneCameraIndex;
    public float _SceneCameraSmoothSpeed;
    //[SerializeField] private float _SwipeResistance = 200.0f;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _CameraMode = CameraMode.Local;
        _LookAt = _LocalPlayer;
        _Offset = new Vector3(0.0f, _Y_Offset, -1.0f * _Distance);
        _LeftButton.interactable = false;
        _RightButton.interactable = false;
        _SwitchButtonText.text = "操作中";
        _SpectatorsName.text = null;
    }

    private void LateUpdate()
    {
        _SpectatorPlayerList = Rank.ball;

        if (_CameraMode == CameraMode.Local)
        {
            _DesiredPosition = _LookAt.position + _Offset;
            _Pivot.position = Vector3.MoveTowards(_Pivot.position, _DesiredPosition, Mathf.Infinity);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _DesiredPosition, _SmoothSpeed * Time.deltaTime);
            _Pivot.LookAt(_LookAt.position + Vector3.up);
            Camera.main.transform.LookAt(_LookAt.position + Vector3.up);
            return;
        }
        if (_CameraMode == CameraMode.Spectator)
        {
            _CameraAngle_Y += _FixedTouchField._TouchDist.x * _CameraAngleSpeed;
            _CameraPos_Y = Mathf.Clamp(_CameraPos_Y - _FixedTouchField._TouchDist.y * _CameraPosSpeed * Time.deltaTime, 0.0f, _Y_SpectatorOffset);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _LookAt.position + Quaternion.AngleAxis(_CameraAngle_Y, Vector3.up) * new Vector3(0.0f, _CameraPos_Y, _Y_SpectatorOffset), _SpectatorSmoothSpeed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.LookRotation(_LookAt.position + Vector3.up - Camera.main.transform.position, Vector3.up);
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            return;
        }
        if (_CameraMode == CameraMode.Scene)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _SceneCameraList[_SceneCameraIndex].position, _SceneCameraSmoothSpeed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, _SceneCameraList[_SceneCameraIndex].rotation, _SceneCameraSmoothSpeed * Time.deltaTime);
        }

        /*if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) _TouchPosition = Input.mousePosition;
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float _swipe_force = _TouchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(_swipe_force) > _SwipeResistance)
            {
                // Left = True
                if (_swipe_force < 0) SlideCamera(null);
                else SlideCamera(null);
            }
        }*/
    }

    public void SwitchMode()
    {
        if (_CameraMode == CameraMode.Local)
        {
            _CameraMode = CameraMode.Spectator;
            MovementSystem._Instance._EnableGyro = false;
            _LeftButton.interactable = true;
            _RightButton.interactable = true;
            _SwitchButtonText.text = "觀戰中";
            _SpectatorsName.text = _LookAt.name;
            return;
        }
        if (_CameraMode == CameraMode.Spectator)
        {
            _CameraMode = CameraMode.Scene;
            MovementSystem._Instance._EnableGyro = false;
            _LeftButton.interactable = true;
            _RightButton.interactable = true;
            _SwitchButtonText.text = "世界模式";
            _SpectatorsName.text = null;
            return;
        }
        if (_CameraMode == CameraMode.Scene)
        {
            _CameraMode = CameraMode.Local;
            _LookAt = _LocalPlayer;
            MovementSystem._Instance._EnableGyro = true;
            _LeftButton.interactable = false;
            _RightButton.interactable = false;
            _SwitchButtonText.text = "操作中";
            _SpectatorsName.text = null;
            return;
        }
    }

    public void LocalControl(Transform _transform)
    {
        if (_CameraMode != CameraMode.Local) return;
        Vector3 _test = _transform.eulerAngles - _Pivot.eulerAngles;
        _Offset = Quaternion.Euler(0.0f, _test.y, 0.0f) * _Offset;
    }

    public void SpectatorControl(bool _left_button)
    {
        if (_CameraMode != CameraMode.Spectator) return;
        if (_left_button == true)
        {
            for (int _i = 0; _i < _SpectatorPlayerList.Count; _i++)
            {
                if (_LookAt == _SpectatorPlayerList[_i])
                {
                    if (_i != 0)
                    {
                        _LookAt = _SpectatorPlayerList[_i - 1];
                        break;
                    }
                    if (_i == 0)
                    {
                        _i = _SpectatorPlayerList.Count - 1;
                        _LookAt = _SpectatorPlayerList[_i];
                        break;
                    }
                }
            }
        }
        if (_left_button == false)
        {
            for (int _i = 0; _i < _SpectatorPlayerList.Count; _i++)
            {
                if (_LookAt == _SpectatorPlayerList[_i])
                {
                    if (_i != _SpectatorPlayerList.Count - 1)
                    {
                        _LookAt = _SpectatorPlayerList[_i + 1];
                        break;
                    }
                    if (_i == _SpectatorPlayerList.Count - 1)
                    {
                        _i = 0;
                        _LookAt = _SpectatorPlayerList[_i];
                        break;
                    }
                }
            }
        }
        _SpectatorsName.text = _LookAt.name;
    }

    public void SceneControl(bool _left_button)
    {
        if (_CameraMode != CameraMode.Scene) return;
        if (_left_button == true)
        {
            for (int _i = 0; _i < _SceneCameraList.Count; _i++)
            {
                if (_SceneCameraIndex == _i)
                {
                    if (_i != 0)
                    {
                        _SceneCameraIndex = _i - 1;
                        break;
                    }
                    if (_i == 0)
                    {
                        _i = _SceneCameraList.Count - 1;
                        _SceneCameraIndex = _i;
                        break;
                    }
                }
            }
        }
        if (_left_button == false)
        {
            for (int _i = 0; _i < _SceneCameraList.Count; _i++)
            {
                if (_SceneCameraIndex == _i)
                {
                    if (_i != _SceneCameraList.Count - 1)
                    {
                        _SceneCameraIndex = _i + 1;
                        break;
                    }
                    if (_i == _SceneCameraList.Count - 1)
                    {
                        _i = 0;
                        _SceneCameraIndex = _i;
                        break;
                    }
                }
            }
        }
    }
}
