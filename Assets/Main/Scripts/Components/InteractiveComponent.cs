using UnityEngine;
using UnityEngine.Events;

public class InteractiveComponent : MonoBehaviour
{
    [Header("Interactive settings")]
    [SerializeField] private bool _canInteract = true;
    [SerializeField] private Mesh _highlightableMesh;
    [SerializeField] private bool _onlyInteractOnce;
    [SerializeField] private bool _destroyOnInteract;

    [Header("Collectable settings")]
    [SerializeField] private bool _isCollectable;
    [SerializeField] private CollectableObject _collectable;

    public UnityEvent onInteracted;

    public void setCanInteract(bool newState) => _canInteract = !_onlyInteractOnce && newState;

    public bool getCantInteract() => _canInteract;

    public void setHighlight()
    {
        //TODO AQUÍ HAY QUE HACER QUE EL OBJETO SE HIGHLIGHTEE
    }

    public CollectableObject Interact()
    {
        if (_canInteract) 
        { 
            onInteracted.Invoke();
            if (_onlyInteractOnce) _canInteract = false;
            if (_destroyOnInteract) Destroy(this.gameObject);
        }

        return _isCollectable ? _collectable : null;
    }

}
