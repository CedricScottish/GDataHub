# GDataHub
A generic tool that provides a unified interface for accessing different databases.

## A Small Code Example

```C#
try
{
    IDataBase db = DataBaseSwitcher.GetDataBase(Util.DBType.SQL, "Your Connection String");
    dgTestGrid.DataSource = db.ExecuteQueryDataTable("Your SQL Code Here");
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message.ToString());
}
```
## Note
It currently only supports ORACLE and MSSQL databases. Providing support for other databases is quite simple and will be done at the first opportunity. 
