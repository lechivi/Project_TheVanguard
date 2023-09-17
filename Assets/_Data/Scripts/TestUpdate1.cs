using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestUpdate1 : SaiMonoBehaviour
{
    public TMP_Text text;

    private void Awake()
    {
        this.text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UpdatePlayerHealth, this.OnListenerUpdateHealth);
            Debug.Log("Register");
        }
    }

    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UpdatePlayerHealth, this.OnListenerUpdateHealth);
            Debug.Log("Unregister");
        }
    }

    private void OnListenerUpdateHealth(object value)
    {
        Debug.Log("Trigger");
        if (value == null) return;
        if (value is TestUpdate test)
        {
            this.text.SetText(test.cur + "/" + test.max);
        }
    }
}
