CREATE TABLE [dbo].[ViewallocInfo] (
    [remainMoney] FLOAT (53) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账户余额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ViewallocInfo', @level2type = N'COLUMN', @level2name = N'remainMoney';

