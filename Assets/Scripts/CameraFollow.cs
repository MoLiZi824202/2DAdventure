using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector2 minPostion;
    public Vector2 maxPostion;
    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector2 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPostion.x, maxPostion.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPostion.y, maxPostion.y);
                transform.position = Vector2.Lerp(transform.position, targetPos, speed);
            }
        }
    }

    public void SetCamPosLimit(Vector2 minPos,Vector2 maxPos)
    {
        minPostion = minPos;
        maxPostion = maxPos;
    }
}
