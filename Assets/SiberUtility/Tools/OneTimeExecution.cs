namespace SiberUtility.Tools
{
    /// <summary> 幫助在 Update 情況下判斷，只執行一次 </summary>
    public class OneTimeExecution
    {
    #region ========== [Private Variables] ==========

        private bool hasExecuted = false;

    #endregion

    #region ========== [Public Methods] ==========

        public bool ExecuteOnce()
        {
            if (!hasExecuted)
            {
                hasExecuted = true;
                return true; // 返回 true 表示成功執行一次
            }

            return false; // 返回 false 表示已經執行過，不再執行
        }

        public void Reset()
        {
            hasExecuted = false;
        }

    #endregion
    }
}