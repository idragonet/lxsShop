using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// _goods_cats_old_20200313
    /// </summary>
    public class _goods_cats_old_20200313
    {
        /// <summary>
        /// _goods_cats_old_20200313
        /// </summary>
        public _goods_cats_old_20200313()
        {
        }

        private System.Int64 _catId;
        /// <summary>
        /// catId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 catId { get { return this._catId; } set { this._catId = value; } }

        private System.Int64? _parentId;
        /// <summary>
        /// parentId
        /// </summary>
        public System.Int64? parentId { get { return this._parentId; } set { this._parentId = value ?? default(System.Int64); } }

        private System.String _catName;
        /// <summary>
        /// catName
        /// </summary>
        public System.String catName { get { return this._catName; } set { this._catName = value; } }

        private System.Int64? _isShow;
        /// <summary>
        /// isShow
        /// </summary>
        public System.Int64? isShow { get { return this._isShow; } set { this._isShow = value ?? default(System.Int64); } }

        private System.Int64? _catSort;
        /// <summary>
        /// catSort
        /// </summary>
        public System.Int64? catSort { get { return this._catSort; } set { this._catSort = value ?? default(System.Int64); } }

        private System.Int64? _dataFlag;
        /// <summary>
        /// dataFlag
        /// </summary>
        public System.Int64? dataFlag { get { return this._dataFlag; } set { this._dataFlag = value ?? default(System.Int64); } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }
    }
}