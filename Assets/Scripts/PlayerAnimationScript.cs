using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    float time;     //����
    float timeTmp;  //���Ԃ̈ꎞ�I�ȋL��
    PlayerMoveScript playerMoveScript;  //�����̃X�N���v�g
    int moveNum;    //����ԍ�
    int pmoveNum;   //�O�̓���ԍ�
    /*
     * 0:��
     * 1:���s
     * 2:�W�����v�ő�
     * 3:���R����
     */
    int lr;         //���E
    int plr;        //�O�̍��E
    /*
     * 0 < lr : �E
     * lr < 0 : ��
     */

    public GameObject bodyObj; //�g��obj
    public GameObject legRObj; //�E��obj
    public GameObject legLObj; //����obj
    public GameObject eyeObj;  //��obj
    public GameObject finObj;  //�hobj

    SpriteRenderer bodySprRend; //�g��spriteRenderer
    SpriteRenderer legRSprRend; //�E��spriteRenderer
    SpriteRenderer legLSprRend; //����spriteRenderer
    SpriteRenderer eyeSprRend;  //��spriteRenderer
    SpriteRenderer finSprRend;  //�hspriteRenderer

    public Sprite[] bodySprite; //�g��sprite�z��
    /*
     * 0:�ʏ�
     */
    public Sprite[] legSprite;  //��sprite�z��
    /*
     * 0:���s1
     * 1:���s2
     * 2:���s3, �ʏ�E
     * 3:���s4, ���R�����E
     * 4:���s5, �ʏ퍶
     * 5:���s6, �W�����v��, ���R������
     * 6:�W�����v�E
     */
    public Sprite[] eyeSprite;  //��sprite�z��
    /*
     * 0:�ʏ�
     * 1:�_���[�W
     * 2:���C
     */
    public Sprite[] finSprite;  //�hSprite�z��
    /*
     * 0:����, �ʏ�
     * 1:�E��
     * 2:�E��
     * 3:����
     */

    public float moveInterval;
    

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timeTmp = 0;
        moveNum = 0;
        pmoveNum = 0;
        lr = 1;
        plr = 1;
        playerMoveScript = this.GetComponent<PlayerMoveScript>();   //����X�N���v�g�̎擾

        bodySprRend = bodyObj.GetComponent<SpriteRenderer>();
        legRSprRend = legRObj.GetComponent<SpriteRenderer>();
        legLSprRend = legLObj.GetComponent<SpriteRenderer>();
        eyeSprRend = eyeObj.GetComponent<SpriteRenderer>();
        finSprRend= finObj.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveScript.anmStop) return;

        time += Time.deltaTime;
        moveNum = playerMoveScript.moveNum;
        lr = playerMoveScript.lr;

        //����ԍ�0:��
        if (moveNum == 0)
        {
            //�O�t���[����0�łȂ���ΕύX
            if (pmoveNum != 0)
            {
                bodySprRend.sprite = bodySprite[0];
                legRSprRend.sprite = legSprite[2];
                legLSprRend.sprite = legSprite[4];
                eyeSprRend.sprite = eyeSprite[0];
                finSprRend.sprite= finSprite[0];
            }
        }
        //����ԍ�1:���s
        if (moveNum == 1)
        {
            //�O�t���[�����Ⴄ�Ȃ�J�n���Ԃ̕ۑ�
            if (pmoveNum != 1) { timeTmp = time; }
            //�v�f�Ԗڂ̌v�Z
            int idxR = (int)(((time - timeTmp) / moveInterval) % 6);
            int idxL = (int)(((time - timeTmp) / moveInterval + 3) % 6);
            legRSprRend.sprite = legSprite[idxR];
            legLSprRend.sprite = legSprite[idxL];
        }
        //����ԍ�2:�W�����v
        if (moveNum == 2)
        {
            legRSprRend.sprite = legSprite[6];
            legLSprRend.sprite = legSprite[5];
        }
        //����ԍ�3:���R����
        if (moveNum == 3)
        {
            legRSprRend.sprite = legSprite[3];
            legLSprRend.sprite = legSprite[5];
        }

        


        //�E����
        if (0 < lr)
        {
            //�O���������Ȃ�
            if (plr <= 0)
            {
                Vector3 r = new Vector3(1f, 1f, 1f);
                bodyObj.transform.localScale = r;
                legRObj.transform.localScale = r;
                legLObj.transform.localScale = r;
                eyeObj.transform.localScale = r;
                finObj.transform.localScale = r;
            }
        }
        //������
        if (0 > lr)
        {
            //�O���E�����Ȃ�
            if (plr >= 0)
            {
                Vector3 r = new Vector3(-1f, 1f, 1f);
                bodyObj.transform.localScale = r;
                legRObj.transform.localScale = r;
                legLObj.transform.localScale = r;
                eyeObj.transform.localScale = r;
                finObj.transform.localScale = r;
            }
        }

        //�O�̍X�V
        pmoveNum = moveNum;
        plr = lr;
    }
}
