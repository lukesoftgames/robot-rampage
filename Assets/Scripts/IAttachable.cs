using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class IAttachable : MonoBehaviour
{
    public Rigidbody2D rb;
    private float health = 12.0f;
    private AudioSource audioData;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            destroy();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // if(transform.parent != null) rb.isKinematic = true;
        if (GetComponent<AudioSource>() != null)
        {
            audioData = GetComponents<AudioSource>()[1];
        }
    }

    public void attach(Transform anchor)
    {
        audioData.Play();
        transform.gameObject.layer = 0;
        rb.isKinematic = true;
        transform.SetParent(anchor);
        if (anchor != null)
        {
            rb.isKinematic = true;
        }
    }

    public void damage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            destroy();
        }
    }

    public void destroy()
    {
        List<Transform> children = new List<Transform>(GetComponentsInChildren<Transform>());
        foreach (Transform t in children)
        {
            RobotManager.pieces.Remove(t.gameObject);
            Destroy(t.gameObject);
        }
    }
}