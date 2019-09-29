using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItemResponse : MiscItem
{
    [SerializeField] private int first;
    [SerializeField] private int second;
    [SerializeField] private int third;
    [SerializeField] private int fourth;

    private ServiceBattle serviceBattle;

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
                serviceBattle.GetComponent<ServiceBattle>().AffectLevels(first, second, third, fourth);
                Destroy(this.gameObject);
            }
        }
    }
}
