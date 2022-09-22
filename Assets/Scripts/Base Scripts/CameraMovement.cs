using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CameraMinMaxPos CameraMinMax;
    public Transform target;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        minPos = CameraMinMax.getStartPosMin();
        maxPos = CameraMinMax.getStartPosMax();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
    
    public void BeginKick()
    {
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickActive", false);
    }
}