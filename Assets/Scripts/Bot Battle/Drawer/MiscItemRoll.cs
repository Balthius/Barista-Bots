using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItemRoll : MiscItem
{
    private ServiceBattle serviceBattle;

    [SerializeField] private int modNum;
    [SerializeField] private float modNumMul;

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (!isHeld)
        {
            serviceBattle = FindObjectOfType<ServiceBattle>();
            if (collision.tag == "UseItemBar")
            {
                serviceBattle.GetComponent<ServiceBattle>().ModifyNumberByItem(modNum,modNumMul);
                Destroy(this.gameObject);
            }
        }
    }
}
