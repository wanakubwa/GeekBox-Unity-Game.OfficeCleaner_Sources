using System.Collections;
using UnityEngine;

public class ReversibleFurniture : Interactable
{
    #region Fields

    [SerializeField]
    private float targetImpuseForce = 2f;
    [SerializeField]
    private int showEffectProbability = 10;

    #endregion

    #region Propeties



    #endregion

    #region Methods

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerCharacterController>() == null) { return; }

        if(IsInteractable == true && collision.impulse.magnitude > targetImpuseForce)
        {
            IsInteractable = false;

            if(RandomMath.IsSuccessPercent(showEffectProbability) == true)
            {
                GameplayEvents.Instance.NotifyOnFurnitureCollapse();
                Debug.Log("Collapsed!");
            }
        }
    }

    #endregion

    #region Enums



    #endregion
}