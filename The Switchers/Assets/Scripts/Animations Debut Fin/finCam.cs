using UnityEngine;
using System.Collections;

public class finCam : MonoBehaviour
{

    public ameFinController ame;
    private Animator anim;
    private Vector3 pos;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ame.gameObject.activeInHierarchy)
            transform.position = new Vector3(transform.position.x, ame.transform.position.y, transform.position.z);
        if (!pos.Equals(transform.position))
        {
            pos = transform.position;
        }
        else if (ame.aBouge)
        {
            anim.speed = 0.1f;
        }
    }

    public void wait()
    {
        anim.speed = 0;
    }
}