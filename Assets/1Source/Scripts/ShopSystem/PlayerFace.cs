using UnityEngine;

public class PlayerFace : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material[] _faces;

    public void Set(int index)
    {
        _meshRenderer.material = _faces[index];
    }
}
