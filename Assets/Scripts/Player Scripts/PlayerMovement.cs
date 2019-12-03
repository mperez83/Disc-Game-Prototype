using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionMode { Brake, Boost }

public class PlayerMovement : MonoBehaviour
{
    PlayerData playerData;

    public bool useSpeedLimit;
    public float speedLimit;

    public ActionMode actionMode;

    //Brake stuff
    [Range(0.0f, 1.0f)]
    public float brakePower;
    bool braking;
    public Transform brakeAura;
    Vector3 vel;

    //Boost stuff
    public float boostPower;
    bool boosting;

    public Canvas playerCanvas;

    Rigidbody2D rb;



    void Start()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        brakeAura.GetComponent<SpriteRenderer>().color = playerData.playerColors[playerData.GetPlayerNum() - 1];
    }

    void Update()
    {
        //Handle switching action modes
        if (Input.GetButtonDown("P" + playerData.GetPlayerNum() + "_Alt_Action"))
        {
            if (actionMode == ActionMode.Brake) actionMode = ActionMode.Boost;
            else actionMode = ActionMode.Brake;

            braking = false;
            boosting = false;
        }

        //Handle action input
        if (Input.GetButton("P" + playerData.GetPlayerNum() + "_Action"))
        {
            switch (actionMode)
            {
                case ActionMode.Brake:
                    braking = true;
                    break;

                case ActionMode.Boost:
                    boosting = true;
                    break;
            }
        }
        else
        {
            switch (actionMode)
            {
                case ActionMode.Brake:
                    braking = false;
                    break;

                case ActionMode.Boost:
                    boosting = false;
                    break;
            }
        }

        //Handle brake aura
        if (braking && actionMode == ActionMode.Brake)
        {
            rb.velocity = rb.velocity * (1 - brakePower);
            brakeAura.localScale = Vector3.SmoothDamp(brakeAura.localScale, new Vector3(3, 3, 3), ref vel, 0.15f);
        }
        else
        {
            brakeAura.localScale = Vector3.SmoothDamp(brakeAura.localScale, new Vector3(1, 1, 1), ref vel, 0.15f);
        }

        //Handle flying off into infinity
        if (GameManager.instance.IsTransformOffCamera(transform))
        {
            transform.position = new Vector2(0, 0);
            print("Zoop!");
        };

        //Update position of personal canvas
        playerCanvas.transform.position = transform.position;
    }

    void FixedUpdate()
    {
        //Handle boosting
        if (boosting && actionMode == ActionMode.Boost)
        {
            rb.AddForce(TrigUtilities.DegreesToVector(playerData.GetPlayerAngle()) * boostPower);
        }
    }



    public void ApplyForce(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force);

        if (useSpeedLimit)
        {
            if (rb.velocity.magnitude > speedLimit)
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
        }
    }

    public ActionMode GetActionMode() { return actionMode; }
    public bool GetBraking() { return braking; }
    public bool GetBoosting() { return boosting; }
}