using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelCodeButton : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] WheelCodeLock codeLock;

    public void OnClick()
    {
        int n = Convert.ToInt32(text.text);
        if (n == 9) { n = 0; return; }
        n++;
        text.text = n.ToString();
        codeLock.CheckCode();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
