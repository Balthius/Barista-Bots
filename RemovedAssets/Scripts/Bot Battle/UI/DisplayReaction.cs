using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayReaction : MonoBehaviour
{
    [SerializeField] private Sprite[] employeeReactions;
    [SerializeField] private Sprite[] customerReactions;


    [SerializeField] private GameObject employeeCurReact;
    [SerializeField] private GameObject customerCurReact;

    public void ProcessReaction(int empRea, int cusRea)// Where am I using this?
    {
        employeeCurReact.GetComponent<SpriteRenderer>().sprite = employeeReactions[empRea];

        customerCurReact.GetComponent<SpriteRenderer>().sprite = customerReactions[cusRea];
    }







    
}
