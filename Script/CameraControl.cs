using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraControl : MonoBehaviour
{
    public Transform cam;
    int mask = (1 << 11)| (1 << 12);
    public float distance = 4f;
    public float zoomSpeed = 3.0f;
    public float cameraSpeed = 5f;
    private Vector3 velocity = Vector3.zero;

    RaycastHit hitInfo;

    // 상점 메뉴
    public GameObject WeaponStore;
    public GameObject itemStore;
    public GameObject guildManager;

    // 캐릭터 UI
    public GameObject inventory;
    public GameObject equipment;
    public GameObject request;
    public GameObject option;
    public GameObject skill;
    private void FixedUpdate()
    {
        if(MouseControl())
        {
            CameraRaycast();
            LookAround();
            CameraZoom();
        }
    }
    bool MouseControl()
    {
        if (!inventory.activeSelf && !equipment.activeSelf && !option.activeSelf && !WeaponStore.activeSelf && !itemStore.activeSelf && !guildManager.activeSelf && !request.activeSelf&&!skill.activeSelf)
        {
            return true;
        }
        return false;
    }

    private void CameraRaycast()
    {
        float camDis = Vector3.Distance(transform.position, cam.position);

        Vector3 target = -transform.forward * cam.position.z + transform.up * cam.position.y;

        Physics.Raycast(transform.position, target, out hitInfo, camDis, mask);
        if (hitInfo.point != Vector3.zero)
        {
            cam.position = hitInfo.point;
            if (hitInfo.transform.tag == "Loop")
            {
                cam.Translate(new Vector3(0.2f, -0.2f, 0));
            }
            else
            {
                cam.Translate(new Vector3(0.2f, 0.3f, 0));
            }
        }
    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = transform.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y * cameraSpeed;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -35, 35f);
        }
        else
        {
            x = Mathf.Clamp(x, 300f, 361f);
        }

        transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x * cameraSpeed, camAngle.z);
    }

    void CameraZoom()
    {
        Vector3 reverseDist = new Vector3(0f, -1f, distance);
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, transform.position - cam.transform.rotation * reverseDist, ref velocity, 0.2f);
        if (distance <= 2f)
        {
            distance = 2f;
        }
        if (distance >= 15f)
        {
            distance = 15f;
        }
    }
}
