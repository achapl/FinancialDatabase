﻿using FinancialDatabase;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using Date = Util.Date;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Diagnostics.Eventing.Reader;
using System.Transactions;

public class ItemViewTab : Tab
{

    public ItemViewTab(Form1 Form1) : base(Form1)
    {
        updateButton = Form1.button1;
        editButton   = Form1.button4;
    }

    protected override void generateTBoxGroups()
    {
        allItemLabels = new List<Label>() {
            Form1.label40,
            Form1.label17,
            Form1.label18,
            Form1.label19,
            Form1.label20,
            Form1.label21,
            Form1.label22,
            Form1.label23,
            Form1.label24,
            Form1.label25,
            Form1.label26,
            Form1.label43
        };
        nonEditingLabels = new List<Label>(){
            Form1.label40,
            Form1.label19,
            Form1.label20,
            Form1.label22,
            Form1.label23,
            Form1.label24,
            Form1.label25,
            Form1.label26,
        };
        nonEditingControls = new List<Control>(){
            Form1.label40,
            Form1.label19,
            Form1.label20,
            Form1.label22,
            Form1.label23,
            Form1.label24,
            Form1.label25,
            Form1.label26,
        };

        itemTBoxes = new List<TextBox>()
        {
            Form1.textBox3,
            Form1.textBox4,
            Form1.textBox5
        };
        weightTBoxes = new List<TextBox>()
        {
            Form1.textBox6,
            Form1.textBox7
        };
        shippingTBoxes = new List<TextBox>()
        {
            Form1.textBox6,
            Form1.textBox7,
            Form1.textBox8,
            Form1.textBox9,
            Form1.textBox10
        };

        editControls = new List<Control>() {
            Form1.textBox3,
            Form1.textBox4,
            Form1.textBox5,
            Form1.textBox6,
            Form1.textBox7,
            Form1.textBox8,
            Form1.textBox9,
            Form1.textBox10
        };
        editingControls = new List<Control>(){
            Form1.textBox3,
            Form1.textBox4,
            Form1.textBox5,
            Form1.textBox6,
            Form1.textBox7,
            Form1.textBox8,
            Form1.textBox9,
            Form1.textBox10,
            Form1.button5

        };

        labelTextboxPairs = new Dictionary<Control, Label>();


        for (int i = 0; i < editControls.Count; i++)
        {
            labelTextboxPairs[editControls[i]] = nonEditingLabels[i];
        }

        controlBoxAttrib = new Dictionary<Control, string>
        {{ Form1.textBox3,  "item.Name" },
        { Form1.textBox4,  "item.InitialQuantity" },
        { Form1.textBox5,  "item.CurrentQuantity" },
        { Form1.textBox6,  "shipping.WeightLbs" },
        { Form1.textBox7, "shipping.WeightOz" },
        { Form1.textBox8, "shipping.Length" },
        { Form1.textBox9, "shipping.Width" },
        { Form1.textBox10, "shipping.Height" }};
    }


    public bool allShippingBoxesFilled()
    {
        foreach (Control c in shippingTBoxes)
        {
            if (c.Text.CompareTo("") == 0)
            {
                return false;
            }
        }
        return true;
    }

