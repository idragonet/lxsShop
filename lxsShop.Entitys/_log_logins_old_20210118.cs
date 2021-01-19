using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// _log_logins_old_20210118
    /// </summary>
    public class _log_logins_old_20210118
    {
        /// <summary>
        /// _log_logins_old_20210118
        /// </summary>
        public _log_logins_old_20210118()
        {
        }

        private System.Int64 _ID;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 ID { get { return this._ID; } set { this._ID = value; } }

        private System.String _loginTime;
        /// <summary>
        /// loginTime
        /// </summary>
        public System.String loginTime { get { return this._loginTime; } set { this._loginTime = value; } }

        private System.String _loginIp;
        /// <summary>
        /// loginIp
        /// </summary>
        public System.String loginIp { get { return this._loginIp; } set { this._loginIp = value; } }

        private System.String _LoginUser;
        /// <summary>
        /// LoginUser
        /// </summary>
        public System.String LoginUser { get { return this._LoginUser; } set { this._LoginUser = value; } }

        private System.String _PASSWord;
        /// <summary>
        /// PASSWord
        /// </summary>
        public System.String PASSWord { get { return this._PASSWord; } set { this._PASSWord = value; } }
    }
}