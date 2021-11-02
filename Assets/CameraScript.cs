using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public Transform camTransform;
    public float LerpSpeed = .6f;
    public PlayerMovement pm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = new Vector3(playerTransform.position.x, playerTransform.position.y, camTransform.position.z) + new Vector3(pm.facing.normalized.x,pm.facing.normalized.y,0);
        camTransform.position = Vector3.Lerp(camTransform.position, newpos, LerpSpeed * Time.deltaTime);
    }
}
