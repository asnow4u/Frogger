using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{

    [SerializeField] private GameObject smallPrefab;
    [SerializeField] private GameObject largePrefab;
    [SerializeField] private float objectMoveSpeed;
    [SerializeField] private float timer;

    private bool _isSpace;

    void Start() {
      _isSpace = true;
    }

    // Update is called once per frame
    void Update()
    {

      //Check for free space
      if (_isSpace) {

        //Randomize when spawn happens
        if (Random.Range(0f, 200f) < 1f) {

          _isSpace = false;
          StartCoroutine(DelayTimer());

          //Randomize big or small object
          if (Random.Range(0f, 2f) < 1f) {
            GameObject vehicle = Instantiate(smallPrefab, transform.position, transform.rotation);
            vehicle.GetComponent<ObjectController>().SetMoveSpeed(objectMoveSpeed);

          } else {
            GameObject vehicle = Instantiate(largePrefab, transform.position, transform.rotation);
            vehicle.GetComponent<ObjectController>().SetMoveSpeed(objectMoveSpeed);
          }
        }
      }
    }

    //Delay spawn of next object to maintain space
    private IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(timer);

        _isSpace = true;
    }

}
