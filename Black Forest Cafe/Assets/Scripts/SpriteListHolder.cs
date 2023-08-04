using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListHolder : MonoBehaviour
{
    public List<Sprite> necklaces;
    public List<Sprite> bracelets;
    public List<Sprite> rings;
    public List<Sprite> earrings;
    public Dictionary<ItemType, List<Sprite>> spriteLists;

    private void Awake()
    {
        spriteLists = new Dictionary<ItemType, List<Sprite>>
        {
            { ItemType.necklace, necklaces },
            { ItemType.bracelet, bracelets},
            { ItemType.ring, rings},
            { ItemType.earring, earrings}
        };
    }

    private void Start()
    {
        Sprite necklace1 = Resources.Load<Sprite>("Sprites/Objects/accessories_0");
        Sprite bracelet1 = Resources.Load<Sprite>("Sprites/Objects/accessories_1");
        Sprite ring1 = Resources.Load<Sprite>("Sprites/Objects/accessories_2");
        Sprite earring1 = Resources.Load<Sprite>("Sprites/Objects/accessories_3");

        // Add the loaded sprites to the list
        necklaces.Add(necklace1);
        bracelets.Add(bracelet1);
        rings.Add(ring1);
        earrings.Add(earring1);
    }

    public List<Sprite> GetSpriteListByEnum(ItemType spriteEnum)
    {
        if (spriteLists.ContainsKey(spriteEnum))
        {
            return spriteLists[spriteEnum];
        }

        return new List<Sprite>();
    }

    public enum ItemType
    {
        necklace,
        bracelet,
        ring,
        earring
    }
}
