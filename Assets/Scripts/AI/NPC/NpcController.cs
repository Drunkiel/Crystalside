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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
