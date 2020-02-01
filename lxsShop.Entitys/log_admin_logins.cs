using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// log_admin_logins
    /// </summary>
    public class log_admin_logins
    {
        /// <summary>
        /// log_admin_logins
        /// </summary>
        public log_admin_logins()
        {
        }

        private System.Int64 _loginId;
        /// <summary>
        /// loginId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 loginId { get { return this._loginId; } set { this._loginId = value; } }

        private System.Int64? _adminId;
        /// <summary>
        /// adminId
        /// </summary>
        public System.Int64? adminId { get { return this._adminId; } set { this._adminId = value; } }

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
    }
}