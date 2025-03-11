using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_PlayerCamera : MonoBehaviour
{
    public static float followSpeed = 0f;
    public float sensitivity = 100f;
    public float clampAngle = 70f;

    private float rotX = 0.0f;
    public static float rotY = 0.0f;
    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    public Transform objectTofollow;
    public FloatingJoystick cameraJoy;
    void Start(){
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;//megnitude는 벡터의 길이를 반환한다.
    }
    void FixedUpdate()
    {
        rotX += -(cameraJoy.Vertical) * sensitivity * Time.deltaTime;
        rotY += cameraJoy.Horizontal * sensitivity * Time.deltaTime;       
        /* rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;//이부분을 수정하여 카메라 구현가능
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; */

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);//clamp는 최소값과 최대값을 정해주는 함수

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = rot;

    }
    void LateUpdate(){//Update가 실행후 그다음 실행되는 업데이트 함수이다.
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);
        //MoveTowards는 첫번째 인자의 위치에서 두번째 인자의 위치로 이동한다.

        finalDir = transform.TransformDirection(dirNormalized*maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit)){//Linecast는 두개의 위치를 연결하는 선을 그려주는 함수이다 그리고 그 선이 충돌하는지 확인한다.
        // out hit은 충돌한 오브젝트의 정보를 담는다.
            finalDistance = Mathf.Clamp(hit.distance,  minDistance, maxDistance);
        }
        else{
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        //Lerp는 첫번째 인자의 위치에서 두번째 인자의 위치로 이동한다.
    }

    public void CameraLocationFix(){
        realCamera.position = objectTofollow.position;
    }
}