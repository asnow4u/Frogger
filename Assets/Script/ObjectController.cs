using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    private float _moveSpeed;

    void Start() {
      Destroy(gameObject, 25f);
    }

    //Move forward each frame 
    void Update()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    //Adjust movement speed
    public void SetMoveSpeed(float speed)
    {
      _moveSpeed = speed;
    }
}