    public void editUpdate()
    {
        if (Form1.currItem == null) { return; }
        List<Control> changedFields = getChangedFields();


        foreach (Control c in changedFields)
        {
            if (c is null) { Console.WriteLine("ERROR: Control Object c is null, ItemViewTab.cs"); continue; }

            TextBox t = c as TextBox ?? new TextBox();// ?? denotes null assignment

            string query = "";
            if (tableEntryExists(t))
            {
                if (weightTBoxes.Contains(t))
                {
                    if (t.Text == "") { continue; }

                    // Get info for weight
                    int lbs = Int32.Parse(Form1.textBox6.Text);
                    int oz = Int32.Parse(Form1.textBox7.Text);
                    int ttlWeight = lbs * 16 + oz;

                    // Execute query
                    string attrib = "shipping.Weight";
                    string type = Form1.colDataTypes[attrib];
                    query = QB.buildUpdateQuery(Form1.currItem, attrib, type, ttlWeight.ToString());
                    string output = PyConnector.runStatement(query);

                    // Clear shipping textboxes
                    Form1.textBox6.BackColor = Color.White;
                    Form1.textBox7.BackColor = Color.White;
                    Form1.textBox6.Clear();
                    Form1.textBox7.Clear();
                }
                else
                {
                    string type = Form1.colDataTypes[controlBoxAttrib[c]];
                    if (c is TextBox)
                    {
                        query = QB.buildUpdateQuery(Form1.currItem, controlBoxAttrib[c], type, t.Text);
                    }
                    else if (c is DateTimePicker)
                    {
                        query = QB.buildUpdateQuery(Form1.currItem, controlBoxAttrib[c], type, new Date(c));
                    }

                    // Update the item table with the new shipping info
                    string output = PyConnector.runStatement(query);
                    updateItemView(PyConnector.getItem(Form1.currItem.get_ITEM_ID())); // Will also reset currItem with new search for it
                    t.Clear();
                    t.BackColor = Color.White;
                }
            } 
            else if (!tableEntryExists(t))
            {
                if (shippingTBoxes.Contains(t))
                {
                    if (allShippingBoxesFilled())
                    {
                        int weightLbs = Int32.Parse(Form1.textBox6.Text);
                        int weightOz = Int32.Parse(Form1.textBox7.Text);
                        int l = Int32.Parse(Form1.textBox8.Text);
                        int w = Int32.Parse(Form1.textBox9.Text);
                        int h = Int32.Parse(Form1.textBox10.Text);

                        query = QB.buildShipInfoInsertQuery(Form1.currItem, weightLbs, weightOz, l, w, h);

                        int lastrowid = -1;
                        string output = PyConnector.runStatement(query, ref lastrowid);

                        string attrib = "item.ShippingID";
                        string type = Form1.colDataTypes[attrib];
                        int shippingID = lastrowid;
                        query = QB.buildUpdateQuery(Form1.currItem, attrib, type, shippingID.ToString());

                        // Update the item table with the new shipping info
                        output = PyConnector.runStatement(query);
                        updateItemView(PyConnector.getItem(Form1.currItem.get_ITEM_ID())); // Will also reset currItem with new search for it
                    }
                    else
                    {
                        MessageBox.Show(
                            "To Add Shipping Info, all fields must be filled (Lbs, Oz, L, W, H)",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                    }
                }
                else if (itemTBoxes.Contains(t))
                {
                    Console.WriteLine("ERROR: no item entry for CurrItem, This should not be possible");
                    continue;
                }
            }



            // OLD CODE
            /*
            string s = controlBoxAttrib[c!];
            List<int> WeightLbsOz = ozToOzLbs(currItem.get_Weight());
            
            //Hardcode weight as it is the only case where a comb. of 2 fields must be combined into 1 value
            if (s.CompareTo("shipping.WeightLbs") == 0)
            {
                type = Form1.colDataTypes["shipping.WeightLbs"];
                TextBox t_Lbs = Form1.textBox6;
                TextBox t_oz  = Form1.textBox7;
                Label   l_oz  = Form1.label23;

                // Get lbs
                // Type check to make sure proper numbers are  given
                int throwaway;
                if (!Int32.TryParse(t_Lbs.Text, out throwaway)) {query = "ERROR: BAD USER INPUT"; }
                int lbs = Int32.Parse(t_Lbs.Text);

                // Get oz
                int oz = 0;
                if (!Int32.TryParse(t_oz.Text, out oz)) { oz = WeightLbsOz[1]; }

                int weight = 16 * lbs + oz;

                query = QB.buildUpdateQuery(currItem, "shipping.Weight", type, weight.ToString());
                
            }
            else if (s.CompareTo("shipping.WeightOz") == 0)
            {
                type = Form1.colDataTypes["shipping.WeightOz"];
                // Convert weight lbs to oz
                TextBox t_Lbs = Form1.textBox6;
                TextBox t_oz  = Form1.textBox7;
                Label   l_Lbs = Form1.label22;

                // Get lbs
                int lbs = 0;
                if (!Int32.TryParse(t_Lbs.Text, out lbs)) { lbs = WeightLbsOz[0]; }

                // Get ounces
                // Type theck to make sure proper numbers are  given
                int throwaway;
                if (!Int32.TryParse(t_oz.Text, out throwaway)) {query = "ERROR: BAD USER INPUT"; }
                int oz = Int32.Parse(t_oz.Text);
                
                int weight = 16 * lbs + oz;

                query = QB.buildUpdateQuery(currItem, "shipping.Weight", type, weight.ToString());
                
            }
            // ! denotes to the compiler that c will not be null
            else if(c!.GetType() == typeof(TextBox))                
            {
                type = Form1.colDataTypes[controlBoxAttrib[c]];
                
                query = QB.buildUpdateQuery(currItem, controlBoxAttrib[t], type, t.Text);
                t.Clear();
                t.BackColor = Color.White;
            }
            else if (c.GetType() == typeof(DateTimePicker))
            {
                DateTimePicker dt = c as DateTimePicker ?? new DateTimePicker();
                type = Form1.colDataTypes[controlBoxAttrib[c]];
                query = QB.buildUpdateQuery(currItem, controlBoxAttrib[c], type, new Date(dt));
                dt.Value = dt.MinDate; // Set as default value to show it has been "cleared" if new date does not show
            }

            // Clear shipping textboxes
            Form1.textBox6.Clear();
            Form1.textBox6.BackColor = Color.White;
            Form1.textBox7.Clear();
            Form1.textBox7.BackColor = Color.White;

            // Got bad input from user, QB could not create a query
            if (query.CompareTo("ERROR: BAD USER INPUT") == 0) 
            {
                continue ;
            }

            int lastrowid = -1;
            List<string> colNames = new List<string>(new string[] { "" });
            string output = PyConnector.runStatement(query, ref colNames, ref lastrowid);*/

        }
        updateItemView(PyConnector.getItem(Form1.currItem.get_ITEM_ID()));
        showItem(Form1.currItem);
    }

    public void deleteShippingInfo()
    {
        // Delete shipping info entry
        string query = QB.buildDelShipInfoQuery(Form1.currItem);
        string output = PyConnector.runStatement(query);

        // Remove foreign key reference to shipping info from item table
        string attrib = "item.ShippingID";
        string type = Form1.colDataTypes[attrib];
        query = QB.buildUpdateQuery(Form1.currItem, attrib, type, null);
        output = PyConnector.runStatement(query);

        if (output.CompareTo("ERROR") != 0)
        {
            updateItemView(PyConnector.getItem(Form1.currItem.get_ITEM_ID()));
        }
        flipEditState();

    }

    public void updateItemView(ResultItem item)
    {
        showItem(item);
        Form1.tabControl1.SelectTab(1);
    }

    

    
}