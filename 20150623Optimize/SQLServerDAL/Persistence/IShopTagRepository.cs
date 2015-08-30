using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IShopTagRepository
    {
        /// <summary>
        /// 客户端查询当前从城市一级商圈信息（悠先点菜）
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        List<ShopTag> GetFirstGradeShopTagByCityId(int cityId);
        /// <summary>
        /// 客户端查询当前从城市二级商圈信息（悠先点菜）
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        List<ShopTag> GetSecondGradeShopTagByCityId(int cityId);
        /// <summary>
        /// 查询当前城市某个一级商圈所有二级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<ShopTag> GetSecondGradeShopTagByCityId(int cityId, int flag);
        /// <summary>
        /// 获取当前商圈门店ID
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        List<int> GetCurrectBusinessDistrictShopId(int tagId);
        /// <summary>
        /// 查询当前门店下所有上线门店
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        List<ShopTagExt> GetCurrectBusinessDistrictHandleShopId(int cityId);
        /// <summary>
        /// 根据一级商圈ID查询其所有二级商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        List<ShopTag> GetSecondGradeShopTagByFirstGrade(int tagId);

        #region 服务器端配置页面方法
        /// <summary>
        /// 新增商圈
        /// </summary>
        /// <param name="tagId">父商圈TagId</param>
        /// <param name="name">商圈名称</param>
        /// <returns></returns>
        int AddShopTag(int tagId, string name);
        /// <summary>
        /// 新增商圈归属城市
        /// </summary>
        /// <param name="tagId">商圈Id</param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        bool AddDistrict(int tagId, int cityId);
        /// <summary>
        /// 新增商圈和门店关联关系
        /// </summary>
        /// <param name="tagId">当前商圈TagId</param>
        /// <param name="shopId">门店编号</param>
        /// <returns></returns>
        int AddShopWithTag(int tagId, int shopId);
        /// <summary>
        /// 更新商圈名称
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool UpdateShopTagName(int tagId, string name);

        /// <summary>
        /// 更新商圈绑定店铺的数量，包括二级及一级
        /// </summary>
        /// <param name="tagId">可以传N个二级商圈tagId，用英文逗号隔开</param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool UpdateShopTagCount(string tagId, int shopCount);

        /// <summary>
        /// 删除商圈
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        bool DeleteShopTag(string tagIds);

        /// <summary>
        /// 删除店铺和商圈的关系
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        int DeleteShopWithTag(string tagId, int shopId);

        /// <summary>
        /// 根据shopId查询其所有的商圈标记
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        List<ShopTag> GetShopTagByShopId(int shopId);

        /// <summary>
        /// 检查shop和商圈的对应关系是否存在，TRUE表示存在
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        bool CheckShopTagRelation(int tagId, int shopId);

        #endregion
    }
}
