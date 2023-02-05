using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleUIController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float waitTimeS = 1.5f;
    [SerializeField]
    private Image toolImg;

    #endregion

    #region Propeties

    private Coroutine WaitRoutineHandler { get; set; } = null;

    #endregion

    #region Methods

    public void ShowTool(Sprite icon)
    {
        toolImg.sprite = icon;

        if(WaitRoutineHandler != null)
        {
            StopCoroutine(WaitRoutineHandler);
        }

        gameObject.SetActive(true);
        StartCoroutine(_WaitAndHide());
    }

    #endregion

    #region Enums

    private IEnumerator _WaitAndHide()
    {
        yield return new WaitForSeconds(waitTimeS);
        gameObject.SetActive(false);
    }

    #endregion
}
