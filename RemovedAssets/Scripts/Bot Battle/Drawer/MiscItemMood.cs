using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItemMood : MiscItem
{
    //controls which mood the customer bot has
    
    [SerializeField] private int affectedMood;

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (!isHeld)
        {
            if (collision.tag == "UseItemBar")
            {
                customerBot.SetMood(affectedMood);
                Destroy(this.gameObject);
            }
        }
    }
}
