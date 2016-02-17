using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qin.Blog.Extentions
{
    public class PagesReturn : ActionResult
    {
        #region 属性
        private bool _status = false;
        private string _msg = string.Empty;
        private int _pageIndex = 1;
        private int _pageSize = 10;
        private int _total = 0;
        private int _pageCount = 0;
        private int _curPageCount = 0;
        private object _data;

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 当前页条数
        /// </summary>
        public int CurPageCount
        {
            get { return _curPageCount; }
            set { _curPageCount = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }
        #endregion

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public PagesReturn(int pageIndex, int total, object data, int curPageCount)
        {
            Status = true;
            PageIndex = pageIndex;
            PageSize = PageSize;
            PageCount = total / PageSize;
            Total = total;
            CurPageCount = curPageCount;
            Data = data;
        }


        public PagesReturn(bool status, int pageIndex, int total, string msg)
        {
            Status = false;
            PageIndex = pageIndex;
            PageSize = PageSize;
            PageCount = total / PageSize;
            Total = total;
            Msg = msg;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var model = new
            {
                status = Status,
                pageindex = PageIndex,
                pagesize = PageSize,
                pagecount = PageCount,
                total = Total,
                curpagecount = CurPageCount,
                data = Data,
                msg = Msg
            };
            var result = new JsonResult
            {
                Data = model,
                MaxJsonLength = 104857600,  //100MB
                JsonRequestBehavior = JsonRequestBehavior.AllowGet //允许GET请求
            };
            result.ExecuteResult(context);
        }
    }
}
