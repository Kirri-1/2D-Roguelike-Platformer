using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EntryWay))]
public class TransitionTrigger : MonoBehaviour
{
    CircleCollider2D _collider;
    SceneTransitions _sceneTransitions;
    EntryWay _entryWay;
    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;

        _sceneTransitions = SceneTransitions.Instance;
        if (_sceneTransitions == null)
        {
            Debug.LogError("SceneTransitions component not found in the scene. Please ensure there is a SceneTransitions component present.");
        }

         _entryWay = GetComponent<EntryWay>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        { 
            _sceneTransitions.StartTransitionCoroutine(_entryWay.NewSceneReference, _entryWay.NewEntryWayName);
        }
    }
}
