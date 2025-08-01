using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// ������p�X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData")]
    [System.Serializable]
    public class WeaponData : ScriptableObject
    {
        // �ۑ���p�X
        const string PATH = "WeaponData";

        // �A�N�Z�X�p�̃C���X�^���X
        static WeaponData _data;
        public static WeaponData Data
        {
            get
            {
                if (_data == null)
                {
                    // �A�N�Z�X���ꂽ�烊�\�[�X�ɂ���p�X���̃I�u�W�F�N�g��ǂݍ���
                    _data = Resources.Load<WeaponData>(PATH);

                    // �ǂݍ��ݎ��s���̃G���[
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // ���퐔�l���
        public Vector3 GreatSwordSize => _greatSwordSize;
        [Header("�匕�T�C�Y")]
        [SerializeField] Vector3 _greatSwordSize;

        public Vector2 LightAttackHitWindow => _lightAttackHitWindow;
        [Header("���C�g�U�������蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _lightAttackHitWindow;

        public Vector2 HeavyAttackHitWindow => _heavyAttackHitWindow;
        [Header("�w�r�[�U�������蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _heavyAttackHitWindow;

        public Vector2 SpecialAttackHitWindow => _specialAttackHitWindow;
        [Header("�X�y�V�����U�������蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _specialAttackHitWindow;

        public Vector2 ParryHitWindow => _parryHitWindow;
        [Header("�p���B�����蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _parryHitWindow;

        public float FreezeDuration => _freezeDuration;
        [Header("�q�b�g�X�g�b�v�p������")]
        [SerializeField] float _freezeDuration;

        public int InitialPoolSize => _initialPoolSize;
        [Header("�c���p�����v�[���T�C�Y")]
        [SerializeField] int _initialPoolSize;

        public GameObject AfterImagePrefab => _afterImagePrefab;
        [Header("�c���p�v���n�u")]
        [SerializeField] GameObject _afterImagePrefab;

        public float AfterImageInterval => _afterImageInterval;
        [Header("�c�������Ԋu")]
        [SerializeField] float _afterImageInterval;

        public float AfterImageDuration => _afterImageDuration;
        [Header("�c���\������")]
        [SerializeField] float _afterImageDuration;

        public float AfterImageEndRatio => _afterImageEndRatio;
        [Header("�A�j���[�V�����ɂ�����c���\���I������")]
        [SerializeField] float _afterImageEndRatio;
    }
}
