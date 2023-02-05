using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PaperGarbage : GarbageBase
{
    #region Fields

    [SerializeField]
    private float speed = 10f;

    #endregion

    #region Propeties

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if(CanInteract() == true)
        {
            StartDestroyAnimation();
        }
    }

    private void StartDestroyAnimation()
    {
        GameObject toolObject = Player.AnimatorController.CurrentSpawnedTool;
        StartCoroutine(_FlyToTargetAndDestroy(toolObject.transform));
    }

    private IEnumerator _FlyToTargetAndDestroy(Transform target)
    {
        while (true)
        {
            Vector3 move = target.position - transform.position;
            Vector3 step = move.normalized * speed * Time.deltaTime;
            transform.position = transform.position += step;

            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                break;
            }

            yield return null;
        }

        DestroyGarbage();
    }

    #endregion

    #region Enums



    #endregion
}
