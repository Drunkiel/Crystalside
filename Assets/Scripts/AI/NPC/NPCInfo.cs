using UnityEngine;

public enum Role
{
    Trader,
    Guard,
    Priest
};

[System.Serializable]
public class NPCInfo
{
    public float speed;
    public Role npcRole;
    public NpcCanvasController npcCanvas;
}
