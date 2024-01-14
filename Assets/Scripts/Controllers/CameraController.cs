using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        [SerializeField] Camera mainCamera;
        [SerializeField] float shakeStrength;
        [SerializeField] float shakefrequency;
        [SerializeField] float shakeTime;
        [SerializeField] NoiseSettings noiseSetting;
        [SerializeField] CinemachineVirtualCamera virtualCamera; // temporary
        public Camera MainCamera => mainCamera;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        void OnDestroy()
        {
            Instance = null;
        }

        [Button]
        public void OnShakeCamera()
        {
            StartCoroutine(CorOnShakeCamera());

        }

        private IEnumerator CorOnShakeCamera()
        {
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
