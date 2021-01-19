using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;
using Entitys;
using FineUICore;
using lxsShop.NewServices;
using lxsShop.NewServices.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lxsShop.Web.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class HomeController : BaseController
    {

        private readonly IgoodServer _goodserver;
        private readonly IbrandsServer _brandsserver;
        private readonly IAdminUsersServer _adminUsersserverr;

        public HomeController(IgoodServer goodserver, Igoods_catsServer goodscatsserver, IbrandsServer brandsserver, IAdminUsersServer adminUsersserverr)
        {
            _goodserver = goodserver;
            _brandsserver = brandsserver;
            _adminUsersserverr = adminUsersserverr;
        }


        [HttpGet]
        public string Get()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            LoadTreeMenuData();


            ViewBag.OS= RuntimeInformation.OSDescription;
            ViewBag.Ver= System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

            var post = await _goodserver.GetPagesAsync(new PageParm(){limit = 999999});
            ViewBag.GoodsCount= post.data.Items.Count;


            var post2 = await _brandsserver.GetPagesAsync(new PageParm() { limit = 999999 });
            ViewBag.BrandCount = post2.data.Items.Count;


            return View();
        }



        private void LoadTreeMenuData()
        {
            string xmlPath = PageContext.MapWebPath("~/res/menu.xml");

            string xmlContent = String.Empty;
            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlContent = sr.ReadToEnd();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xmlContent);

            IList<TreeNode> nodes = new List<TreeNode>();
            ResolveXmlNodeList(nodes, xdoc.DocumentElement.ChildNodes);

            // 视图数据
            ViewBag.TreeMenuNodes = nodes.ToArray();
        }


        private int ResolveXmlNodeList(IList<TreeNode> nodes, XmlNodeList xmlNodes)
        {
            // nodes 中渲染到页面上的节点个数
            int nodeVisibleCount = 0;

            foreach (XmlNode xmlNode in xmlNodes)
            {
                if (xmlNode.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                TreeNode node = new TreeNode();

                // 是否叶子节点
                bool isLeaf = xmlNode.ChildNodes.Count == 0;

                bool currentNodeIsVisible = true;

                string nodeText = "";
                bool nodeIsCorp = false;

                XmlAttribute textAttr = xmlNode.Attributes["Text"];
                if (textAttr != null)
                {
                    nodeText = textAttr.Value;
                }

                // 是否企业版
                XmlAttribute isCorpAttr = xmlNode.Attributes["IsCorp"];
                if (isCorpAttr != null)
                {
                    nodeIsCorp = isCorpAttr.Value.ToLower() == "true";
                }


                int childVisibleCount = 0;

                node.Expanded = true;

                if (isLeaf)
                {
                    // 仅显示基础版示例
                    /*if (_cookieShowOnlyBase && nodeIsCorp)
                    {
                        currentNodeIsVisible = false;
                    }*/
                }
                else
                {
                    // 递归
                    childVisibleCount = ResolveXmlNodeList(node.Nodes, xmlNode.ChildNodes);

                    if (childVisibleCount == 0)
                    {
                        currentNodeIsVisible = false;
                    }
                }

                if (currentNodeIsVisible)
                {
                    foreach (XmlAttribute attribute in xmlNode.Attributes)
                    {
                        string name = attribute.Name;
                        string value = attribute.Value;

                        if (name == "Text")
                        {
                            // Text需要特殊处理
                            if (isLeaf)
                            {
                                // 设置节点的提示信息
                                node.ToolTip = nodeText;
                            }

                            // 存在 IsCorp=True 属性，则改变 Text 的值
                            if (nodeIsCorp)
                            {
                                node.IconFont = IconFont._Enterprise;
                                //nodeText += "&nbsp;<span class=\"iscorp\">Corp.</span>";
                            }

                            node.Text = nodeText;
                        }
                        else
                        {
                            node.SetPropertyValue(name, value);
                        }
                    }

                    nodes.Add(node);

                    // 本子节点显示
                    nodeVisibleCount++;


                }

            }

            return nodeVisibleCount;
        }



        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
          return Redirect("/Admin/Home/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> btnLogin_Click(string tbxUserName, string tbxPassword)
        {

           var ip= Get();

           var post = await _adminUsersserverr.LoginAsync(new AdminUsers {loginName = tbxUserName, loginPwd = tbxPassword},ip);

            if (string.IsNullOrEmpty(post.message))
            {
                //  ShowNotify("成功登录！", MessageBoxIcon.Success);



                //创建用户登录标识，Cookie名称与IServiceCollection中配置的一样即可
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //添加之后，可使用User.Identity.Name获取该值
                identity.AddClaim(new Claim(ClaimTypes.Name, post.data.loginName));

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                return Redirect("/Admin/Home/Index");
            }
            else
            {
                ShowNotify(post.message, MessageBoxIcon.Information);
            }

            //  return RedirectToPagePreserveMethod("index", "Index");
            return UIHelper.Result();
        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult btnHello_Click()
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);

            return UIHelper.Result();
        }

        public async Task<IActionResult> LoginOut_Click()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Home/Login");
        }
    }
}