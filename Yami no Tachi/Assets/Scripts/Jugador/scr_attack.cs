using UnityEngine;

public class scr_attack : MonoBehaviour {

    private Animator mi_animator;

    private void OnEnable(){
        mi_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Atacar();
        }
    }

    void Atacar(){
        mi_animator.SetTrigger("attack");
    }

}
