using UnityEngine;

namespace SiberUtility.Tools
{
    /// <summary> 複雜的演算工具
    /// <para> (Vector Random, Rot, Pos, Rigidbody) </para>
    /// </summary>
    public static class AlgorithmHelper
    {
    #region Public Methods

        //方塊 - 範圍內的隨機地點
        public static Vector3 RandomPointInBox(BoxCollider2D boxCollider2D)
        {
            var extents = boxCollider2D.size / 2;
            var point = new Vector3(Random.Range(-extents.x, extents.x),
                                    Random.Range(-extents.y, extents.y), 0f);
            return boxCollider2D.transform.TransformPoint(point);
        }

        public static Vector3 RandomPointInBox(Vector2 newPos, Transform spawner)
        {
            var extents = newPos / 2;
            var point = new Vector3(Random.Range(-extents.x, extents.x),
                                    Random.Range(-extents.y, extents.y), 0f);
            return spawner.transform.TransformPoint(point);
        }

        public static Vector3 RandomPointInCircle(Transform origin, float range = 1f)
        {
            var randomPoint = Random.insideUnitCircle * range;
            var point       = origin.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            return point;
        }

        // 2D 爆炸
        public static void AddExplosionForce
        (this Rigidbody2D rb,              float explosionForce, Vector2 explosionPosition,
         float            explosionRadius, float upwardsModifier = 0.0F,
         ForceMode2D      mode = ForceMode2D.Force)
        {
            var explosionDir      = rb.position - explosionPosition;
            var explosionDistance = explosionDir.magnitude;

            // Normalize without computing magnitude again
            if (upwardsModifier == 0)
                explosionDir /= explosionDistance;
            else
            {
                // From Rigidbody.AddExplosionForce doc:
                // If you pass a non-zero value for the upwardsModifier parameter, the direction
                // will be modified by subtracting that value from the Y component of the centre point.
                explosionDir.y += upwardsModifier;
                explosionDir.Normalize();
            }

            rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
        }

        public static bool RandomIntResult(int expectResult)
        {
            //ex: 15%機率打中, 計算比15小就等於 true
            var value  = Mathf.Clamp(expectResult, 0, 100);
            var random = Random.Range(1, 101);
            return random <= value;
        }

        // 單純更改顏色的Alpha
        public static void ChangeColorAlpha(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
                var newColor = spriteRenderer.color;
                newColor.a           = 1f;
                spriteRenderer.color = newColor;
            }
        }

    #region 2D - 隨著中心旋轉並改變位置

        // 2D - 隨著中心旋轉
        public static Vector3 RotateAroundCenter(float angle, float radius, Transform center)
        {
            if (center == null) return Vector2.zero;

            var posX = center.position.x + (Mathf.Cos(angle) * radius);
            var posY = center.position.y + (Mathf.Sin(angle) * radius);

            return new Vector2(posX, posY);
        }

        public static Vector3 RotateAroundCenter(float angle, float radiusA, float radiusB, Transform center)
        {
            if (center == null) return Vector2.zero;

            var posX = center.position.x + (Mathf.Cos(angle) * radiusA);
            var posY = center.position.y + (Mathf.Sin(angle) * radiusB);

            return new Vector2(posX, posY);
        }

        public static float PingPong(float min, float max)
        {
            return Mathf.Lerp(min, max, Mathf.PingPong(Time.time, 1));
        }

        public static float PingPong(float min, float max, float time, float length)
        {
            return Mathf.Lerp(min, max, Mathf.PingPong(time, length));
        }

        public static bool IsBetweenRange(float thisValue, float value1, float value2)
        {
            return thisValue >= Mathf.Min(value1, value2) &&
                   thisValue <= Mathf.Max(value1, value2);
        }

    #endregion


    #region 2D - 面向中心

        /// <summary> 獲得 2D面向目標的 Quaternion </summary>
        /// <param name="target"> A:目標 </param>
        /// <param name="main"> B:自己 </param>
        /// <param name="correctionAngle">修正角度，如果物體角度不正確，可自行修正</param>
        /// <example> B 面向-> A </example>>
        public static Quaternion FacingTarget(Transform target, Transform main, float correctionAngle = 0f)
        {
            var angle = FacingTargetAngle(target.position, main.position);
            return Quaternion.AngleAxis(angle + correctionAngle, Vector3.forward);
        }

        /// <summary> 獲得 2D面向目標的 Quaternion </summary>
        /// <param name="targetPos"> A:目標位置 </param>
        /// <param name="mainPos"> B:自己位置 </param>
        /// <param name="correctionAngle">修正角度，如果物體角度不正確，可自行修正</param>
        /// /// <example> B 面向-> A </example>>
        public static Quaternion FacingTarget(Vector3 targetPos, Vector3 mainPos, float correctionAngle = 0f)
        {
            var angle = FacingTargetAngle(targetPos, mainPos);
            return Quaternion.AngleAxis(angle + correctionAngle, Vector3.forward);
        }

        /// <summary> 獲得 2D面向向量的 Quaternion </summary>
        /// <param name="v"> A:垂直向量 </param>
        /// <param name="h"> B:水平向量 </param>
        /// <param name="correctionAngle">修正角度，如果物體角度不正確，可自行修正</param>
        public static Quaternion FacingTarget(float v, float h, float correctionAngle = 0f)
        {
            var angle = FacingDirectionAngle(v, h);
            return Quaternion.AngleAxis(angle + correctionAngle, Vector3.forward);
        }

        /// <summary> 獲得 2D面向目標的角度 (Angle) </summary>
        /// <param name="targetPos"> A:目標位置 </param>
        /// <param name="mainPos"> B:自己位置 </param>
        /// /// <example> B 面向 -> A </example>> 
        public static float FacingTargetAngle(Vector3 targetPos, Vector3 mainPos)
        {
            var dir   = targetPos - mainPos;
            var angle = FacingDirectionAngle(dir.y, dir.x);
            return angle;
        }

        /// <summary> 獲得 2D向量角度 (Angle) </summary>
        public static float FacingDirectionAngle(float y, float x)
        {
            var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            return angle;
        }

        /// <summary>
        /// 建議用於For loop - 使物體位置能因半徑平均分布於圓周上。
        /// </summary>
        /// <param name="count">總數</param>
        /// <param name="i">當前對象的數字</param>
        /// <param name="radius">偏移半徑</param>
        public static Vector3 SetPosFromCenter(int i, int count, float radius)
        {
            float theta = (2 * Mathf.PI / count) * i;
            float x     = radius * Mathf.Sin(theta);
            float y     = radius * Mathf.Cos(theta);
            return new Vector3(x, -y, 0);
        }

    #endregion

    #endregion
    }
}