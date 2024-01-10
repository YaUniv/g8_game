using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    Vector2 pos;    //�ʒu
    Rigidbody2D rb; //RigidBody2D
    Vector2 vel;    //���x
    float time;     //����

    bool canMove;   //�����邩

    bool isGround;  //�n�ʂɗ����Ă邩

    public float accel;         //�����x
    public float deccel;        //�����x
    public float maxMoveSpeed;  //�ō��ړ����x

    float jumpStep;             //�W�����v�i�K
    public float jumpPower;     //�W�����v��
    public float maxJumpTime;   //�ō��W�����v�͎���
    float jumpTime;

    bool leftMoveKey;   //���ړ��L�[
    bool rightMoveKey;  //�E�ړ��L�[
    bool jumpKey;       //�W�����v�L�[
    bool beamKey;       //�r�[���L�[
    bool finKey;        //�h�U���L�[

    public int moveNum; //����ԍ�
    /*
     * 0:��
     * 1:���s
     * 2:�W�����v�ő�
     * 3:���R����
     */
    public int lr;      //���E
    /*
     * 0 < lr : �E
     * lr < 0 : ��
     */

    //�r�[���p
    public GameObject beamObj;
    public float beamTime;
    public float beamCoolTime;
    bool beamShooting;
    public Transform muzzleR;
    public Transform muzzleL;
    //�h�U���p
    public GameObject finAttackObj;
    public float finAttackTime;
    public float finAttackCoolTime;
    bool finAttacking;

    //����
    public GameObject ExplodeObj;

    //���ʍ���
    public float deathY = -3;

    //�A�j���[�V������~
    public bool anmStop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        canMove = true;
        moveNum = 0;
        lr = 1;

        beamShooting = false;
        finAttacking = false;

        anmStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�ʒu�Ƒ��x���擾
        pos = this.transform.position;
        vel = rb.velocity;
        //���Ԃ�i�߂�
        time += Time.deltaTime;

        if (GameManager.instance.goal)
        {
            moveNum = 1;
            float speed = 2f;
            if (vel.x > speed)
            {
                vel.x -= accel * Time.deltaTime;
                if (vel.x < speed) { vel.x = speed; }
            }
            if (vel.x < speed)
            {
                vel.x += accel * Time.deltaTime;
                if (vel.x > speed) { vel.x = speed; }
            }

            rb.velocity = vel;

            if (pos.x > 100 && !GameManager.instance.maskFade)
            {
                GameManager.instance.MaskFade();
            }

            return;
        }

        if (GameManager.instance.bossCatCome)
        {
            rb.velocity = Vector2.zero;

            float time2 = GameManager.instance.time2;
            if (time2 < 1)
            {
                anmStop = true;
            }
            else
            {
                anmStop = false;
                lr = -1;
                moveNum = 0;
            }
            return;
        }

        if (GameManager.instance.bossClear)
        {
            canMove = false;
        }

        if (canMove)
        {
            //�L�[����
            leftMoveKey = Input.GetKey(KeyCode.LeftArrow);
            rightMoveKey = Input.GetKey(KeyCode.RightArrow);
            jumpKey = Input.GetKey(KeyCode.UpArrow);
            beamKey = Input.GetKeyDown(KeyCode.Space);
            finKey = Input.GetKeyDown(KeyCode.C);
        }
        else
        {
            leftMoveKey = false;
            rightMoveKey = false;
            jumpKey = false;
            beamKey = false;
            finKey = false;
        }


        //���E�ړ�
        {
            //�E�������ꂽ�Ȃ�
            if (rightMoveKey)
            {
                //����
                vel.x += accel * Time.deltaTime;
                //�ō����x�ȉ���
                if (vel.x > maxMoveSpeed) { vel.x = maxMoveSpeed; }

                //�W�����v���łȂ���Γ���ԍ��̕ύX
                if (jumpStep == 0) { moveNum = 1; }
                if (!beamShooting && !finAttacking) lr = 1;     //���E�ύX
            }
            else
            {
                //�E�ړ����Ȃ�
                if (vel.x > 0)
                {
                    //����
                    vel.x -= deccel * Time.deltaTime;
                    if (vel.x < 0) { vel.x = 0; }
                }
            }
            //���������ꂽ�Ȃ�
            if (leftMoveKey)
            {
                //����
                vel.x -= accel * Time.deltaTime;
                //�ō����x�ȉ���
                if (vel.x < -maxMoveSpeed) { vel.x = -maxMoveSpeed; }

                //�W�����v���łȂ���Γ���ԍ��̕ύX
                if (jumpStep == 0) { moveNum = 1; }
                if (!beamShooting && !finAttacking) lr = -1;    //���E�ύX
            }
            else
            {
                //���ړ����Ȃ�
                if (vel.x < 0)
                {
                    //����
                    vel.x += deccel * Time.deltaTime;
                    if (vel.x > 0) { vel.x = 0; }
                }
            }
            //�ǂ����������ĂȂ��Ȃ�
            if (!rightMoveKey && !leftMoveKey)
            {
                //�W�����v���łȂ���Γ���ԍ��̕ύX
                if (jumpStep == 0) { moveNum = 0; }
            }
        }

        //�W�����v
        {
            //�W�����v�i�K0�F�W�����v�ł�����
            if (jumpStep == 0)
            {
                //�n�ʂɐG��Ă��ăW�����v�L�[�������ꂽ�Ȃ�
                if (jumpKey && isGround)
                {
                    jumpStep = 1;       //�i�K�ڍs
                    vel.y = jumpPower;  //�W�����v
                    jumpTime = time;    //�W�����v�J�n����

                    //����ԍ��̕ύX
                    moveNum = 2;

                    AudioSource.PlayClipAtPoint(SEManager.sounds[0], Vector3.zero, SEManager.volume[0]);
                }
                //�n�ʂɐG��Ă��Ȃ��Ȃ�
                if (!isGround)
                {
                    //����ԍ��̕ύX
                    moveNum = 3;
                }
            }
            //�W�����v�i�K1�F�������W�����v��
            else if (jumpStep == 1)
            {
                vel.y = jumpPower;  //�ō��W�����v�͂��ێ�
                                    //�W�����v�L�[�������ꂽ�ł͂Ȃ��܂��͍ō��W�����v�͎��Ԃ��߂�����
                if (!jumpKey || time > jumpTime + maxJumpTime)
                {
                    jumpStep = 2;       //�i�K�ڍs
                }
            }
            //�W�����v�i�K2�F���R����
            else if (jumpStep == 2)
            {
                //�n�ʂɐG�ꂽ��W�����v�ł���悤��
                if (isGround) { jumpStep = 0; }
            }
        }

        //���x�K��
        rb.velocity = vel;


        //�r�[��
        if (!beamShooting && beamKey)
        {
            StartCoroutine(beamShoot());
        }
        //�h�U��
        if (!finAttacking && finKey)
        {
            StartCoroutine(finAttack());
        }

        if (pos.y < deathY)
        {
            PlayerDestroy();
        }

    }

    public void setIsGround(bool b)
    {
        isGround = b;
    }

    IEnumerator beamShoot()
    {
        beamShooting = true;

        GameObject newBeam = Instantiate(beamObj, muzzleR.position, Quaternion.identity);
        if (lr > 0) newBeam.GetComponent<PlayerBeamScript>().playerObj = muzzleR;
        if (lr < 0) newBeam.GetComponent<PlayerBeamScript>().playerObj = muzzleL;
        newBeam.GetComponent<PlayerBeamScript>().beamTime = this.beamTime;
        newBeam.GetComponent<PlayerBeamScript>().lr = lr;

        yield return new WaitForSeconds(beamCoolTime);

        beamShooting = false;
    }

    IEnumerator finAttack()
    {
        finAttacking = true;

        GameObject newFin = Instantiate(finAttackObj, transform.position, Quaternion.identity);
        newFin.GetComponent<PlayerFinAttackScript>().playerObj = this.transform;
        newFin.GetComponent<PlayerFinAttackScript>().finAttackTime = this.finAttackTime;
        newFin.GetComponent<PlayerFinAttackScript>().lr = lr;

        yield return new WaitForSeconds(finAttackCoolTime);

        finAttacking = false;
    }

    public void EnemyStep()
    {
        jumpStep = 1;       //�i�K�ڍs
        vel.y = jumpPower;  //�W�����v
        jumpTime = time;    //�W�����v�J�n����

        //����ԍ��̕ύX
        moveNum = 2;

        AudioSource.PlayClipAtPoint(SEManager.sounds[0], Vector3.zero, SEManager.volume[0]);
    }

    public void PlayerDestroy()
    {
        GameManager.instance.GameOver();
        Destroy(gameObject);
        Instantiate(ExplodeObj, transform.position, Quaternion.identity);
    }

    public void ReMove()
    {
        canMove = true;
    }
}
