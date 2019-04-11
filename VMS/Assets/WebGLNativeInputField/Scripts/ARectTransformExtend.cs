using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ARectTransformExtend
{

    public static Rect worldRect(this RectTransform main)
    {
        Rect result = new Rect();
        float xScale = main.lossyScale.x;
        float yScale = main.lossyScale.y;
        Vector2 size = main.rect.size;

        result.width = size.x * xScale;
        result.height = size.y * yScale;
        result.x = main.position.x - result.width * main.pivot.x;
        result.y = Screen.height - (main.position.y + result.height * (1 - main.pivot.y));
        return result;
    }
}
