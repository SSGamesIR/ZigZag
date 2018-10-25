using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    public GameObject particle;

	[SerializeField]
	private float Speed;
    bool started;
    bool gameover;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        started = false;
        gameover = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(Speed, 0, 0);
                started = true;

                GameManager.instance.StartGame();
            }
        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (! Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameover = true;
            rb.velocity = new Vector3(0, -25, 0);

            Camera.main.GetComponent<CameraFollow>().gameover = true;

            GameManager.instance.GameOver();
        }

        if (Input.GetMouseButtonDown(0) && !gameover)
        {
            SwitchDirection();
        }
	}

    void SwitchDirection() {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(Speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, Speed);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

            Destroy(col.gameObject);
            Destroy(part, 1f);
        }
    }
}
