using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private float heightOffset;
    [SerializeField] private float rotateSpeed;

    private void Awake()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = 0;
    }

    public void OrbitStack(Stack _stack)
    {
        freeLookCamera.Follow = _stack.transform;
        freeLookCamera.LookAt = _stack.transform;

        for(int i = 0; i < freeLookCamera.m_Orbits.Length; ++i)
        {
            freeLookCamera.m_Orbits[i].m_Height = _stack.StackHeight + heightOffset;
        }
    }

    public void EnableRotation()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = rotateSpeed;
    }

    public void DisableRotation()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = 0;
    }
}
