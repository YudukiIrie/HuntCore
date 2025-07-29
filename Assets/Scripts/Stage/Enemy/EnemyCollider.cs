using Stage.Enemies;
using Stage.HitDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.enemies
{
    /// <summary>
    /// �G�R���C�_�[�Ǘ��N���X
    /// </summary>
    public class EnemyCollider : MonoBehaviour
    {
        [Header("�GTransform")]
        [SerializeField] Transform _enemyTransform;

        [Header("��Transform")]
        [SerializeField] Transform _headTransform;

        [Header("�E���rTransform")]
        [SerializeField] Transform _rWingTransform;

        [Header("�����rTransform")]
        [SerializeField] Transform _lWingTransform;

        [Header("�E���r�t����Transform")]
        [SerializeField] Transform _rWingRootTransform;

        [Header("�����r�t����Transform")]
        [SerializeField] Transform _lWingRootTransform;

        // �R���C�_�[
        public OBB Enemy {  get; private set; }
        public OBB RWing { get; private set; }
        public OBB LWing { get; private set; }
        public OBB RWingRoot { get; private set; }
        public OBB LWingRoot { get; private set; }
        public HitSphere Head {  get; private set; }

        // �R���C�_�[�ꊇ�Ǘ��pList
        public List<HitCollider> Colliders { get; private set; }

        // ��List�ɑΉ������R���C�_�[�X�V�p�z��
        Transform[] _transforms;

        void Awake()
        {
            int capacity = 10;
            CreateColliders(capacity);
        }

        void Update()
        {
            UpdateColliders();
        }

        /// <summary>
        /// �R���C�_�[�̍쐬
        /// </summary>
        void CreateColliders(int capacity)
        {
            Colliders = new List<HitCollider>(capacity);
            _transforms = new Transform[capacity];

            // �G�R���C�_�[
            Colliders.Add(Enemy = new OBB(
                _enemyTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).Size,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _enemyTransform;

            // �E���r�R���C�_�[
            Colliders.Add(RWing = new OBB(
                _rWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingTransform;

            // �����r�R���C�_�[
            Colliders.Add(LWing = new OBB(
                _lWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingTransform;

            // �E���r�t�����R���C�_�[
            Colliders.Add(RWingRoot = new OBB(
                _rWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingRootTransform;

            // �����r�t�����R���C�_�[
            Colliders.Add(LWingRoot = new OBB(
                _lWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingRootTransform;

            // ���R���C�_�[
            Colliders.Add(Head = new HitSphere(
                _headTransform.position, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).HeadRadius,
                HitCollider.ColliderShape.Sphere, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _headTransform;
        }

        void UpdateColliders()
        {
            for (int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].UpdateInfo(_transforms[i]);
            }
        }
    }
}