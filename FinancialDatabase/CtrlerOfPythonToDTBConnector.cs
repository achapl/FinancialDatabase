﻿using FinancialDatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms.VisualStyles;
using Python.Runtime;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection.Metadata.Ecma335;
using Date = Util.Date;




public class CtrlerOfPythonToDTBConnector
{
    static bool pythonInitialized = false;

    const string PYTHON_EXEC = @"/K python "/* + "-m pdb "*/ + @"C:\Users\Owner\source\repos\FinancialDatabaseSolution\FinancialDatabase\Python\Connection\DCQControl.py ";
    const string START_COL_MARKER = "Column Names:"; // Marker to the Start of Column Names
    const string END_COL_MARKER = "END OF COLUMN NAMES"; // Marker to the End of Column Names
    const string EOS = "EOS";     // end-of-stream

    QueryBuilder QB;

    public CtrlerOfPythonToDTBConnector()
    {
        QB = new QueryBuilder();
    }


    public List<string> getTableNames()
    {

        string query = "SHOW TABLES";
        int lastrowid = -1;
        List<string> colNames = new List<string>(new string[] { "" });
        string rawTablenames = runStatement(query, ref colNames, ref lastrowid);
        List<string> tableNames = new List<string>(rawTablenames.Substring(3, rawTablenames.Length - 7).Split("',), ('"));

        return tableNames;
    }

