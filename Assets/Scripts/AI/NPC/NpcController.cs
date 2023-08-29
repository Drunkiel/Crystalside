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

        _info.npcCanvas.UpdateTitle(_info.npcRole.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        _movement.RotateTo(_info.npcCanvas.transform, GameObject.Find("Player").transform.position);
    }
}
