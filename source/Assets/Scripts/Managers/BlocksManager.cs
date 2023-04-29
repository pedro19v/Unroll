using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    private Dictionary<ElementalColor, List<Block>> blocksByColor;
    // Start is called before the first frame update
    void Start()
    {
        InitBlocks();
    }

    public void ChangeBreakableBlocks(ElementalColor oldColor, ElementalColor newColor)
    {
        if (oldColor.ballMaterial != null && blocksByColor.ContainsKey(oldColor))
            foreach (Block block in blocksByColor[oldColor])
                block.SetBreakable(false);

        if (blocksByColor.ContainsKey(newColor))
            foreach (Block block in blocksByColor[newColor])
                block.SetBreakable(true);
    }

    void InitBlocks()
    {
        blocksByColor = new Dictionary<ElementalColor, List<Block>>();
        foreach (Block block in FindObjectsOfType<Block>())
        {
            if (blocksByColor.ContainsKey(block.color))
                blocksByColor[block.color].Add(block);
            else
                blocksByColor.Add(block.color, new List<Block>() { block });
        }
    }

    public void RemoveBlock(Block block)
    {
        blocksByColor[block.color].Remove(block);
    }
}
