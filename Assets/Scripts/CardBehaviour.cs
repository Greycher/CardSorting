using UnityEngine;

public class CardBehaviour : MonoBehaviour {
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private int foregroundMatIndex;
    [SerializeField] private float width;

    private Card _card;

    public void AssignCard(Card card) {
        _card = card;
        UpdateTexture(_card.Texture);
    }

    private void UpdateTexture(Texture texture) {
        var mat = renderer.materials[foregroundMatIndex];
        mat.mainTexture = texture;
    }
}
