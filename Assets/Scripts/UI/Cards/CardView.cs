using UnityEngine;
using UnityEngine.UI;
using Game.Data;
#if TMP_PRESENT
using TMPro;
#endif

namespace Game.UI
{
    /// <summary>
    /// Renders a CardDefinition onto UI elements.
    /// Attach to a prefab representing a card in hand/library.
    /// </summary>
    public class CardView : MonoBehaviour
    {
    [Header("UI References")] 
    public Text nameText; 
    public Text costText; 
    public Text descriptionText; 
    public Image artworkImage; 
    public Image frameImage; 
    public Image rarityStripe;
#if TMP_PRESENT
    public TMP_Text tmpName; 
    public TMP_Text tmpCost; 
    public TMP_Text tmpDescription;
#endif

        [Header("Optional")] public Color attackColor = new(0.85f, 0.3f, 0.3f); public Color defenseColor = new(0.3f, 0.6f, 0.9f); public Color utilityColor = new(0.3f, 0.8f, 0.5f); public Color curseColor = new(0.45f, 0.1f, 0.5f);
        public Color commonColor = Color.gray; public Color rareColor = new(0.2f,0.4f,1f); public Color epicColor = new(0.6f,0.2f,0.9f); public Color legendaryColor = new(1f,0.6f,0.1f);

        private CardDefinition _bound;

        public void Bind(CardDefinition def, Sprite artwork = null)
        {
            _bound = def;
            if (def == null)
            {
                Clear();
                return;
            }
            var displayName = def.DisplayName;
            var cost = def.Cost.ToString();
            var desc = def.Description;
#if TMP_PRESENT
            if (tmpName) tmpName.text = displayName; 
            if (tmpCost) tmpCost.text = cost; 
            if (tmpDescription) tmpDescription.text = desc;
#endif
            if (nameText) nameText.text = displayName; if (costText) costText.text = cost; if (descriptionText) descriptionText.text = desc;
            if (artworkImage && artwork) artworkImage.sprite = artwork;

            // Type color accent
            var typeColor = GetTypeColor(def.Type);
            if (frameImage) frameImage.color = typeColor;

            // Rarity stripe color
            if (rarityStripe) rarityStripe.color = GetRarityColor(def.Rarity);
        }

        private Color GetTypeColor(CardType type) => type switch
        {
            CardType.Attack => attackColor,
            CardType.Defense => defenseColor,
            CardType.Utility => utilityColor,
            CardType.Curse => curseColor,
            _ => Color.white
        };

        private Color GetRarityColor(Rarity rarity) => rarity switch
        {
            Rarity.Common => commonColor,
            Rarity.Rare => rareColor,
            Rarity.Epic => epicColor,
            Rarity.Legendary => legendaryColor,
            _ => Color.white
        };

        public void Clear()
        {
#if TMP_PRESENT
            if (tmpName) tmpName.text = ""; 
            if (tmpCost) tmpCost.text = ""; 
            if (tmpDescription) tmpDescription.text = "";
#endif
            if (nameText) nameText.text = ""; if (costText) costText.text = ""; if (descriptionText) descriptionText.text = "";
            if (artworkImage) artworkImage.sprite = null;
            if (frameImage) frameImage.color = Color.white;
            if (rarityStripe) rarityStripe.color = Color.white;
            _bound = null;
        }

        // Example hover highlight (can be wired via EventTrigger in editor)
        public void OnHover(bool isHover)
        {
            if (frameImage) frameImage.transform.localScale = isHover ? Vector3.one * 1.05f : Vector3.one;
        }
    }
}
