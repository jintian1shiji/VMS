using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class WebGLNativeInputField : UnityEngine.UI.InputField
{

#if UNITY_WEBGL && !UNITY_EDITOR 

    public override void OnSelect(BaseEventData eventData)
    {
        Rect rect = this.GetComponent<RectTransform>().worldRect();
        int size = (int)(this.transform.lossyScale.x * this.textComponent.fontSize);
        WebNativeDialog.SetUpOverlayDialog(this.text, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, size);
        StartCoroutine(this.OverlayHtmlCoroutine());
    }

    private IEnumerator DelayInputDeactive()
    {
        yield return new WaitForEndOfFrame();
        this.DeactivateInputField();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private IEnumerator OverlayHtmlCoroutine()
    {
        yield return new WaitForEndOfFrame();
        this.DeactivateInputField();
        EventSystem.current.SetSelectedGameObject(null);
        WebGLInput.captureAllKeyboardInput = false;
        while (WebNativeDialog.IsOverlayDialogActive())
        {
            yield return null;
        }
        WebGLInput.captureAllKeyboardInput = true;

        if (!WebNativeDialog.IsOverlayDialogCanceled())
        {
            this.text = WebNativeDialog.GetOverlayDialogValue();
        }
    }

#endif
}
