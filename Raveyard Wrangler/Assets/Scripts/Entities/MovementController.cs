using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    Vector2 m_velocity = Vector2.zero;
    Transform m_transform;

    // Start is called before the first frame update
    void Awake()
    {
        m_velocity.x = 10;
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_transform.transform.position += (Vector3)((m_velocity/100) * Time.fixedDeltaTime);
    }
}
