using UnityEngine;

[ExecuteInEditMode]
public class ChildColliderRemover : MonoBehaviour
{
    [ContextMenu("Remove All Colliders")]
    private void RemoveColliders()
    {
        var _childGameobjects = gameObject.GetComponentsInChildren<Collider>();

        foreach (var collider in _childGameobjects)
        {
            DestroyImmediate(collider);
        }
    }
}
