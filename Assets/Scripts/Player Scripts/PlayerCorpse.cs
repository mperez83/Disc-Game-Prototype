using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorpse : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D m_collider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            Destroy(rb);
            Destroy(m_collider);
            Destroy(this);
        }
    }
}