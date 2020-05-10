using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreraController : MonoBehaviour
{
    public Transform Player;
    public Transform transform;
    private Vector3 newPosition;
    public float offsetz = -10f;
    public float offsety = 1.25f;

    public float lerpFactor;

    void Start()
    {
        newPosition = new Vector3();
        newPosition.z = offsetz;
    }

    void Update()
    {
        newPosition.x = Player.position.x;
        newPosition.y = Player.position.y + offsety;
        transform.position = Vector3.Lerp(transform.position, newPosition, lerpFactor);
    }
}
