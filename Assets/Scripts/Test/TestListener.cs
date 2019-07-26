using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class MyStringEvent : UnityEvent<string>
{

}
public class TestListener : MonoBehaviour
{
    public MyStringEvent testAction;
    public TestScriptObj testScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        print("isEnabled");
        testScript.Register(this);
    }

    public void React(string passVal)
    {
        testAction.Invoke(passVal);
    }

    void OnDestroy() 
    {
        print("isDestroyed");
        testScript.Deregister(this);
    }

    void OnDisable()
     {
        print("isDisabled");
        testScript.Deregister(this);
    }

    public void PrintOne()
    {
        print("SomethingOne");
    }

    public void PrintTwo()
    {
        print("SomethingTwo");
    }

    public void CreateString(string words)
    {
        print(words);
    }
}
