using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class cameraHandler : MonoBehaviour
{
    public Transform taregetTransform;
    public Transform cameraTransform;
    public Transform PivotTransform;
    private Transform myTransform;
    private Vector3 CameraTransformPosition;
    private Vector3 Camerafollowvelocity = Vector3.zero;
    public LayerMask ignoreLayer;


    public static cameraHandler singleton;

    public float lookSpeed= 0.1f;
    public float followSpeed= 0.1f;
    public float pivotSpeed = 0.3f;

    private float tagetpostion;
    private float defaultPosition;
    private float lookAngle;
    private float pviotAngle;

    public float maxPiviot = 35f;
    public float minPiviot = -35f;
    public float camerasphereRadius = 0.2f;
    public float camercolliosOffset = 0.2f;
    public float mincolliosionoffset = 0.2f;

private void Awake() {
    singleton= this;
    myTransform = transform;
    defaultPosition = cameraTransform.localPosition.z;
    ignoreLayer = ~(1 << 8 | 1 << 9| 1 << 10);
}
public void followtarget(float delta){
    Vector3 tagretPosition = Vector3.SmoothDamp(myTransform.position,taregetTransform.position,ref Camerafollowvelocity,delta/followSpeed);
    myTransform.position = tagretPosition;
    handleCameracolloion(delta);
}
public void handleCameraRotation(float delta,float mouseInputX,float mouseInputY){
    lookAngle += (mouseInputX *lookSpeed) / delta;
    pviotAngle -=(mouseInputY * pivotSpeed) / delta;
    pviotAngle = Mathf.Clamp(pviotAngle,minPiviot,maxPiviot);
    
    Vector3 rotation = Vector3.zero;
    rotation.y = lookAngle;
    Quaternion targertRotation= Quaternion.Euler(rotation);
    myTransform.rotation = targertRotation;

    rotation = Vector3.zero;
    rotation.x = pviotAngle;

    targertRotation = Quaternion.Euler(rotation);
    PivotTransform.localRotation = targertRotation;
}
private void handleCameracolloion(float delta){
    tagetpostion = defaultPosition;
    RaycastHit hit;
    Vector3 direction = cameraTransform.position - PivotTransform.position;
    direction.Normalize();

    if(Physics.SphereCast(PivotTransform.position,camerasphereRadius,direction,out hit,Mathf.Abs(tagetpostion),ignoreLayer)){
        float dis = Vector3.Distance(PivotTransform.position,hit.point);
        tagetpostion = -(dis - camercolliosOffset);
    }
    if(Mathf.Abs(tagetpostion) < mincolliosionoffset){
        tagetpostion = -mincolliosionoffset;

    }
    CameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z , tagetpostion,delta/0.2f);
    cameraTransform.localPosition = CameraTransformPosition;
}
}
}