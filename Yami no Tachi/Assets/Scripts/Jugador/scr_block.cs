using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour{

    private Animator mi_animator;

    private void OnEnable()
    {
        mi_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire2")){
            mi_animator.SetBool("isblock", true);
        }else{
            mi_animator.SetBool("isblock", false);
        }
    }

}
