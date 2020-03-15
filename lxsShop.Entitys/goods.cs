using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// goods
    /// </summary>
    public class goods
    {
        /// <summary>
        /// goods
        /// </summary>
        public goods()
        {
        }

        private System.Int64 _goodsId;
        /// <summary>
        /// goodsId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 goodsId { get { return this._goodsId; } set { this._goodsId = value; } }

        private System.String _goodsSn;
        /// <summary>
        /// goodsSn
        /// </summary>
        public System.String goodsSn { get { return this._goodsSn; } set { this._goodsSn = value; } }

        private System.String _goodsName;
        /// <summary>
        /// goodsName
        /// </summary>
        public System.String goodsName { get { return this._goodsName; } set { this._goodsName = value; } }

        private System.String _goodsImg;
        /// <summary>
        /// goodsImg
        /// </summary>
        public System.String goodsImg { get { return this._goodsImg; } set { this._goodsImg = value; } }

        private System.Int64 _isRecom;
        /// <summary>
        /// isRecom
        /// </summary>
        public System.Int64 isRecom { get { return this._isRecom; } set { this._isRecom = value; } }

        private System.Int64 _isNew;
        /// <summary>
        /// isNew
        /// </summary>
        public System.Int64 isNew { get { return this._isNew; } set { this._isNew = value; } }

        private System.Int64 _brandId;
        /// <summary>
        /// brandId
        /// </summary>
        public System.Int64 brandId { get { return this._brandId; } set { this._brandId = value; } }

        private System.String _goodsDesc;
        /// <summary>
        /// goodsDesc
        /// </summary>
        public System.String goodsDesc { get { return this._goodsDesc; } set { this._goodsDesc = value; } }

        private System.String _gallery;
        /// <summary>
        /// gallery
        /// </summary>
        public System.String gallery { get { return this._gallery; } set { this._gallery = value; } }

        private System.String _goodsSeoKeywords;
        /// <summary>
        /// goodsSeoKeywords
        /// </summary>
        public System.String goodsSeoKeywords { get { return this._goodsSeoKeywords; } set { this._goodsSeoKeywords = value; } }

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

        private System.Int64 _goodsCatId;
        /// <summary>
        /// goodsCatId
        /// </summary>
        public System.Int64 goodsCatId { get { return this._goodsCatId; } set { this._goodsCatId = value; } }
    }
}
