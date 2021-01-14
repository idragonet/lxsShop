using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// _sys_configs_old_20210114
    /// </summary>
    public class _sys_configs_old_20210114
    {
        /// <summary>
        /// _sys_configs_old_20210114
        /// </summary>
        public _sys_configs_old_20210114()
        {
        }

        private System.Int64 _configId;
        /// <summary>
        /// configId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 configId { get { return this._configId; } set { this._configId = value; } }

        private System.String _fieldName;
        /// <summary>
        /// fieldName
        /// </summary>
        public System.String fieldName { get { return this._fieldName; } set { this._fieldName = value; } }

        private System.String _fieldCode;
        /// <summary>
        /// fieldCode
        /// </summary>
        public System.String fieldCode { get { return this._fieldCode; } set { this._fieldCode = value; } }

        private System.String _fieldValue;
        /// <summary>
        /// fieldValue
        /// </summary>
        public System.String fieldValue { get { return this._fieldValue; } set { this._fieldValue = value; } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }
    }
}