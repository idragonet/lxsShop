using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Entitys;
using lxsShop.ViewModel;
using MySql.Data.MySqlClient;
using SqlSugar;

namespace lxsShop.Repository
{
   public class articleRepository : GenericRepository<articles>, IarticlesRepository
    {

        /// <summary>
        /// 需要多表查询，所以重写
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<articlesViewModel> FindAll2()
        {

            /*
            var list = Db.Queryable<Student, School>((st, sc) => new object[] {
                    JoinType.Left,st.SchoolId==sc.Id})
                .Select((st, sc) => new { Id = st.Id, Name = st.Name, SchoolName = sc.Name }).ToList();
                */

           // var list = db.Queryable<articles>().ToList();
            var list = db.Queryable<articles,article_cats>((st, sc) => new object[] {
                    JoinType.Inner,st.articleId==sc.catId})
                    .Select<articlesViewModel>().ToList();
                /*.Select((st, sc) => new
                {
                    articleId = st.articleId,
                    articleTitle = st.articleTitle,
                    catId = st.catId,
                    CreateDate = st.CreateDate,
                    CreatorUser = st.CreatorUser,
                    articleContent = st.articleContent,
                    dataFlag = st.dataFlag,
                    catName = sc.catName,
                    isShow = st.isShow
                })*/
                /*.ToList();*/

            return list;
        }
    }
}
