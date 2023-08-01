using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeExit : ObjectInteractive
{
    public ScreenWhite ScreenWhite;

    public override bool Interact()
    {
        return true;
    }

    new private void OnTriggerEnter(Collider other)
    {
            if (GameController.HasProp("ExitPassword"))
            {
                SetTipMessage("");
               ScreenWhite.StartWhitening();
            }
            else
            {
                SetTipMessage("Need password of the exit.");
            }
            base.OnTriggerEnter(other);
    }

}
