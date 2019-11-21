using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    public abstract void ApplyForce(float angle, float force);
}