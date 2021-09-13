using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillypadController : MonoBehaviour
{
    [SerializeField] private float sinkSpeed;
    [SerializeField] private float sinkTime;
    [SerializeField] private float riseTime;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float topLimit;

    private bool _sink;
    private bool _rise;

    // Start is called before the first frame update
    void Start()
    {
        _sink = false;
        _rise = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Sink to a certain point
        if (_sink && transform.position.y > bottomLimit) {
          transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);

          if (transform.position.y <= bottomLimit) {
            _sink = false;
            StartCoroutine(Rise());
          }
        }

        //Rise to a certain point
        if (_rise && transform.position.y < topLimit) {
          transform.Translate(Vector3.up * sinkSpeed * Time.deltaTime);

          if (transform.position.y >= topLimit) {
            _rise = false;
          }
        }
    }

    //Upon Collision Start Sinking
    private void OnCollisionEnter(Collision col)
    {
      if (col.gameObject.tag == "Player") {
        StartCoroutine(Sink());
      }
    }

    //Sink after a set time
    private IEnumerator Sink() {

      yield return new WaitForSeconds(sinkTime);

      _sink = true;
    }

    //Rise after a set time
    private IEnumerator Rise() {

      yield return new WaitForSeconds(riseTime);

      _rise = true;
    }
}
