using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimHelper : MonoBehaviour
{
    public MeleeBase meleeBase;

    public void ToggleHitbox()
    {
        meleeBase.ToggleHitbox();
    }

    public void ClearDamagedPlayers()
    {
        meleeBase.ClearDamagedPlayers();
    }
}