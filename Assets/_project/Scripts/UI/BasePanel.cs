using DG.Tweening;
using UnityEngine;

namespace _project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public virtual void Show()
        {
            canvasGroup.DOFade(1, 0.2f);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            canvasGroup.ignoreParentGroups = true;
        }
        public void Hide()
        {
            canvasGroup.DOFade(0, 0.2f);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            canvasGroup.ignoreParentGroups = false;
        }
    }
}
