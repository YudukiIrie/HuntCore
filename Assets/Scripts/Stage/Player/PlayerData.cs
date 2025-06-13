using UnityEngine;

namespace Stage.Players
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

        public float HeavyAttackRotSpeed => _heavyAttackRotSpeed;
        [Header("�w�r�[�U���̉�]���x")]
        [SerializeField] float _heavyAttackRotSpeed;

        public float SpecialAttackRotSpeed => _specialAttackRotSpeed;
        [Header("�X�y�V�����U���̉�]���x")]
        [SerializeField] float _specialAttackRotSpeed;

        public float MagnitudeBorder => _magnitudeBorder;
        [Header("�����Ə��������ʂ���x�N�g�����̋��E")]
        [SerializeField]float _magnitudeBorder;

        public float ChainTime => _chainTime;
        [Header("�R���{�ԗP�\����")]
        [SerializeField] float _chainTime;

        public float IdleToOtherDuration => _idleToOtherDuration;
        [Header("�ҋ@����J�ډ\�܂ł̎���")]
        [SerializeField] float _idleToOtherDuration;

        public float AnimBlendTime => _animBlendTime;
        [Header("�A�j���[�V�����u�����h����")]
        [SerializeField] float _animBlendTime;
    }
}
