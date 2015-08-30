using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using System.Configuration;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class GoodsOperate
    {
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public object[] InsertGoods(Goods goods)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.InsertGoods(goods);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public object[] UpdateGoods(Goods goods)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.UpdateGoods(goods);
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public object[] DeleteGoods(int goodId)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.DeleteGoods(goodId);
        }

        /// <summary>
        /// 根据GoodId查询相应的商品信息
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public Goods QueryGoods(int goodId)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.QueryGoods(goodId);
        }

        /// <summary>
        /// 查询所有有效的商品数据
        /// </summary>
        /// <returns></returns>
        public IList<Goods> QueryGoods()
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.QueryGoods();
        }

        /// <summary>
        /// 根据商品名称查询符合的数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<Goods> QueryGoods(string name)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.QueryGoods(name);
        }

        /// <summary>
        /// 根据商品名称查询符合的数据
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public DataTable QueryGoodsDataTable(string name)
        {
            GoodsManage _GoodsManage = new GoodsManage();
            return _GoodsManage.QueryGoodsDataTable(name);
        }

        /// <summary>
        /// 客户端web网页查询商品
        /// </summary>
        /// <returns></returns>
        public string ClientQueryGoods()
        {
            GoodsOperate _Operate = new GoodsOperate();//BLL
            IList<Goods> goods = QueryGoods();
            if (goods.Count > 0)
            {
                string server = WebConfig.CdnDomain;
                string path = server + WebConfig.Goods;
                foreach (Goods good in goods)
                {
                    good.pictureName = path + good.pictureName;
                }
            }
            string result = Common.ConvertListToJson<IList<Goods>>(goods);
            return result;
        }
    }
}
