using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
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

        public float Attack1HitStartRatio => _attack1HitStartRatio;
        [Header("�匕�U��1�̓����蔻��J�n����")]
        [SerializeField] float _attack1HitStartRatio;

        public float Attack2HitStartRatio => _attack2HitStartRatio;
        [Header("�匕�U��2�̓����蔻��J�n����")]
        [SerializeField] float _attack2HitStartRatio;

        public float Attack3HitStartRatio => _attack3HitStartRatio;
        [Header("�匕�U��3�̓����蔻��J�n����")]
        [SerializeField] float _attack3HitStartRatio;
    }
}
