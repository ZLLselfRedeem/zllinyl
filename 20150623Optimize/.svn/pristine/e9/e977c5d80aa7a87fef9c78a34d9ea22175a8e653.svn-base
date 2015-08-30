using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VAGastronomistMobileApp.WebPageDll.Services
{
    public interface IShopTagService
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
        /// 获取当前城市商圈
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        List<BusinessDistrictTag> GetShopBusinessDistricts(int cityId);
        /// <summary>
        /// 获取当前商圈门店ID
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        string GetCurrectBusinessDistrictShopId(int tagId);

        List<ShopTag> GetSecondGradeShopTagByFirstGrade(int tagId);

        int GetWithdrawTypeShopId(int shopid);

        int UpdateWithdrawType(int shopid, int withdrawtype);

        double GetViewallocCommissionValue(int shopid);

        int UpdateViewallocCommissionValue(int shopid, double viewalloccommissionvalue);

        double GetShopVipInfo(int shopid);

        int UpdateShopVipInfo(int shopid, double discount);

        CompanyAccountInfo GetAccountInfo(int shopid);

        #region 服务器端配置页面方法
        /// <summary>
        /// 新增商圈
        /// </summary>
        /// <param name="tagId">父商圈TagId</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="name">商圈名称</param>
        /// <returns></returns>
        bool AddShopTag(int tagId, int cityId, string name);
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
        /// 查询当前城市某个一级商圈所有二级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<ShopTag> GetSecondGradeShopTagByCityId(int cityId, int flag);

        /// <summary>
        /// 根据shopId查询其所有的商圈标记
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        List<ShopTag> GetShopTagByShopId(int shopId);

        /// <summary>
        /// 维护店铺和商圈的关系
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <param name="delete">若是删除请传true，新增请传false</param>
        /// <returns></returns>
        object[] MaintainShopTag(string tagId, int shopId, bool delete);

        #endregion
    }
    public class ShopTagService : BaseService, IShopTagService
    {
        private IShopTagRepository shopTagRepository;
        public ShopTagService(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            shopTagRepository = RepositoryContext.GetShopTagRepository();
        }

        public List<BusinessDistrictTag> GetShopBusinessDistricts(int cityId)
        {
            List<ShopTag> listFirstShopTags = shopTagRepository.GetFirstGradeShopTagByCityId(cityId);
            List<ShopTag> listSecondShopTags = shopTagRepository.GetSecondGradeShopTagByCityId(cityId);//有上线门店的二级商圈
            List<ShopTagExt> listHandleShop = shopTagRepository.GetCurrectBusinessDistrictHandleShopId(cityId);
            var listBusinessDistrict = new List<BusinessDistrictTag>();
            listBusinessDistrict.Add(new BusinessDistrictTag()
            {
                tagId = 1,
                name = "全部区域",
                childTags = new List<BusinessDistrictTag>()
            });
            if (listFirstShopTags != null && listFirstShopTags.Any())
            {
                foreach (var firstTagItem in listFirstShopTags)
                {
                    List<ShopTag> childTagsList = listSecondShopTags.Where(p => p.Flag == firstTagItem.Flag).ToList();//找寻子商圈
                    var listChildBusinessDistrict = new List<BusinessDistrictTag>();
                    if (childTagsList.Any())
                    {
                        foreach (var firstChildTagItem in childTagsList)
                        {
                            if (listHandleShop.Any(s => s.TagId == firstTagItem.TagId))//该商圈有上线门店
                            {
                                listChildBusinessDistrict.Add(new BusinessDistrictTag()
                                {
                                    name = firstChildTagItem.Name,
                                    tagId = firstChildTagItem.TagId,
                                    childTags = new List<BusinessDistrictTag>() //二级截至，涉及多级，改用递归);
                                });
                            }
                        }
                        if (listHandleShop.Any(s => s.TagId == firstTagItem.TagId))//该商圈有上线门店
                        {
                            listBusinessDistrict.Add(new BusinessDistrictTag()
                            {
                                name = firstTagItem.Name,
                                tagId = firstTagItem.TagId,
                                childTags = listChildBusinessDistrict
                            });
                        }
                    }
                }
            }
            return listBusinessDistrict;
        }

        /// <summary>
        /// 根据一级商圈ID查询其所有二级商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public string GetCurrectBusinessDistrictShopId(int tagId)
        {
            List<int> shopIdList = shopTagRepository.GetCurrectBusinessDistrictShopId(tagId);
            if (shopIdList.Any())
            {
                StringBuilder str = new StringBuilder();
                str.Append("(");
                foreach (var item in shopIdList)
                {
                    str.Append(item + ",");
                }
                return str.ToString().TrimEnd(',') + ")";
            }
            return "";
        }

        #region 服务器端配置页面方法
        /// <summary>
        /// 新增商圈
        /// </summary>
        /// <param name="tagId">父商圈TagId</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="name">商圈名称</param>
        /// <returns></returns>
        public bool AddShopTag(int tagId, int cityId, string name)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = shopTagRepository.AddShopTag(tagId, name);
                if (id > 0)
                {
                    bool falg = shopTagRepository.AddDistrict(id, cityId);
                    if (falg)
                    {
                        scope.Complete();
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 新增商圈和门店关联关系
        /// </summary>
        /// <param name="tagId">当前商圈TagId</param>
        /// <param name="shopId">门店编号</param>
        /// <returns></returns>
        public int AddShopWithTag(int tagId, int shopId)
        {
            return shopTagRepository.AddShopWithTag(tagId, shopId);
        }
        /// <summary>
        /// 更新商圈名称
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UpdateShopTagName(int tagId, string name)
        {
            return shopTagRepository.UpdateShopTagName(tagId, name);
        }

        /// <summary>
        /// 更新商圈绑定店铺的数量，包括二级及一级
        /// </summary>
        /// <param name="tagId">可以传N个二级商圈tagId，用英文逗号隔开</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateShopTagCount(string tagId, int shopCount)
        {
            return shopTagRepository.UpdateShopTagCount(tagId, shopCount);
        }

        /// <summary>
        /// 删除商圈
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public bool DeleteShopTag(string tagIds)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (shopTagRepository.DeleteShopTag(tagIds))
                {
                    scope.Complete();
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 客户端查询当前从城市一级商圈信息（悠先点菜）
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ShopTag> GetFirstGradeShopTagByCityId(int cityId)
        {
            return shopTagRepository.GetFirstGradeShopTagByCityId(cityId);
        }
        /// <summary>
        /// 客户端查询当前从城市二级商圈信息（悠先点菜）
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByCityId(int cityId)
        {
            return shopTagRepository.GetSecondGradeShopTagByCityId(cityId);
        }
        /// <summary>
        /// 查询当前城市某个一级商圈所有二级商圈信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByCityId(int cityId, int flag)
        {
            return shopTagRepository.GetSecondGradeShopTagByCityId(cityId, flag);
        }

        /// <summary>
        /// 根据一级商圈ID查询其所有二级商圈
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public List<ShopTag> GetSecondGradeShopTagByFirstGrade(int tagId)
        {
            return shopTagRepository.GetSecondGradeShopTagByFirstGrade(tagId);
        }

        /// <summary>
        /// 根据shopId查询其所有的商圈标记
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<ShopTag> GetShopTagByShopId(int shopId)
        {
            return shopTagRepository.GetShopTagByShopId(shopId);
        }

        /// <summary>
        /// 维护店铺和商圈的关系
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="shopId"></param>
        /// <param name="delete">若是删除请传true，新增请传false</param>
        /// <returns></returns>
        public object[] MaintainShopTag(string childrenTagId, int shopId, bool delete)
        {
            int relation = 0;
            bool shopCount = false;
            object[] obj = new object[] { false, "" };
            using (TransactionScope ts = new TransactionScope())
            {
                if (delete)
                {
                    relation = shopTagRepository.DeleteShopWithTag(childrenTagId, shopId);
                    shopCount = shopTagRepository.UpdateShopTagCount(childrenTagId, -1);
                }
                else//新增
                {
                    //先检查有没有此关系
                    bool check = shopTagRepository.CheckShopTagRelation(Common.ToInt32(childrenTagId), shopId);
                    if (check)
                    {
                        //obj[1] = "此店铺已经属于此商圈";
                        relation = 1;
                        shopCount = true;
                    }
                    else
                    {
                        relation = shopTagRepository.AddShopWithTag(Common.ToInt32(childrenTagId), shopId);
                        shopCount = shopTagRepository.UpdateShopTagCount(childrenTagId, 1);
                    }
                }
                if (relation > 0 && shopCount)
                {
                    ts.Complete();
                    obj[0] = true;
                    obj[1] = "操作成功";
                }
                else
                {
                    obj[1] = "操作失败";
                }
            }
            return obj;
        }

        /// <summary>
        /// 查询提款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int GetWithdrawTypeShopId(int shopid)
        {
            ShopOperate so = new ShopOperate();
            return so.getWithdrawType(shopid);
        }

        /// <summary>
        /// 修改提款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="withdrawtype"></param>
        /// <returns></returns>
        public int UpdateWithdrawType(int shopid, int withdrawtype)
        {
            ShopOperate so = new ShopOperate();
            return so.updateWithdrawType(shopid, withdrawtype);
        }

        /// <summary>
        /// 查询佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public double GetViewallocCommissionValue(int shopid)
        {
            ShopOperate so = new ShopOperate();
            return so.getViewallocCommissionValue(shopid);
        }

        /// <summary>
        /// 修改佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="viewalloccommissionvalue"></param>
        /// <returns></returns>
        public int UpdateViewallocCommissionValue(int shopid, double viewalloccommissionvalue)
        {
            ShopOperate so = new ShopOperate();
            return so.updateViewallocCommissionValue(shopid, viewalloccommissionvalue);
        }

        public double GetShopVipInfo(int shopid)
        {
            ShopOperate so = new ShopOperate();
            return so.getShopVipInfo(shopid);
        }

        public int UpdateShopVipInfo(int shopid,double discount)
        {
            ShopOperate so=new ShopOperate();
            return so.updateShopVipInfo(shopid, discount);
        }

        public CompanyAccountInfo GetAccountInfo(int shopid)
        {
            CompanyAccountOprate cao = new CompanyAccountOprate();
            return cao.GetAccountInfo(shopid);
        }
        #endregion
    }
}
