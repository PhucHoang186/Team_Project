using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public enum CamType
    {
        PlayerCam,

    }

    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        [SerializeField] Camera mainCamera;
        [SerializeField] float shakeStrength;
        [SerializeField] float shakefrequency;
        [SerializeField] float shakeTime;
        [SerializeField] NoiseSettings noiseSetting;
        private VirtualCamera currentCam;
        private VirtualCamera[] cams;
        public Camera MainCamera => mainCamera;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            cams = GetComponentsInChildren<VirtualCamera>();
        }

        void OnDestroy()
        {
            Instance = null;
        }

        void Start()
        {
        }

        [Button]
        public void OnShakeCamera()
        {
            StartCoroutine(CorOnShakeCamera());

        }

        public void SetCurrentCam(CamType camType, Transform lookAt= null, Transform follow = null)
        {
            foreach (var cam in cams)
            {
                if (cam.cameraType == camType)
                {
                    cam.virtualCamera.Priority = 99;
                    currentCam = cam;
                }
                else
                {
                    cam.virtualCamera.Priority = 10;
                }
            }
            
            currentCam.SetLookAt(lookAt);
            currentCam.SetFollow(follow);
        }

        private IEnumerator CorOnShakeCamera()
        {
            CinemachineBasicMultiChannelPerlin noise = currentCam.virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_NoiseProfile = noiseSetting;
            noise.m_AmplitudeGain = shakeStrength;
            noise.m_FrequencyGain = shakefrequency;

            yield return new WaitForSecondsRealtime(shakeTime);
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
            Destroy(noise);
        }
    }
}
