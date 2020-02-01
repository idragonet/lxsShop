using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// sqlite_sequence
    /// </summary>
    public class sqlite_sequence
    {
        /// <summary>
        /// sqlite_sequence
        /// </summary>
        public sqlite_sequence()
        {
        }

        private System.Object _name;
        /// <summary>
        /// name
        /// </summary>
        public System.Object name { get { return this._name; } set { this._name = value; } }

        private System.Object _seq;
        /// <summary>
        /// seq
        /// </summary>
        public System.Object seq { get { return this._seq; } set { this._seq = value; } }
    }
}