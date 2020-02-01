using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// ads
    /// </summary>
    public class ads
    {
        /// <summary>
        /// ads
        /// </summary>
        public ads()
        {
        }

        private System.Int64 _adId;
        /// <summary>
        /// adId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 adId { get { return this._adId; } set { this._adId = value; } }

        private System.String _adName;
        /// <summary>
        /// adName
        /// </summary>
        public System.String adName { get { return this._adName; } set { this._adName = value; } }

        private System.String _image;
        /// <summary>
        /// image
        /// </summary>
        public System.String image { get { return this._image; } set { this._image = value; } }

        private System.String _LINK;
        /// <summary>
        /// LINK
        /// </summary>
        public System.String LINK { get { return this._LINK; } set { this._LINK = value; } }

        private System.Int64? _SORT;
        /// <summary>
        /// SORT
        /// </summary>
        public System.Int64? SORT { get { return this._SORT; } set { this._SORT = value; } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value; } }
    }
}