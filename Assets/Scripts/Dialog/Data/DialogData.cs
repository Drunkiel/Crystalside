using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class DialogOption
{
    public string[] dialogTexts;
    public string[] responseTexts;
}

[System.Serializable]
public class DialogAction
{
    public bool[] isEvent;
    public UnityEvent[] dialogEvents;
}

[CreateAssetMenu()]
public class DialogData : ScriptableObject
{
    public List<DialogOption> _dialogOptions = new List<DialogOption>();
    public List<DialogAction> _dialogActions = new List<DialogAction>();
}
