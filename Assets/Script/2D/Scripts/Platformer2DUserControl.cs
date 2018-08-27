using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private float CurrentX;
        private float PerviousX;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }
        private void Start()
        {
        }

        private void OnEnable()
        {
            StartCoroutine(JumpCheck());

        }

        private void OnDisable()
        {


        }

        private void Update()
        {

        if (this.transform.position.x > 12) {
            MoveToPool();

        }
            //if (!m_Jump)
            //{
            //    // Read the jump input in Update so button presses aren't missed.
            //    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            //}
        }

    private void MoveToPool() {
        this.gameObject.SetActive(false);

        ObjectPool pool = FindObjectOfType<ObjectPool>();

        this.transform.SetParent(pool.pplTrans);

        this.transform.localPosition = Vector3.zero;

        pool.PPLSPOOL.Enqueue(this);

    }


        IEnumerator JumpCheck() {
            
           yield return StartCoroutine(check());

            StartCoroutine(JumpCheck());
        }

        IEnumerator check() {
            CurrentX = this.transform.position.x;

            m_Jump = DistanceCheck();
     
            yield return new WaitForSeconds(.5f);

            PerviousX = CurrentX;
        }

        bool DistanceCheck() {
            float value = Mathf.Abs(Mathf.Abs(CurrentX) - Mathf.Abs(PerviousX));

            if (value < .01f)
            {
                return true;
            }
            else {
                return false;
            }

        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(1, crouch, m_Jump);
            m_Jump = false;
        }
    }