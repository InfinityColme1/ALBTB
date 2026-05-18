using UnityEngine;
using UnityEngine.Events;

public class InteractionComponent : MonoBehaviour
{
    [Header("Interaction Logic Objects")]
    [Tooltip("The inputs this component will receive")]
    [SerializeField] private InputManager _source;

    [Tooltip("Event invoked when an interaction returns a collectable object")]
    public UnityEvent<CollectableObject> onCollectable;

    [Tooltip("Current object that can be interacted with")]
    private InteractiveComponent _currentInteractive;


    private void Awake()
    {
        if (!GetComponent<BoxCollider>()) Debug.LogError("This component needs a Box Collider");
        InputManager temp = gameObject.GetComponentInParent<InputManager>();
        _source =  temp ? temp : _source;
        if (!_source) Debug.LogError("This component needs an InputManager instance attached in this GameObject or in the parent");
    }

    private void Start()
    {
        _source.onInteract.AddListener(Interact);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_currentInteractive) _currentInteractive = other.gameObject.GetComponent<InteractiveComponent>();
        if (_currentInteractive) _currentInteractive.setHighlight();
    }

    private void OnCollisionExit(Collision collision)
    {
        _currentInteractive = null;
    }

    private void Interact()
    {
        if (_currentInteractive)
        {
            CollectableObject collectable = _currentInteractive.Interact();
            if (collectable) onCollectable.Invoke(collectable);
        }
    }
}
