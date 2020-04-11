using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// ADMIN
    /// </summary>
    public class ADMIN
    {
        /// <summary>
        /// ADMIN
        /// </summary>
        public ADMIN()
        {
        }

        private System.Int64 _adminId;
        /// <summary>
        /// adminId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 adminId { get { return this._adminId; } set { this._adminId = value; } }

        private System.String _loginName;
        /// <summary>
        /// loginName
        /// </summary>
        public System.String loginName { get { return this._loginName; } set { this._loginName = value; } }

        private System.String _loginPwd;
        /// <summary>
        /// loginPwd
        /// </summary>
        public System.String loginPwd { get { return this._loginPwd; } set { this._loginPwd = value; } }

        private System.String _staffName;
        /// <summary>
        /// staffName
        /// </summary>
        public System.String staffName { get { return this._staffName; } set { this._staffName = value; } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }

        private System.DateTime? _lastTime;
        /// <summary>
        /// lastTime
        /// </summary>
        public System.DateTime? lastTime { get { return this._lastTime; } set { this._lastTime = value ?? default(System.DateTime); } }
    }
}