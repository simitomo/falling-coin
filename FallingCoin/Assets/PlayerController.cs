using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// �}�E�X���������n�_���痣�����n�_�܂ł̍��W�̌v�Z
    private Vector2 AddPos(Vector2 startPos, Vector2 endPos)
    {
        Vector2 temp;
        temp.x = startPos.x + endPos.y;
        temp.y = startPos.y + endPos.y;
        return temp;
    }

    // �}�E�X�����������W������ϐ�
    Vector2 startPos = new Vector2();

    // Rigidbody2D�^�̕ϐ���p��
    Rigidbody2D rigid;

    void Start()
    {
        // rigidbody��������悤�ɋ@�\�������Ă���
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �}�E�X�������ꂽ�n�_�̍��W����
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("U_OK");
            startPos = Input.mousePosition;
        }

        // �}�E�X�������ꂽ���W����
        // �����������ʂ����͂�������
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 addPos = AddPos(startPos, Input.mousePosition);

            this.rigid.AddForce(addPos);
        }
    }

    void FixedUpdate()
    {
        // �n�ʂɂ��Ă��Ȃ��ꍇ�d�͂�ʏ��2�{������
        if (this.rigid.velocity.y != 0)
        {
            Debug.Log("FU_OK");
            this.rigid.AddForce(transform.up * -19.6f);
        }
    }
}
