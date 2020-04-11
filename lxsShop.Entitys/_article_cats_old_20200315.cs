using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// _article_cats_old_20200315
    /// </summary>
    public class _article_cats_old_20200315
    {
        /// <summary>
        /// _article_cats_old_20200315
        /// </summary>
        public _article_cats_old_20200315()
        {
        }

        private System.Int64 _catId;
        /// <summary>
        /// catId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 catId { get { return this._catId; } set { this._catId = value; } }

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

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }
    }
}