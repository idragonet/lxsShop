using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// banner
    /// </summary>
    public class banner
    {
        /// <summary>
        /// banner
        /// </summary>
        public banner()
        {
        }

        private System.Int64 _ID;
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 ID { get { return this._ID; } set { this._ID = value; } }

        private System.String _Img;
        /// <summary>
        /// Img
        /// </summary>
        public System.String Img { get { return this._Img; } set { this._Img = value; } }

        private System.String _URL;
        /// <summary>
        /// URL
        /// </summary>
        public System.String URL { get { return this._URL; } set { this._URL = value; } }

        private System.Object _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.Object CreateDate { get { return this._CreateDate; } set { this._CreateDate = value; } }

        private System.Int64? _BannerOrder;
        /// <summary>
        /// BannerOrder
        /// </summary>
        public System.Int64? BannerOrder { get { return this._BannerOrder; } set { this._BannerOrder = value ?? default(System.Int64); } }
    }
}