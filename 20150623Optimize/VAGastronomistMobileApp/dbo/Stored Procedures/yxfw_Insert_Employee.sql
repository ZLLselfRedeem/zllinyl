
/**created by wangc 20140417**/
/**优先服务新增用户，减少避免重复注册概率**/
CREATE proc [dbo].[yxfw_Insert_Employee] 
    @UserName nvarchar(50), ----手机号码
    @cookie nvarchar(100), ----cookie
    @EmployeeStatus int, ----用户状态
    @EmployeePhone nvarchar(50), ----手机号码
	@EmployeeAge int,----年龄
    @EmployeeSequence int,----排序
    @EmployeeSex int,----性别
    @Password nvarchar(50), ----密码
	@isViewAllocWorker bit,---是否va员工
    @registerTime datetime,----注册时间
    @isSupportLoginBgSYS bit,----可进去后台
    @EmployeeFirstName nvarchar(50),----员工姓名
	@position nvarchar(100),--职位
	@defaultPage nvarchar(500),--缺省页面
    @rtn int output 
as 
----判断该手机号码是否已注册
    if exists(select * from EmployeeInfo where UserName=@UserName and EmployeeStatus>0) 
    ----是，直接return出去
        begin 
            set @rtn=0
             return @rtn
        end 
    else
    ----否，没有相同的数据，进行插入处理 
        begin 
           insert into
            EmployeeInfo(UserName,Password,EmployeeSex,EmployeeAge,EmployeePhone,
            EmployeeSequence,EmployeeStatus,cookie,isViewAllocWorker,
            registerTime,isSupportLoginBgSYS,EmployeeFirstName, position,defaultPage)
            values (@UserName,@Password,@EmployeeSex,@EmployeeAge,@EmployeePhone,
            @EmployeeSequence,@EmployeeStatus,@cookie,@isViewAllocWorker,
            @registerTime,@isSupportLoginBgSYS,@EmployeeFirstName,@position,@defaultPage )
            set @rtn=1 
            return @rtn
        end
