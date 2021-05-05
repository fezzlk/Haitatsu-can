using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://qiita.com/krlnmattun/items/77fb612f6290429fc023 ���Q�l
public class showWhenIsOutside : MonoBehaviour
{
    [SerializeField] private Camera minimapCamera;              // �~�j�}�b�v�p�J����
    [SerializeField] private Transform iconTarget;              // �A�C�R���ɑΉ�����I�u�W�F�N�g�i���������j
    [SerializeField] private float rangeRadiusOffset = 1.0f;    // �\���͈͂̃I�t�Z�b�g

    // �K�v�ȃR���|�[�l���g
    private SpriteRenderer spriteRenderer;

    private float minimapRangeRadius;   // �~�j�}�b�v�̕\���͈�
    private float defaultPosY;          // �A�C�R���̃f�t�H���gY���W
    const float normalAlpha = 1.0f;     // �͈͓��̃A���t�@�l
    const float outRangeAlpha = 0.5f;   // �͈͊O�̃A���t�@�l

    // Start is called before the first frame update

    private void Start()
    {
        minimapRangeRadius = minimapCamera.orthographicSize;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        DispIcon();
    }

    private bool CheckInsideMap()
    {
        var cameraPos = minimapCamera.transform.position;
        var targetPos = iconTarget.position;

        // ���������Ŕ��肷�邽�߁Ay��0�����ɂ���
        cameraPos.y = targetPos.y = 0;

        return Vector3.Distance(cameraPos, targetPos) <= minimapRangeRadius - rangeRadiusOffset;
    }

    private void DispIcon()
    {
        // �A�C�R����\��������W
        var iconPos = new Vector3(iconTarget.position.x, defaultPosY, iconTarget.position.z);

        // �~�j�}�b�v�͈͓��̏ꍇ�͂��̂܂ܕ\������
        if (CheckInsideMap())
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, normalAlpha);
            transform.position = iconPos;
            return;
        }

        // �A�C�R���𔼓����ɂ���
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, outRangeAlpha);

        // �J�����ƃA�C�R���̈ʒu��������x�N�g�������߂�
        var centerPos = new Vector3(minimapCamera.transform.position.x, defaultPosY, minimapCamera.transform.position.z);
        var offset = iconPos - centerPos;

        // �w�苗���Ő������������x�N�g�������߂ăA�C�R���ʒu��ݒ肷��
        transform.position = centerPos + Vector3.ClampMagnitude(offset, minimapRangeRadius - rangeRadiusOffset);
    }


}
