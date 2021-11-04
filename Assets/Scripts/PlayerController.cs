using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 mov_val;
    int speed = 500;
    private Vector2 _rotation;
    private CharacterController characterController;
    private float _sensitivity = 3f;
    public GameObject bullet;
    public static Vector3 playerPosition;
    public Camera gameOverCam;
    public static int resource;
    public GameObject gunTower;
    public GameObject buildBar;
    GameObject buildBarIns = null;
    GunTower buildingGunTower = null;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        buildBarIns = Instantiate(buildBar);
        buildBarIns.transform.parent = null;
        buildBarIns.SetActive(false);
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        var buildAction = GetComponent<PlayerInput>().actions["Build"];
            buildAction.performed += content =>
            {
                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.distance < 10.0f)
                {
                    if (hit.collider.gameObject.CompareTag("GunTower"))
                    {
                        buildingGunTower = hit.collider.gameObject.GetComponent<GunTower>();
                        if (buildingGunTower.isFinished())
                        {
                            buildingGunTower = null;
                        }
                        return;
                    }
                    if (hit.collider.gameObject.CompareTag("Ground") && resource >= 300)
                    {
                        resource -= 300;
                        var raycastPos = hit.point;
                        var obj = Instantiate(gunTower, hit.point + new Vector3(0f, 1f, 0f), Quaternion.Euler(0, 0, 0));
                        buildingGunTower = obj.GetComponent<GunTower>();
                        return;
                    }
                }
            };
            buildAction.canceled += content =>
            {
                buildingGunTower = null;
            };
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * mov_val.y + transform.right * mov_val.x;
        characterController.SimpleMove(moveDirection * speed * Time.fixedDeltaTime);
        playerPosition = transform.position;
        build();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(_rotation);
        ScoreText.setText(resource);
    }

    private void OnMove(InputValue value)
    {
        mov_val = value.Get<Vector2>();
    }
    private void OnLook(InputValue value)
    {
        Vector2 lookValue = value.Get<Vector2>();
        _rotation.x += -lookValue.y * 0.022f * _sensitivity;
        if (_rotation.x > 90)
        {
            _rotation.x = 90;
        }
        if (_rotation.x < -90)
        {
            _rotation.x = -90;
        }
        _rotation.y += lookValue.x * 0.022f * _sensitivity;
    }
    private void OnFire(InputValue value)
    {
        float isFire = value.Get<float>();
        if (isFire > 0f)
        {
            Instantiate(bullet, transform.position + transform.forward * 2, transform.rotation);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gameOverCam.gameObject.SetActive(true);
    }

    private void build()
    {
        if (buildingGunTower != null && resource > 0 && !buildingGunTower.isFinished())
        {
            resource -= 1;
            buildingGunTower.build(0.2f * Time.fixedDeltaTime);
            buildBarIns.transform.position = transform.position * 0.25f + buildingGunTower.transform.position * 0.75f;
            buildBarIns.transform.LookAt(buildingGunTower.transform.position);
            buildBarIns.GetComponent<BuildProcessBar>().setValue(buildingGunTower.buildingProgress);
            buildBarIns.SetActive(true);
        }
        else
        {
            buildBarIns.SetActive(false);
        }
    }
}
