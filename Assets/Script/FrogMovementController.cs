using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Update summary
//NOTE: Not using physics to move frog

public class FrogMovementController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpDist;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Vector3 cameraStartPos;

    private GameObject camera;
    private Vector3 nextPos;
    private float jumpMovement;
    private bool _isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = false;
        jumpMovement = gravity;
        camera = GameObject.Find("Main Camera");
        camera.transform.position = cameraStartPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (_isGrounded) {

          //Check Input
          if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) {

            _isGrounded = false;
            transform.parent = null;
            transform.localEulerAngles = new Vector3(0f,0f,0f);
            jumpMovement = jumpForce;
            nextPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + jumpDist);
          }

          if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) {

            _isGrounded = false;
            transform.parent = null;
            transform.localEulerAngles = new Vector3(0f,180f,0f);
            jumpMovement = jumpForce;
            nextPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - jumpDist);
          }

          if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) {

            _isGrounded = false;
            transform.parent = null;
            transform.localEulerAngles = new Vector3(0f,270f,0f);
            jumpMovement = jumpForce;
            nextPos = new Vector3(transform.position.x - jumpDist, transform.position.y, transform.position.z);
          }

          if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) {

            _isGrounded = false;
            transform.parent = null;
            transform.localEulerAngles = new Vector3(0f,90f,0f);
            jumpMovement = jumpForce;
            nextPos = new Vector3(transform.position.x + jumpDist, transform.position.y, transform.position.z);
          }
        }

        else {

          //Frog Movement
          transform.position = Vector3.Lerp(transform.position, nextPos, moveSpeed * Time.fixedDeltaTime);
          transform.Translate(new Vector3(0f, jumpMovement, 0f) * Time.fixedDeltaTime);

          //Camera Movement
          Vector3 nextCameraPos = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z + cameraStartPos.z), moveSpeed);
          camera.transform.position = nextCameraPos;

          //Decrement Gravity
          jumpMovement -= gravity;

          //Cap Gravity
          if (jumpMovement < -9.8f) {
            jumpMovement = -9.8f;
          }
        }
    }


    void OnCollisionEnter(Collision col)
    {

        //Grounded when landing on platform
        if (col.gameObject.tag == "Platform") {
          _isGrounded = true;
        }

        if (col.gameObject.tag == "Platform_Log" || col.gameObject.tag == "Platform_Sinking") {
          _isGrounded = true;
          transform.SetParent(col.gameObject.transform);
        }

        if (col.gameObject.tag == "Finish") {
          GameController.instance.IncrementCompleteRuns();
          GameController.instance.SpawnFrog();
          Destroy(gameObject);
        }

        if (col.gameObject.tag == "DeathZone") {
          GameController.instance.IncrementAttemptCounter();
          GameController.instance.SpawnFrog();
          Destroy(gameObject);
        }
    }
}
