using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class GridPager
    {
        /// <summary>
        /// 排序的列名
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序方式,asc或desc
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页显示的行数
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int Count { get; set; }
    }
    public static class GridExtends
    {
        /// <summary>
        /// 将分页信息转成分页控件能读取的json格式
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public static string ToGridPagerJson(this GridPager pager)
        {
            string json = "{";
            json += string.Format("\"totalpage\":\"{0}\",\"totalcount\":\"{1}\",\"currentpage\":\"{2}\",\"sortcolumn\":\"{3}\",\"orderby\":\"{4}\",\"pagesize\":{5}"
                , pager.TotalPage, pager.Count, pager.Page, pager.SortColumn, pager.OrderBy, pager.Size);
            json += "}";
            return json;
        }

        /// <summary>
        /// 将已经分页的列表转成grid格式的Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radios"></param>
        /// <param name="pager"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static object ToGridJson<T>(this List<T> radios, GridPager pager, Func<T, object> selector = null)
        {
            if (radios != null)
            {
                if (selector == null)
                    return new { totalcount = pager.Count, page = pager.Page, totalpage = pager.TotalPage, rows = radios.ToArray() };
                else
                    return new { totalcount = pager.Count, page = pager.Page, totalpage = pager.TotalPage, rows = radios.Select(selector).ToArray() };
            }
            else
                return null;
        }

        /// <summary>
        /// 将未分页的列表转成grid格式的Json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radios"></param>
        /// <param name="pager"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static object ToGridJson<T>(this IEnumerable<T> radios, ref GridPager pager, Func<T, object> selector = null, bool needOrder = true)
        {
            if (radios != null)
            {
                int n = radios.Count();//总记录数
                int t = (int)Math.Ceiling((double)n / (double)pager.Size);//总页数
                int s = n % pager.Size;
                int takecount = pager.Size;
                if (pager.Page > t)
                    pager.Page = t;
                if (pager.Page == t && s > 0)
                    takecount = s;
                IEnumerable<T> olist;
                //if (needOrder)
                //    olist = radios.OrderBy(pager.SortColumn, pager.OrderBy).Skip((pager.Page - 1) * pager.Size).Take(takecount);
                //else
                //    olist = radios.Skip((pager.Page - 1) * pager.Size).Take(takecount);
                olist = radios.Skip((pager.Page - 1) * pager.Size).Take(takecount);
                pager.TotalPage = t;
                pager.Count = n;

                if (selector == null)
                    return new { totalpage = t, page = pager.Page, totalcount = n, rows = olist.ToArray() };
                else
                    return new { totalpage = t, page = pager.Page, totalcount = n, rows = olist.Select(selector).ToArray() };
            }
            else
                return null;
        }
    }
}