using System;
using System.Data;
using DAL;
using DTO;
namespace BLL
{
    class BLLAccountPlan
    {
        DALSql sql = new DALSql();

        public void SaveAccountPlan(AccountPlan accountPlan)
        {
            try
            {
                sql.TableName = "TBL_ACCOUNTPLAN";
                sql.setValues(null, accountPlan.Description, accountPlan.ToString());
                sql.Insert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAccountPlan(AccountPlan accountPlan)
        {
            try
            {
                sql.TableName = "TBL_ACCOUNTPLAN";
                sql.setConditionFields("ID =");
                sql.setCondition(accountPlan.Id.ToString());
                sql.Delete();
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAccountPlan(AccountPlan accountPlan)
        {
            try
            {
                sql.TableName = "TBL_ACCOUNTPLAN";
                sql.setFields("TYPE  ", "DESCRIPTION ");
                sql.setValues(accountPlan.Type.ToString(), accountPlan.Description);
                sql.setConditionFields("ID =");
                sql.setCondition(accountPlan.Id.ToString());
                sql.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable SelectAllAccountPlan()
        {
            try
            {
                sql.TableName = "TBL_ACCOUNTPLAN";
                sql.setFields("id as CODIGO, DESCRIPTION as DESCRICAO, TYPE as TIPO");
                return sql.Select();
            }
            catch (Exception)
            {
                throw;
            }

        }
        
    }
}
