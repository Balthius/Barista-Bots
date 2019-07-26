using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestObj", menuName = "ScriptableObjects/TestScriptableObject", order = 1)]
public class TestScriptObj : ScriptableObject
{
    public string testScriptObjName;

    private List<TestListener> testList = new List<TestListener>(5);
    private void Start()
    {
        
    }

    public void Register(TestListener test)
    {
        testList.Add(test);
    }

    public void Deregister(TestListener test)
    {
        testList.Remove(test);
    }
    public void Notify(string passVal)
    {
        for (int i = 0; i < testList.Count; i++)
        {
            testList[i].React(passVal);

        }
    }

}
