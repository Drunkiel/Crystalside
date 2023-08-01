using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool isNPCStopped;
    public bool isNPCTalking;

    public NPCInfo _info;
    private NPCMovement _movement;

    private void Start()
    {
        _movement = GetComponent<NPCMovement>();

        _info.npcTag.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = _info.npcRole.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.RotateTo(_info.npcTag.transform, GameObject.Find("Player").transform.position);
    }
}