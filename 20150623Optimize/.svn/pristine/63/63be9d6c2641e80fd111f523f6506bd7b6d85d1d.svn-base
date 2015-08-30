using System;
using System.Collections.Generic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public interface IRedEnvelopeRepository
    {
        IEnumerable<RedEnvelope> GetManyByTreasureChest(long treasureChestId);

        RedEnvelope GetByTreasureChestAndMobilePhone(long treasureChestId, string mobilePhone);

        RedEnvelope GetByActivityAndMobilePhone(int activityId, string mobilePhone, string weChatUserId);

        bool AnyByTreasureChestAndUser(long treasureChestId, string mobilePhone);
        int CountByUserToday(string mobilePhone, DateTime today);
        double SumUserAmount(string mobilePhone);
        void Add(RedEnvelope redEnvelope);
        void UpdateTreasureChestExecuted(long treasureChestId, VARedEnvelopeStateType isExecuted);
        IEnumerable<RedEnvelope> GetPageByActivity(Page page, int activityId);

        bool AnyRedEnvelopeOwnerInActivity(string mobilePhone, int activityId);

        RedEnvelope GetByTreasureChestAndCookie(long treasureChestId, string cookie);
        RedEnvelope GetByActivityAndCookie(int activityId, string cookie);

        RedEnvelope GetById(long id);

        int UpdateMobilePhoneNumberAndisExecuted(long redEnvelopeId, string mobilePhoneNumber, VARedEnvelopeStateType vARedEnvelopeStateType, string uuid, double amount, string weChatUserId);


        void Sum(long treasureChestId, out int count, out double amount);
        void Sum(int activityId, out double amount);

        IEnumerable<RedEnvelope> Ranklist();

        bool RankLiskNumber(string mobilePhoneNumber, out long ranking, out double amount);
        int UpdateMobilePhoneNumberAndIsChange(long redEnvelopeId, string mobilePhoneNumber, string uuid);
        /// <summary>
        /// 更新用户已生效和未生效金额
        /// </summary>
        /// <param name="oldPhone"></param>
        /// <param name="newPhone"></param>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        //int UpdateCustomerRedEnvelopeAmount(string oldPhone, string newPhone, double amount, bool effect);
        //int AddCustomerRedEnvelopeAmount(string Phone, double amount, bool effect);
        List<AppUUIDModel> GetCustomerDeviceUUID(string Phone, int activityId);

        /// <summary>
        /// 判断微信用户是否抢过
        /// </summary>
        /// <param name="activityId">活动号</param>
        /// <param name="weChatUserId">微信用户id</param>
        /// <returns></returns>
        RedEnvelope GetWeChatModel(int activityId, Guid weChatUserId);

         /// <summary>
        /// 按活动号跟uuid返回model
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="mobile">设备号</param>
        /// <returns></returns>
        RedEnvelope GetAcitvityIdAndUuidModel(int activityId, string mobile);

         /// <summary>
        /// 更新微信用户id
        /// </summary>
        /// <param name="redEnvelopeId">id</param>
        /// <param name="weChatUserId">微信用户id</param>
        /// <returns></returns>
        int UpdateWeChatUserId(long redEnvelopeId, Guid weChatUserId);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="redEnvelopeId">id</param>
        /// <returns></returns>
        int DelSingleData(long redEnvelopeId);
    }
}