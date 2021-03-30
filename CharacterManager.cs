using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public bool Finish = false;

    public float speed= 100f;

    public float sideSpeed = 150f;

    public GameObject wall1, wall2;

    public float HP=100;

    private bool WallsCanDamage = false;

    public GameObject SliderSpeed;

    public GameObject SliderHP;

    private GameObject CurrentHit = null;

    public GameObject boom;

    private bool final;

    public bool start = false, dead=false;

    public GameObject StartCanvas, LoseCanvas, WinCanvas;

    void Start()
    {
        final = true;

        StartCanvas.SetActive(true);

        GetComponent<Animator>().Play("BasicMotions@Idle01");
    }

    void Update()
    {
        if (Finish == false && start == true && dead ==false)
        {
            GetComponent<Animator>().Play("BasicMotions@Run01");

            speed = GameObject.Find("Speed").GetComponent<Slider>().value;

            GameObject.Find("HP").GetComponent<Slider>().value = HP;

            GetComponent<Rigidbody>().AddForce(transform.forward * 100 * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A) && GetComponent<CapsuleCollider>().bounds.Intersects(wall1.GetComponent<BoxCollider>().bounds)==false)
            {
                GetComponent<Rigidbody>().AddForce(-transform.right * 100 * sideSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) && GetComponent<CapsuleCollider>().bounds.Intersects(wall2.GetComponent<BoxCollider>().bounds)==false)
            {
                GetComponent<Rigidbody>().AddForce(transform.right * 100 * sideSpeed * Time.deltaTime);
            }

            if (GetComponent<CapsuleCollider>().bounds.Intersects(wall1.GetComponent<BoxCollider>().bounds) ||
                GetComponent<CapsuleCollider>().bounds.Intersects(wall2.GetComponent<BoxCollider>().bounds))
            {
                if (!WallsCanDamage)
                {
                    StartCoroutine(WaitForSeconds(1f));
                }

            }

        }
        else if (Finish == true)
        {
            if (final == true)
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * 100 * speed * Time.deltaTime);
                speed = Mathf.Lerp(speed, 0,1f*Time.deltaTime);
                GetComponent<Animator>().Play("AlienWinRotate");
                GameObject.Find("ufo").GetComponent<Animator>().Play("ufoanim");
                WinCanvas.SetActive(true);
                GameObject[] Explosions = GameObject.FindGameObjectsWithTag("Explosion");
                foreach (GameObject i in Explosions)
                {
                    Destroy(i);
                }
                final = false;
            }

        }
        if (HP <= 0)
        {
            dead = true;
            LoseCanvas.SetActive(true);
            GetComponent<Animator>().Play("BasicMotions@Idle01");
            GameObject[] Explosions = GameObject.FindGameObjectsWithTag("Explosion");
            foreach (GameObject i in Explosions)
            {
                Destroy(i);
            }
        }
    }

    private IEnumerator WaitForSeconds(float time)
    {
        WallsCanDamage = true;
        yield return new WaitForSeconds(time);
        HP = HP - 10;
        WallsCanDamage = false;
    }
    
    private void OnCollisionEnter(Collision coll)
    {        
        if (coll.collider.tag == "FireCube" && coll.gameObject!=CurrentHit)
        {
            Vector3 BoomPos = coll.gameObject.transform.position;
            GameObject Boom = Instantiate(boom, BoomPos, Quaternion.identity);
            Destroy(coll.gameObject);
            HP = HP - 10;
        }

        CurrentHit = coll.gameObject;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Finish")
        {
            Finish = true;
            Debug.Log("Finish");
        }
    }

}

