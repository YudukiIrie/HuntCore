using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// �v���C���[���p�X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData",menuName = "ScriptableObject/PlayerData")]
    [System.Serializable]
    public class PlayerData : ScriptableObject
    {
        // �ۑ���p�X
        const string PATH = "PlayerData";

        // �A�N�Z�X�p�̃C���X�^���X
        static PlayerData _data;
        public static PlayerData Data
        {
            get
            {
                if (_data == null)
                {
                    // �A�N�Z�X���ꂽ�烊�\�[�X�ɂ���p�X���̃I�u�W�F�N�g��ǂݍ���
                    _data = Resources.Load<PlayerData>(PATH);

                    // �ǂݍ��ݎ��s���̃G���[
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // �v���C���[���l���
        public float WalkSpeed => _walkSpeed;
        [Header("�������x")]
        [SerializeField] float _walkSpeed;

        public float JogSpeed => _jogSpeed;
        [Header("�����葬�x")]
        [SerializeField] float _jogSpeed;

        public float RunSpeed => _runSpeed;
        [Header("���葬�x")]
        [SerializeField] float _runSpeed;

        public float DrawnMoveSpeed => _drawnMoveSpeed;
        [Header("������Ԃ̈ړ����x")]
        [SerializeField] float _drawnMoveSpeed;

        public float RotSpeed => _rotSpeed;
        [Header("��]���x")]
        [SerializeField] float _rotSpeed;

        public float DrawnRotSpeed => _drawnRotSpeed;
        [Header("������Ԃ̉�]���x")]
        [SerializeField] float _drawnRotSpeed;

        public float Attack2RotSpeed => _attack2RotSpeed;
        [Header("�U��2�̉�]���x")]
        [SerializeField] float _attack2RotSpeed;

        public float MagnitudeBorder => _magnitudeBorder;
        [Header("�����Ə��������ʂ���x�N�g�����̋��E")]
        [SerializeField]float _magnitudeBorder;

        public float ChainTime => _chainTime;
        [Header("�R���{�ԗP�\����")]
        [SerializeField] float _chainTime;
    }
}
