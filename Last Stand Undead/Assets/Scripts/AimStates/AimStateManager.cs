using Unity.Cinemachine;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipfireState Hipfire = new HipfireState();
    public AimState Aim = new AimState();

    [SerializeField] float mouseSense = 1;
    //public Unity.Cinemachine.AxisState xAxis, yAxis;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    [HideInInspector] public Animator animator;
    [HideInInspector] public CinemachineVirtualCamera camera;
    public float aimFOV = 40;
    [HideInInspector] public float hipFOV;
    [HideInInspector] public float currentFOV;
    public float fovSmoothSpeed = 10;

    public Transform aimPosition;
    [HideInInspector] public Vector3 actualAimPosition;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV = camera.m_Lens.FieldOfView;

        animator = GetComponent<Animator>();
        SwitchState(Hipfire);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //xAxis.Update(Time.deltaTime);
        //yAxis.Update(Time.deltaTime);
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, currentFOV, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            actualAimPosition = hit.point;
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
