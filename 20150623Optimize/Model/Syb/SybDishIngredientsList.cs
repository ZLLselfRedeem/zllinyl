using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 配料列表
    /// </summary>
    public class SybDishIngredientsList
    {
        public List<SybDishIngredients> list { get; set; }
        public PageNav page { get; set; }
    }
    public class SybDishIngredientsListExtend
    {
        public List<SybDishIngredientsExtend> list { get; set; }
        public PageNav page { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SybEmployeeShopInfo
    {
        public int employeeID { get; set; }
        public string UserName { get; set; }
        public string EmployeeFirstName { get; set; }
        // public bool isSupportEnterSyb { get; set; }
        //public bool isSupportShopManagePage { get; set; }
        //public bool isSupportReceiveMsg { get; set; }
    }
    public class SybEmployeeShopInfoList
    {
        public List<SybEmployeeShopInfo> list { get; set; }
        public PageNav page { get; set; }
    }
    /// <summary>
    /// 服务员权限
    /// </summary>
    public class WaiterRoleInfo
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public bool isHave { get; set; }
    }

    public class WaiterRole
    {
        public List<WaiterRoleInfo> sybRoles { set; get; }
        public List<WaiterRoleInfo> vaServiceRoles { set; get; }
    }
    /// <summary>
    /// 悠先服务用户支付点单列表
    /// </summary>
    public class CustomerOrderList
    {
        public List<CustomerOrder> customerOrderList { get; set; }
        public PageNav page { get; set; }
    }
    public class CustomerOrder
    {
        public long preOrder19dianId { get; set; }
        public string shopName { get; set; }
        public double prePaidSum { get; set; }
        public Guid orderId { set; get; }
    }
}
