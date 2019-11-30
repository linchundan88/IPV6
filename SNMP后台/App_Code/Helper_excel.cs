using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data.OleDb;

/// <summary>
/// Helper_excel 的摘要说明
/// </summary>
public class Helper_excel
{
    public Helper_excel()
    {
 
    }


    public static System.Data.DataTable ImportFormExcel(string path)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter dataadapter = null;
        OleDbCommand datacommand = null;
        System.Data.DataTable myDatatable = null;
        try
        {
            #region OLEDB 连接Excel
            //Provider=Microsoft.ACE.OLEDB.12.0;这说明是excel2007以上的版本
            //;HDR=YES 第一行是列名而不是数据
            //将所有数据视为文本文档，仅需将设置"IMEX=1"，将Excel里面的默认"通用类型"转为文本类型。
            //可以 连接 xlsx
            //当 IMEX = 0 时为“汇出模式”，这个模式开启的 Excel 档案只能用来做“写入”用途。
            //当 IMEX = 1 时为“汇入模式”，这个模式开启的 Excel 档案只能用来做“读取”用途。
            //当 IMEX = 2 时为“连結模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。

            #endregion

            string strConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties=Excel 12.0;HDR=YES;IMEX=1";

            conn = new OleDbConnection(strConn);
            conn.Open();

            string strExcel = "";

            strExcel = "select * from [device$]";
            datacommand = new OleDbCommand(strExcel, conn);
            dataadapter = new OleDbDataAdapter(datacommand);

            myDatatable = new System.Data.DataTable();
            dataadapter.Fill(myDatatable);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            datacommand = null;
            dataadapter = null;
            conn.Close(); conn = null;
        }
        return myDatatable;
    }

    public static DataTable ExcelToDataTable(string filePath, bool isColumnName)
    {
        DataTable dataTable = null;
        FileStream fs = null;
        DataColumn column = null;
        DataRow dataRow = null;

        IWorkbook workbook = null;
        ISheet sheet = null;
        IRow row = null;
        ICell cell = null;
        int startRow = 0;
        try
        {
            using (fs = File.OpenRead(filePath))
            {
                // 2007版本  
                //if (filePath.IndexOf(".xlsx") > 0)
                workbook = new XSSFWorkbook(fs);
                // 2003版本  
                //else if (filePath.IndexOf(".xls") > 0)
                //  workbook = new HSSFWorkbook(fs);

                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                    dataTable = new DataTable();
                    if (sheet != null)
                    {
                        int rowCount = sheet.LastRowNum;//总行数  
                        if (rowCount > 0)
                        {
                            IRow firstRow = sheet.GetRow(0);//第一行  
                            int cellCount = firstRow.LastCellNum;//列数  

                            //构建datatable的列  
                            if (isColumnName)
                            {
                                startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                {
                                    cell = firstRow.GetCell(i);
                                    if (cell != null)
                                    {
                                        if (cell.StringCellValue != null)
                                        {
                                            column = new DataColumn(cell.StringCellValue);
                                            dataTable.Columns.Add(column);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                {
                                    column = new DataColumn("column" + (i + 1));
                                    dataTable.Columns.Add(column);
                                }
                            }

                            //填充行  
                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                row = sheet.GetRow(i);
                                if (row == null) continue;

                                dataRow = dataTable.NewRow();
                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    cell = row.GetCell(j);
                                    if (cell == null)
                                    {
                                        dataRow[j] = "";
                                    }
                                    else
                                    {
                                        //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                        switch (cell.CellType)
                                        {
                                            case CellType.Blank:
                                                dataRow[j] = "";
                                                break;
                                            case CellType.Numeric:
                                                short format = cell.CellStyle.DataFormat;
                                                //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                if (format == 14 || format == 31 || format == 57 || format == 58)
                                                    dataRow[j] = cell.DateCellValue;
                                                else
                                                    dataRow[j] = cell.NumericCellValue;
                                                break;
                                            case CellType.String:
                                                dataRow[j] = cell.StringCellValue;
                                                break;
                                        }
                                    }
                                }
                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                }
            }
            return dataTable;
        }
        catch (Exception)
        {
            if (fs != null)
            {
                fs.Close();
            }
            return null;
        }
    }


}