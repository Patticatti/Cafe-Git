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
    public Sprite necklace1;
    public Sprite bracelet1;
    public Sprite ring1;
    public Sprite earring1;


    private void Awake()
    {
        spriteLists = new Dictionary<ItemType, List<Sprite>>
        {
            { ItemType.necklace, necklaces },
            { ItemType.bracelet, bracelets},
            { ItemType.ring, rings},
            { ItemType.earring, earrings}
        };
        /*
        necklace1 = Resources.Load<Sprite>("Sprites/Objects/accessories_0");
        bracelet1 = Resources.Load<Sprite>("Sprites/Objects/accessories_1");
        ring1 = Resources.Load<Sprite>("Sprites/Objects/accessories_2");
        earring1 = Resources.Load<Sprite>("Sprites/Objects/accessories_3");
        */
        // Add the loaded sprites to the list
        necklaces.Add(necklace1);
        bracelets.Add(bracelet1);
        rings.Add(ring1);
        earrings.Add(earring1);
    }

    public Sprite GetRandomSprite()
    {
        if (spriteLists.Count == 0)
        {
            Debug.LogWarning("The sprite list is empty.");
            return null;
        }
        List<ItemType> keyList = new List<ItemType>(spriteLists.Keys);
        int randomIndex = Random.Range(0, spriteLists.Count);
        ItemType randomKey = keyList[randomIndex];
        List<Sprite> spriteList = spriteLists[randomKey];
        int randomSpriteIndex = Random.Range(0, spriteList.Count);
        Sprite randomSprite = spriteList[randomSpriteIndex];
        if (randomSprite == null)
        {
            Debug.Log("fck");
        }
        return randomSprite;
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
