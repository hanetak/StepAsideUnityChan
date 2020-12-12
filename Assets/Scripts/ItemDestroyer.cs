using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    private float offset = 10.0f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.z < player.transform.position.z - offset)
        {
            Destroy(this.gameObject);
        }
    }
}