    public ResultItem getItem(int itemID)
    {
        string queryItem = "SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM item WHERE ITEM_ID = " + itemID.ToString() + ") subItem LEFT JOIN purchase ON purchase.PURCHASE_ID = subItem.PurchaseID) subPurchase) subSale LEFT JOIN sale ON sale.SALE_ID = subSale.SaleID) subShipping LEFT JOIN shipping on shipping.SHIPPING_ID = subShipping.shippingID;";

        List<ResultItem> result = RunItemSearchQuery(queryItem);

        // Error Checking
        if (result.Count > 1)
        {
            Console.WriteLine("Error: >1 Items Found for itemID: " + itemID.ToString());
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].ToString());
            }
            return null;
        }
        else if (result.Count() == 0)
        {
            Console.WriteLine("Error: No Items Found for ItemID: " + itemID.ToString());
        }

        ResultItem item = result[0];
        return item;
    }

    public Dictionary<string, string> getColDataTypes()
    {

        List<string> tableNames = getTableNames();

        Dictionary<string, string> colDataTypes = new Dictionary<string, string>();
        string query;
        List<string> output;
        foreach (string tableName in tableNames)
        {
            query = "SHOW COLUMNS FROM " + tableName + ";";

            int lastrowid = -1;
            List<string> colNames = new List<string>(new string[] { "" });
            string rawOutput = runStatement(query, ref colNames, ref lastrowid);
            output = new List<string>(rawOutput.Substring(3, rawOutput.Length - 7).Split("'), ('"));


            //removeColumnNames(ref output);
            string[] startAndEnd = { "('", "',)" };
            foreach (string colAndType in output)
            {
                List<string> typesForCol = new List<string>(colAndType.Split(new string[] { "', '" }, StringSplitOptions.None));
                string colName = tableName + "." + typesForCol[0];
                string type = typesForCol[1];
                colDataTypes[colName] = type;
            }
        }

        //Hardcoded types for special cases
        colDataTypes["shipping.WeightLbs"] = "int unsigned";
        colDataTypes["shipping.WeightOz"] = "int unsigned";

        return colDataTypes;
    }

    // General queries, done by manual string input
    public List<ResultItem> RunItemSearchQuery(string query)
    {
        int lastrowid = -1;
        List<string> colNames = new List<string>(new string[] { "" });
        string queryOutput = runStatement(query, ref colNames, ref lastrowid);

        List<ResultItem> parsedItems = parseRawQuery(queryOutput, colNames);

        return parsedItems;
    }

    public string runStatement(string statement, ref List<string> colNames, ref int lastrowid)
    {
        string retList;

        List<string> result = runPython(statement);
        if (result[0].CompareTo("ERROR") == 0)
        {
            Console.WriteLine("ERROR: Invalid Statement/Query sent to database: " + statement);
        }
        // Returns [0,1,2] -> result, colNames, cursor.lastrowid
        retList = result[0];
        colNames = new List<string>(result[1].Substring(2, result[1].Length - 4).Split("', '"));
        lastrowid = Int32.Parse(result[2]);
        return retList;
    }

    public string runStatement(string statement, ref List<string> colNames)
    {
        string retList;

        List<string> result = runPython(statement);
        if (result[0].CompareTo("ERROR") == 0)
        {
            Console.WriteLine("ERROR: Invalid Statement/Query sent to database: " + statement);
        }
        // Returns [0,1,2] -> result, colNames, cursor.lastrowid
        retList = result[0];
        colNames = new List<string>(result[1].Substring(2, result[1].Length - 4).Split("', '"));
        return retList;
    }

    public string runStatement(string statement, ref int lastrowid)
    {
        string retList;

        List<string> result = runPython(statement);
        if (result[0].CompareTo("ERROR") == 0)
        {
            Console.WriteLine("ERROR: Invalid Statement/Query sent to database: " + statement);
        }
        // Returns [0,1,2] -> result, colNames, cursor.lastrowid
        retList = result[0];
        lastrowid = Int32.Parse(result[2]);
        return retList;
    }

    public string runStatement(string statement)
    {
        string retList;

        List<string> result = runPython(statement);
        if (result[0].CompareTo("ERROR") == 0)
        {
            Console.WriteLine("ERROR: Invalid Statement/Query sent to database: " + statement);
        }
        // Returns [0,1,2] -> result, colNames, cursor.lastrowid
        retList = result[0];
        return retList;
    }

    public int newPurchase(int purcPrice, string notes, Date PurcDate)
    {
        string query = QB.buildInsertPurchaseQuery(purcPrice, notes, PurcDate);
        int lastrowid = -1;
        runStatement(query, ref lastrowid);
        return lastrowid;
    }

    public string insertItem(ResultItem item)
    {
        string query = QB.buildItemInsertQuery(item);
        int lastrowid = -1;
        string output = runStatement(query, ref lastrowid);
        if (lastrowid == -1)
        {
            Console.WriteLine("ERROR, BAD INPUT INTO DATABASE: insertItem");
            return null;
        }

        if (output.CompareTo("ERROR") == 0)
        {
            Console.WriteLine("ERROR insertItem");
            return null;
        }
        int lastrowid2 = -1;
        // If given item also has shipping info, insert that into the database too
        if (item.get_Weight() != ResultItem.DEFAULT_INT)
        {
            item.set_ITEM_ID(lastrowid);
            query = QB.buildShipInfoInsertQuery(item);
            output = runStatement(query, ref lastrowid2);
            if (lastrowid2 == -1)
            {
                Console.WriteLine("ERROR IN INSERTING SHIPPING INFO");
            }

            string attrib = "item.ShippingID";
            string type = "int unsigned";
            int shippingID = lastrowid2;
            query = QB.buildUpdateQuery(item, attrib, type, shippingID.ToString());

            // Update the item table with the new shipping info
            output = runStatement(query);
        }
        
        return output;

    }

    // Given a search query, turn it into a string query and run it
    public List<ResultItem> RunSearchQuery(SearchQuery Q)
    {
        string query = QB.buildSearchByNameQuery(Q);
        return RunItemSearchQuery(query);
    }

    private List<string> runPython(string query)
    {
        // Startup Python
        if (pythonInitialized == false) {
            Runtime.PythonDLL = @"C:\Users\Owner\AppData\Local\Programs\Python\Python311\python311.dll";
            PythonEngine.Initialize();
            pythonInitialized = true;
        }
        
        // Use Python
        List<string> result = new List<string>(){ "","","" };
        using (Py.GIL())
        {
            // Modify path to work
            dynamic os = Py.Import("os");
            dynamic sys = Py.Import("sys");
            sys.path.append(os.path.dirname(os.path.expanduser(@"C:\Users\Owner\source\repos\FinancialDatabaseSolution\FinancialDatabase\Python\")));

            // Start Dtb Connector
            dynamic Connector = Py.Import("Connection.DtbConnAndQuery");
            PyObject[] rawResult = Connector.runQuery(query);

            // Convert Python Objects to normal C# strings
            // ?? denotes alternative assignment to
            // result[n] if rawResult[n] is null
            string ErrMsg = "ERROR: NULL rawResult val in CtrlerOfPythonToDTBConnector.cs";
            result[0] = rawResult[0].ToString() ?? ErrMsg;
            result[1] = rawResult[1].ToString() ?? ErrMsg;
            result[2] = rawResult[2].ToString() ?? ErrMsg;


        }
        return result;
    }

    private List<ResultItem> parseRawQuery(string rawResult, List<string> colNames)
    {

        // raw result is now the format "(itemName, itemID, .etc)(item2Name, item2ID, .etc)"
        // Seperate whole string into list of multiple item strings, "[ (itemName, itemID, .etc), (item2Name, item2ID, .etc) ]"
        rawResult = rawResult.Trim('[', ']');
        List<string> rawItems = Util.PairedCharTopLevelSplit(rawResult, '(');

        List<ResultItem> results = new List<ResultItem>();
        foreach(string rawItem in rawItems)
        {
            // Seperate each item into individual item attributes to make a ResultItem with it
            List<string> itemAttributes = new List<string>(Util.splitOnTopLevelCommas(rawItem));
            results.Add(new ResultItem(itemAttributes, colNames));
        }

        return results;
    }


}
