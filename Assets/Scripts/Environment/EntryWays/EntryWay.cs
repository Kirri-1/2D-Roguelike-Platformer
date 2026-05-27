using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(CircleCollider2D))]
public class EntryWay : MonoBehaviour
{
    [SerializeField]
    string entryWayName;
    public string EntryWayName => entryWayName;

    [SerializeField]
    string newEntryWayName;
    public string NewEntryWayName => newEntryWayName;

    [SerializeField]
    AssetReference newSceneReference;
    public AssetReference NewSceneReference => newSceneReference;

    [SerializeField]
    Vector2 offset;

    public Vector2 Offset => offset;
}
