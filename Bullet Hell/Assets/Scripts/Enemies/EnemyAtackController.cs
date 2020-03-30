using UnityEngine;

public class EnemyAtackController : MonoBehaviour
{
    [SerializeField]
    private AIEnemyAtack AtackType = null;

    private EnemyAtack atack;
    [Header ("RepathSpeeds")]
    [SerializeField]
    private float meleeSpeed = 0.1f;
    [SerializeField]
    private float rangeSpeed = 0.75f;
    private void OnEnable ()
    {
        AtackType.OnChangeAtack.AddListener (ChangeOfAtack);
    }
    private void OnDisable ()
    {
        AtackType.OnChangeAtack.RemoveListener (ChangeOfAtack);
    }
    private void Start ()
    {
        atack = GetComponentInChildren<EnemyAtack> ();
        ChangeOfAtack ();
    }
    public void ChangeTheAtackType (Attack attack)
    {
        AtackType.TypeOfAtack = attack;
    }
    public void ChangeOfAtack ()
    {
        atack.StopAllCoroutines ();
        switch (AtackType.TypeOfAtack)
        {

            case Attack.Ranged:
                //range attack
                atack.ChangeRepath (rangeSpeed);
                atack.StartCoroutine (atack.CheckDamageProxi ());//player takes damage if it is too close

                atack.StartCoroutine (atack.AtackRange ());//shoots the player from distance
                break;
            case Attack.Melee:
                atack.ChangeRepath (meleeSpeed);
                atack.StartCoroutine (atack.CheckDamageProxi ());//player takes damage if it is too close
                break;
            case Attack.Stay:
                Debug.Log ("atack Nothing");
                break;

        }
    }
}
