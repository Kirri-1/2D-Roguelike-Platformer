using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Vector2 spawnOffset;

    
    [SerializeField] private string roomName;
    [SerializeField] private AudioClip roomMusic;

    
    public List<Transform> SpawnPoints => spawnPoints;
    public Transform CameraPosition => cameraPosition;
    public Vector2 SpawnOffset => spawnOffset;
    public string RoomName => roomName;
    public AudioClip RoomMusic => roomMusic;

    
    public bool IsActive => RoomManager.Instance?.CurrentActiveRoom == this;

    private void OnDrawGizmos()
    {
        // Visual aids for the editor
        Gizmos.color = Color.green;
        foreach (var point in spawnPoints)
        {
            if (point != null) Gizmos.DrawSphere(point.position, 0.3f);
        }

        if (cameraPosition != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(cameraPosition.position, new Vector3(2, 2, 0));
        }
    }
}