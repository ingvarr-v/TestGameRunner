using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCubes : MonoBehaviour
{

    public GameObject FireCube;

    private bool CanThrow = false;

    void Update()
    {
        if (GameObject.Find("Alien").GetComponent<CharacterManager>().Finish == false &&  GameObject.Find("Alien").GetComponent<CharacterManager>().start == true && GameObject.Find("Alien").GetComponent<CharacterManager>().dead == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("Alien").transform.position.z + 25
                + GameObject.Find("Alien").GetComponent<CharacterManager>().speed / 20);

            if (!CanThrow)
            {
                StartCoroutine(WaitForSeconds(1f));
            }
        }
    }

    private IEnumerator WaitForSeconds(float time)
    {
        CanThrow = true;
        yield return new WaitForSeconds(time);
        int Throw = Random.Range(1, 5);
        if (Throw == 1)
        {
            GameObject CurrentCube = Instantiate(FireCube, transform.position, Quaternion.identity);
            CurrentCube.GetComponent<Rigidbody>().AddForce(transform.up * 800);

        }
        CanThrow = false;
    }
}
