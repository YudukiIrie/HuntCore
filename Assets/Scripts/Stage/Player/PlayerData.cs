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
        public Vector3 PlayerSize => _playerSize;
        [Header("�v���C���[�T�C�Y")]
        [SerializeField] Vector3 _playerSize;

        public float DrawnMoveSpeed => _drawnMoveSpeed;
        [Header("������Ԃ̈ړ����x")]
        [SerializeField] float _drawnMoveSpeed;

        public float DrawnRotSpeed => _drawnRotSpeed;
        [Header("������Ԃ̉�]���x")]
        [SerializeField] float _drawnRotSpeed;

        public float HeavyAttackRotSpeed => _heavyAttackRotSpeed;
        [Header("�w�r�[�U���̉�]���x")]
        [SerializeField] float _heavyAttackRotSpeed;

        public float SpecialAttackRotSpeed => _specialAttackRotSpeed;
        [Header("�X�y�V�����U���̉�]���x")]
        [SerializeField] float _specialAttackRotSpeed;

        public float ParryableTime => _parryableTime;
        [Header("�p���B�\�Ȏ���")]
        [SerializeField] float _parryableTime;

        public float MoveSpeedOfParry => _moveSpeedOfParry;
        [Header("�p���B���ړ����x")]
        [SerializeField] float _moveSpeedOfParry;

        public float RotSpeedOfParry => _rotSpeedOfParry;
        [Header("�p���B����]���x")]
        [SerializeField] float _rotSpeedOfParry;

        public float RecoilSpeed => _recoilSpeed;
        [Header("�̂����莞�ړ����x")]
        [SerializeField] float _recoilSpeed;

        public float RecoilDistance => _recoilDistance;
        [Header("�̂����莞�ړ�����")]
        [SerializeField] float _recoilDistance;

        public float ChainTime => _chainTime;
        [Header("�R���{�ԗP�\����")]
        [SerializeField] float _chainTime;

        public float IdleToOtherDuration => _idleToOtherDuration;
        [Header("�ҋ@����J�ډ\�܂ł̎���")]
        [SerializeField] float _idleToOtherDuration;

        public float BlockedToOtherDuration => _blockedToOtherDuration;
        [Header("�K�[�h�ォ��J�ډ\�܂ł̎���")]
        [SerializeField] float _blockedToOtherDuration;

        public float AnimBlendTime => _animBlendTime;
        [Header("�A�j���[�V�����u�����h����")]
        [SerializeField] float _animBlendTime;
    }
}
