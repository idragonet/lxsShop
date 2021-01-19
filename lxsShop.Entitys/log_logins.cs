using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// log_logins
    /// </summary>
    public class log_logins
    {
        /// <summary>
        /// log_logins
        /// </summary>
        public log_logins()
        {
        }

        private System.Int64 _ID;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 ID { get { return this._ID; } set { this._ID = value; } }

        private System.DateTime? _loginTime;
        /// <summary>
        /// loginTime
        /// </summary>
        public System.DateTime? loginTime { get { return this._loginTime; } set { this._loginTime = value ?? default(System.DateTime); } }

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