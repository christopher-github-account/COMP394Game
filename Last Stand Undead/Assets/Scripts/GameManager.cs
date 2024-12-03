using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Camera.main.gameObject.AddComponent<CinemachineBrain>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
