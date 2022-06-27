using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class horbShop
    {
        /// <summary>
        /// 
        /// </summary>
        public horbShop()
        {
        }

        private System.Int32 _ID;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 ID { get { return this._ID; } set { this._ID = value; } }

        private System.String _GoodsTitel;
        /// <summary>
        /// 
        /// </summary>
        public System.String GoodsTitel { get { return this._GoodsTitel; } set { this._GoodsTitel = value; } }

        private System.String _GoodsContent;
        /// <summary>
        /// 
        /// </summary>
        public System.String GoodsContent { get { return this._GoodsContent; } set { this._GoodsContent = value; } }

        private System.Int32? _Class1;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Class1 { get { return this._Class1; } set { this._Class1 = value; } }

        private System.Int32? _Class2;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Class2 { get { return this._Class2; } set { this._Class2 = value; } }

        private System.DateTime? _CreatedOn;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedOn { get { return this._CreatedOn; } set { this._CreatedOn = value; } }

        private System.String _Url;
        /// <summary>
        /// 
        /// </summary>
        public System.String Url { get { return this._Url; } set { this._Url = value; } }
    }
}