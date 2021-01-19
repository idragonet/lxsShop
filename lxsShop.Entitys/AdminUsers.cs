using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// AdminUsers
    /// </summary>
    public class AdminUsers
    {
        /// <summary>
        /// AdminUsers
        /// </summary>
        public AdminUsers()
        {
        }

        private System.Int64 _ID;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 ID { get { return this._ID; } set { this._ID = value; } }

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

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }

        private System.DateTime? _LastLoginTime;
        /// <summary>
        /// LastLoginTime
        /// </summary>
        public System.DateTime? LastLoginTime { get { return this._LastLoginTime; } set { this._LastLoginTime = value ?? default(System.DateTime); } }
    }
}