using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage.Players;
using Stage.Enemies;
using Unity.VisualScripting;

namespace Stage.HitCheck
{
    /// <summary>
    /// �����OBB�ɂ�铖���蔻��
    /// </summary>
    public class SwordHitboxOBB : MonoBehaviour
    {
        [Header("�匕�I�u�W�F�N�g")]
        [SerializeField] GameObject _greatSword;

        [Header("�G�I�u�W�F�N�g")]
        [SerializeField] GameObject _enemy;

        #region �f�o�b�O�p
        [Header("�G��ڐG�}�e���A��")]
        [SerializeField] Material _noHitMaterial;

        [Header("�G�ڐG�}�e���A��")]
        [SerializeField] Material _hitMaterial;

        MeshRenderer _enemyMeshRenderer;
        #endregion

        // �e�I�u�W�F�N�gOBB
        OBB _greatSwordOBB;
        OBB _enemyOBB;
        public OBB GreatSwordOBB => _greatSwordOBB;

        // ����ςݓG�i�[�p
        HashSet<OBB> _hitEnemies = new HashSet<OBB>();

        void Start()
        {
            // �e�I�u�W�F�N�gOBB���̓o�^�Ǝ��̂̍쐬
            _greatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            _enemyOBB = new OBB(_enemy.transform, EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).EnemySize);

            #region �f�o�b�O�p
            _enemyMeshRenderer = _enemy.GetComponent<MeshRenderer>();
            #endregion
        }

        void Update()
        {
            UpdateOBBInfo(_greatSwordOBB, _greatSword);
            UpdateOBBInfo(_enemyOBB, _enemy);
        }

        /// <summary>
        /// OBB���̍X�V
        /// </summary>
        /// <param name="obb">�X�V�Ώۂ�OBB</param>
        /// <param name="go">obb�̌��ƂȂ�I�u�W�F�N�g</param>
        void UpdateOBBInfo(OBB obb, GameObject go)
        {
            // ���W�̍X�V
            obb.UpdateCenter(go.transform.position);
            // �������̍X�V
            obb.UpdateAxes(go.transform);
        }

        /// <summary>
        /// �w�肵������OBB�ƓGOBB�Ƃ̓����蔻��
        /// </summary>
        /// <param name="obb">����OBB</param>
        /// <param name="go">obb�̌��ƂȂ�I�u�W�F�N�g</param>
        /// <returns>true:�ڐG�Afalse:��ڐG</returns>
        public bool IsCollideBoxOBB(OBB weaponOBB)
        {
            // ����ς݂̓G�͖���
            if (_hitEnemies.Contains(_enemyOBB))
                return false;

            // ���S�Ԃ̋����̎擾
            Vector3 distance  = weaponOBB.Center - _enemyOBB.Center;

            // ���؎���p���������̔�r
            // ��������؎��Ƃ����ꍇ
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisX, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisY, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisZ, distance)) return false;
            // �G�����؎��Ƃ����ꍇ
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisX, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisY, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisZ, distance)) return false;

            // ���������m�̊O�ς�p���������̔�r
            Vector3 cross = Vector3.zero;
            // �����X���Ƃ̔�r
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            // �����Y���Ƃ̔�r
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            // �����Z���Ƃ̔�r
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;

            // �ڐG�ς݂̓G�𔻒�List�Ɋi�[
            _hitEnemies.Add(_enemyOBB);

            return true;
        }

        /// <summary>
        /// �w�肵������OBB�ƓGOBB�̋�����r
        /// </summary>
        /// <param name="separating">�w�肵��������</param>
        /// <param name="distance">2�_�Ԃ̋���</param>
        /// <returns></returns>
        bool CompareLengthOBB(OBB weaponOBB, OBB enemyOBB, Vector3 separating, Vector3 distance)
        {
            // ���؎���̕���ƓG�̋���
            // �}�C�i�X�̃x�N�g���ł���\�������邽�ߐ�Βl��
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // ����̌��؎���ɂ����镐��̋����̔��������߂�
            float weaponDist =
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisX, separating)) * weaponOBB.Radius.x +
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisY, separating)) * weaponOBB.Radius.y +
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisZ, separating)) * weaponOBB.Radius.z;

            // ����̌��؎���ɂ�����G�̋����̔��������߂�
            float enemyDist =
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisX, separating)) * enemyOBB.Radius.x +
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisY, separating)) * enemyOBB.Radius.y +
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisZ, separating)) * enemyOBB.Radius.z;

            // ����ƓG�̋����ƁA��L�̋����̍��v���r
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// ����̃��Z�b�g
        /// </summary>
        public void ResetHitEnemies()
        {
            _hitEnemies.Clear();
        }

        #region �f�o�b�O�p
        public void ChangeEnemyColor()
        {
            _enemyMeshRenderer.material = _hitMaterial;
        }

        public void ResetEnemyColor()
        {
            _enemyMeshRenderer.material = _noHitMaterial;
        }
        #endregion
    }
}
