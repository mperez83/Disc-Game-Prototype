using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimHelper : MonoBehaviour
{
    public MeleeBase meleeBase;

    public void ActivateHitbox()
    {
        meleeBase.ActivateHitbox();
    }

    public void DeactivateHitbox()
    {
        meleeBase.DeactivateHitbox();
    }

    public void ClearDamagedPlayers()
    {
        meleeBase.ClearDamagedPlayers();
    }
}