using System;
using System.Data;

namespace DAL
{
    /// <summary>
    ///  Class used to execute SQL statements
    /// </summary>
    class DALSql
    {
        DALConnection dal;
        private string[] parameters, conditionParameters , values, fields, conditionFields, condition;
        private string tableName;

        public string TableName { get => tableName; set => tableName = value; }

        /// <summary>
        /// Method used to assign values
        /// </summary>
        /// <param name="values"> Pass the desired values Ex:(NULL,Arhtur,25)</param>
        public void setValues(params string[] values)
        {
            this.values = values;
        }

        /// <summary>
        /// Method used to assign fields / columns in a query
        /// Do not use to insert
        /// </summary>
        /// <param name="fields">Pass the desired fields Ex:(NAME=,AGE=)</param>
        public void setFields(params string[] fields)
        {
            this.fields = fields;
        }

        /// <summary>
        ///  Method used to assign fields and conditions in a query
        /// </summary>
        /// <param name="conditionFields">Pass the fields and their condition Ex:([NAME OF FIELD/COLUMN] =, [NAME OF FIELD/COLUMN] !=)</param>
        public void setConditionFields(params string[] conditionFields)
        {
            this.conditionFields = conditionFields;
        }

        /// <summary>
        /// Method used to assign the values ​​of a condition
        ///  Need to use setConditionsFields first
        /// </summary>
        /// <param name="condition">Pass the values ​​to your condition Ex:(ARTHUR,26)</param>
        public void setCondition(params string[] condition)
        {
            this.condition = condition;
        }

        /// <summary>
        /// Method to insert record.Required TableName, SetValues ​​()
        /// </summary>
        public void Insert()
        {
            try
            {
                parameters = new string[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                        parameters[i] = "@val" + i;
                }
                dal = new DALConnection();
                dal.executeCommand(string.Format("INSERT INTO {0} VALUES ({1})", tableName, String.Join(",", parameters)), this.values);
            }
            catch (Exception)
            {
                if (ValidateData() == true)
                {
                    throw;
                }
            }
            finally
            {
                ClearData();
            }
        }

        public void Delete()
        {
            try
            {
                parameters = new string[condition.Length];
                for (int i = 0; i < condition.Length; i++)
                {
                    parameters[i] = conditionFields[i] + "@val" + i;
                }
                dal = new DALConnection();
                dal.executeCommand(string.Format("DELETE FROM {0} WHERE {1}", tableName, String.Join("", parameters)), this.condition);
            }
            catch (Exception)
            {
                if (ValidateData() == true)
                {
                    throw;
                }
            }
            finally
            {
                ClearData();
            }
        }

        public void Update()
        {
            try
            {
                parameters = new string[fields.Length];
                conditionParameters = new string[conditionFields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    parameters[i] = fields[i] + "= @val" + i;
                }
                for (int i = 0; i < conditionFields.Length; i++)
                {
                    conditionParameters[i] = conditionFields[i] + "@con" + i;
                }
                dal = new DALConnection();
                dal.executeCommand(string.Format("UPDATE {0} SET {1} WHERE {2} ", tableName, String.Join(",", parameters), String.Join("", conditionParameters)), this.values, this.condition);
            }
            catch (Exception)
            {
                if (ValidateData() == true)
                {
                    throw;
                }
            }
            finally
            {
                ClearData();
            }
        }

        public DataTable Select()
        {
            try
            {
                dal = new DALConnection();
                return dal.executeConsult(string.Format("SELECT {0} FROM {1} ",String.Join(",",this.fields), this.tableName));
            }
            catch (Exception)
            {
                if (ValidateData() == true)
                {
                    throw;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                ClearData();
            }

        }



        public DataTable SelectWithCondition()
        {
            try
            {
                parameters = new string[fields.Length];
                conditionParameters = new string[conditionFields.Length];
                for (int i = 0; i < conditionFields.Length; i++)
                {
                    conditionParameters[i] = conditionFields[i] + "@val" + i;
                }
                dal = new DALConnection();
                return dal.executeConsult(string.Format("SELECT {0} FROM {1} WHERE {2} ", String.Join(",",fields), this.tableName, String.Join("", conditionParameters)), condition);
            }
            catch (Exception)
            {
                if (ValidateData() == true)
                {
                    throw;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            this.tableName = null;
            this.parameters = null;
            this.conditionParameters = null;
            this.values = null;
            this.fields = null;
            this.conditionFields = null;
            this.condition = null;
        }
        private bool ValidateData()
        {
            if (this.TableName == null)
            {
                throw new Exception("Tabela não foi atribuída !");
            }
            else if (this.values == null)
            {
                throw new Exception("Valores não foram atribuídos !");
            }
            else if (this.fields == null)
            {
                throw new Exception("Campos da Tabela não foram atribuídos !");
            }
            else if (this.conditionFields == null)
            {
                throw new Exception("Campos para a Condição não foram atribuídos !");
            }
            else if (this.condition == null)
            {
                throw new Exception("Valores para a Condição não foram atribuídos !");
            }
            else
            {
                return true;
            }
        }
    }
}
