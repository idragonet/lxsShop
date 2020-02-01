using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// brands
    /// </summary>
    public class brands
    {
        /// <summary>
        /// brands
        /// </summary>
        public brands()
        {
        }

        private System.Int64 _brandId;
        /// <summary>
        /// brandId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 brandId { get { return this._brandId; } set { this._brandId = value; } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// CreateDate
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value; } }

        private System.String _brandName;
        /// <summary>
        /// brandName
        /// </summary>
        public System.String brandName { get { return this._brandName; } set { this._brandName = value; } }

        private System.String _brandImg;
        /// <summary>
        /// brandImg
        /// </summary>
        public System.String brandImg { get { return this._brandImg; } set { this._brandImg = value; } }

        private System.String _brandDesc;
        /// <summary>
        /// brandDesc
        /// </summary>
        public System.String brandDesc { get { return this._brandDesc; } set { this._brandDesc = value; } }

        private System.Int64? _dataFlag;
        /// <summary>
        /// dataFlag
        /// </summary>
        public System.Int64? dataFlag { get { return this._dataFlag; } set { this._dataFlag = value; } }
    }
}