using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class DialogOption
{
    public List<string> dialogTexts = new List<string>();
    public string[] responseTexts;
}

[System.Serializable]
public class DialogAction
{
    [SerializeField] private bool[] dialogActions;
    public UnityEvent[] dialogEvents;
}

[CreateAssetMenu()]
public class DialogData : ScriptableObject
{
    public List<DialogOption> _dialogOptions = new List<DialogOption>();
    public List<DialogAction> _dialogActions = new List<DialogAction>();
}
