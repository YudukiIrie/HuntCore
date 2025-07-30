using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオブジェクト用ヒット情報クラス
/// </summary>
public class HitInfo : MonoBehaviour
{
    // 被攻撃の有無
    public bool WasHit { get; private set; } = false;

    /// <summary>
    /// ヒット情報の受け取り
    /// </summary>
    public virtual void ReceiveHit() { WasHit = true; }

    /// <summary>
    /// ヒット情報のリセット
    /// </summary>
    public virtual void ResetHitReceived() {  WasHit = false; }
}
