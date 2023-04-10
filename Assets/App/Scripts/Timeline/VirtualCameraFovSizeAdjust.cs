using Cinemachine;
using UnityEngine;

namespace App.Scripts.Timeline
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraFovSizeAdjust : MonoBehaviour
    {


        private enum AdjustType
        {
            Width,
            Height,
            Both,
        }

        [SerializeField, Tooltip("縦・横・両方のどれを見てカメラのパラメータを調整するか")]
        private AdjustType baseType = AdjustType.Both;

        [SerializeField, Tooltip("基準解像度")]
        private Vector2 baseScreenSize = new Vector2(960, 600);

        [SerializeField, Tooltip("Orthographicのみ、Size設定")]
        private float orthographicSize = 100.0f;

        [SerializeField, Tooltip("Perspectiveのみ、Size設定")]
        private float perspectiveFieldOfView = 60.0f;

        [SerializeField, Tooltip("常に変更を見てカメラのパラメータを調整するか")]
        private bool isAlwaysUpdate = false;

        private CinemachineVirtualCamera _camera;
        private float _currentAspect;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _currentAspect = 0;
            UpdateSize();
        }

        private void Update()
        {
            if (!isAlwaysUpdate && Application.isPlaying)
            {
                return;
            }
            UpdateSize();
        }

        private void UpdateSize()
        {
            var currentAspect = (float)Screen.width / (float)Screen.height;
            if (Mathf.Approximately(_currentAspect, currentAspect))
            {
                return;
            }
            _currentAspect = currentAspect;

            var baseAspect = baseScreenSize.x / baseScreenSize.y;

            // カメラの値の更新.
            if (baseType == AdjustType.Height || (baseAspect < _currentAspect && baseType != AdjustType.Width))
            {
                _camera.m_Lens.FieldOfView = perspectiveFieldOfView;
            }
            else
            {
                var horizontalFov = CalcHorizontalFov(perspectiveFieldOfView, baseAspect);
                _camera.m_Lens.FieldOfView = CalcVerticalFov(horizontalFov, _currentAspect);
            }
        }
        private static float CalcVerticalFov(float horizontalFov, float aspect)
        {
            return Mathf.Atan(Mathf.Tan(horizontalFov / 2f * Mathf.Deg2Rad) / aspect) * 2f * Mathf.Rad2Deg;
        }
        private static float CalcHorizontalFov(float verticalFov, float aspect)
        {
            return Mathf.Atan(Mathf.Tan(verticalFov / 2f * Mathf.Deg2Rad) * aspect) * 2f * Mathf.Rad2Deg;
        }
    }
}
