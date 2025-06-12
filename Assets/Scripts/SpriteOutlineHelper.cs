using UnityEngine;

public static class SpriteOutlineHelper
{
    // 靜態方法：為指定 GameObject 添加外框
    public static void AddOutline(GameObject target, Color outlineColor, float outlineSize)
    {
        if (target == null)
        {
            Debug.LogWarning("Target GameObject is null!");
            return;
        }

        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("Target has no SpriteRenderer component!");
            return;
        }

        GameObject outline = new GameObject("Outline");
        outline.transform.parent = target.transform;
        outline.transform.localScale = Vector3.one * (1 + outlineSize);
        outline.transform.localPosition = Vector3.zero;

        SpriteRenderer outlineRenderer = outline.AddComponent<SpriteRenderer>();
        outlineRenderer.sprite = spriteRenderer.sprite;
        outlineRenderer.color = outlineColor;
        outlineRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }

    // 靜態方法：移除所有外框（根據命名規則）
    public static void RemoveAllOutlines(GameObject target)
    {
        if (target == null) return;

        // 找到所有名為 "Outline" 的子物件並刪除
        foreach (Transform child in target.transform)
        {
            if (child.name == "Outline")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}