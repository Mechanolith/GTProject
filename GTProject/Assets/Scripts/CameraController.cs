using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float heightOffset;
    [SerializeField] private float rotateSpeed;
    private CinemachineFreeLook currentCamera;

    public void OrbitStack(Stack _stack)
    {
        if(currentCamera != null)
        {
            currentCamera.m_XAxis.m_MaxSpeed = 0;
            currentCamera.Priority = 0;
        }

        currentCamera = _stack.Camera;
        currentCamera.Priority = 10;
        currentCamera.m_XAxis.m_MaxSpeed = 0;

        for (int i = 0; i < currentCamera.m_Orbits.Length; ++i)
        {
            currentCamera.m_Orbits[i].m_Height = _stack.StackHeight + heightOffset;
        }
    }

    public void EnableRotation()
    {
        currentCamera.m_XAxis.m_MaxSpeed = rotateSpeed;
    }

    public void DisableRotation()
    {
        currentCamera.m_XAxis.m_MaxSpeed = 0;
    }
}
