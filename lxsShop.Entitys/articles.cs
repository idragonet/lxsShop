using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// articles
    /// </summary>
    public class articles
    {
        /// <summary>
        /// articles
        /// </summary>
        public articles()
        {
        }

        private System.Int64 _articleId;
        /// <summary>
        /// articleId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 articleId { get { return this._articleId; } set { this._articleId = value; } }

        private System.String _articleTitle;
        /// <summary>
        /// articleTitle
        /// </summary>
        public System.String articleTitle { get { return this._articleTitle; } set { this._articleTitle = value; } }

        private System.String _articleContent;
        /// <summary>
        /// articleContent
        /// </summary>
        public System.String articleContent { get { return this._articleContent; } set { this._articleContent = value; } }

        private System.Int64? _isShow;
        /// <summary>
        /// isShow
        /// </summary>
        public System.Int64? isShow { get { return this._isShow; } set { this._isShow = value ?? default(System.Int64); } }

        private System.Int64? _dataFlag;
        /// <summary>
        /// dataFlag
        /// </summary>
        public System.Int64? dataFlag { get { return this._dataFlag; } set { this._dataFlag = value ?? default(System.Int64); } }

        private System.DateTime _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime CreateDate { get { return this._CreateDate; } set { this._CreateDate = value; } }

        private System.Int64? _catId;
        /// <summary>
        /// catId
        /// </summary>
        public System.Int64? catId { get { return this._catId; } set { this._catId = value ?? default(System.Int64); } }

        private System.String _CreatorUser;
        /// <summary>
        /// CreatorUser
        /// </summary>
        public System.String CreatorUser { get { return this._CreatorUser; } set { this._CreatorUser = value; } }
    }
}