using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage.Player;
using Stage.Enemy;

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

        // �e�I�u�W�F�N�gOBB
        OBB _greatSwordOBB;
        OBB _enemyOBB;

        void Start()
        {
            // �e�I�u�W�F�N�gOBB���̓o�^�Ǝ��̂̍쐬
            _greatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            _enemyOBB = new OBB(_enemy.transform, EnemyData.Data.EnemySize);
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
        public bool IsCollideBoxOBB(OBB obb, GameObject go)
        {


            return true;
        }
    }
}
