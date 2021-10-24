using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private int foregroundMatIndex;

    private CardData _cardData;

    public void AssignData(CardData cardData) {
        _cardData = cardData;
        UpdateTexture(_cardData.Texture);
    }

    private void UpdateTexture(Texture texture) {
        var mat = renderer.materials[foregroundMatIndex];
        mat.mainTexture = texture;
    }
}
