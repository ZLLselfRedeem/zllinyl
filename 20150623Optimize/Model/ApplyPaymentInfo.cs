using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 申请打款信息
    /// </summary>
   public class ApplyPaymentInfo
    {
       #region Model
        public ApplyPaymentInfo()
        { }
       
        private long _identity_id;
        private int _companyid;
        private int? _shopid;
        private double? _prepaidsum;
        private double? _viewalloccommission;
        private double? _actualpaidsum;
        private DateTime? _applydate;
        private DateTime? _appfromtime;
        private DateTime? _apptotime;
        private int _applystatus;
        private int? _checkpersonid;
        private string _accountnum;
        private string _remittancenum;
        /// <summary>
        /// 
        /// </summary>
        public long identity_Id
        {
            set { _identity_id = value; }
            get { return _identity_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int companyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shopId
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? prePaidSum
        {
            set { _prepaidsum = value; }
            get { return _prepaidsum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? viewallocCommission
        {
            set { _viewalloccommission = value; }
            get { return _viewalloccommission; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? actualPaidSum
        {
            set { _actualpaidsum = value; }
            get { return _actualpaidsum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? applyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? appFromTime
        {
            set { _appfromtime = value; }
            get { return _appfromtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? appToTime
        {
            set { _apptotime = value; }
            get { return _apptotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int applyStatus
        {
            set { _applystatus = value; }
            get { return _applystatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? checkPersonId
        {
            set { _checkpersonid = value; }
            get { return _checkpersonid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accountNum
        {
            set { _accountnum = value; }
            get { return _accountnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RemittanceNum
        {
            set { _remittancenum = value; }
            get { return _remittancenum; }
        }
        #endregion Model
    }
}
