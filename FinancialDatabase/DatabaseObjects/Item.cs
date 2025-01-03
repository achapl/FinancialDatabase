﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Date = Util.Date;



public class Item : IEquatable<Item>
{
    string rawResult;
    List<string> rawList;

    // From item table
    int ITEM_ID;
    string Name;
    int PurchaseID;
    int SaleID;
    int ShippingID;
    int InitialQuantity;
    int CurrentQuantity;
    string Notes_item;

    // From purchase table
    Date Date_Purchased;
    double Amount_purchase;
    double Tax;
    double Fees_purchase;
    string Seller;
    string Notes_purchase;

    // From sale table
    Date Date_Sold;
    double Amount_sale;
    double Fees_sale;
    string Buyer;

    // From shipping table
    int Length;
    int Width;
    int Height;
    int Weight;
    string Notes_shipping;

    // From image table
    List<MyImage> images;

    // Extra
    double totalSales;
    private MyImage thumbnail;


#pragma warning disable CS8618 // Util.DEFAULTs are set to non-null vals
    public Item()
    {
        setDefaults();
    }
    #pragma warning disable CS8618 // Util.DEFAULTs are set to non-null vals
    public Item(string item, List<string> colNames)
	{
        this.setDefaults();
		this.rawResult = item;
	}
    
    void setDefaults()
    {
        //string rawResult;
        //List<string> rawList;

        // From item table
        ITEM_ID         = Util.DEFAULT_INT;
        Name            = Util.DEFAULT_STRING;
        PurchaseID      = Util.DEFAULT_INT;
        SaleID          = Util.DEFAULT_INT;
        ShippingID      = Util.DEFAULT_INT;
        InitialQuantity = Util.DEFAULT_INT;
        CurrentQuantity = Util.DEFAULT_INT;
        Notes_item      = Util.DEFAULT_STRING;

        // From purchase table
        Date_Purchased  = Util.DEFAULT_DATE;
        Amount_purchase = Util.DEFAULT_DOUBLE;
        Tax             = Util.DEFAULT_DOUBLE;
        Fees_purchase   = Util.DEFAULT_DOUBLE;
        Seller          = Util.DEFAULT_STRING;
        Notes_purchase  = Util.DEFAULT_STRING;

        // From sale table
        Date_Sold       = Util.DEFAULT_DATE;
        Amount_sale     = Util.DEFAULT_DOUBLE;
        Fees_sale       = Util.DEFAULT_DOUBLE;
        Buyer           = Util.DEFAULT_STRING;

        // From shipping table
        Length          = Util.DEFAULT_INT;
        Width           = Util.DEFAULT_INT;
        Height          = Util.DEFAULT_INT;
        Weight          = Util.DEFAULT_INT;
        Notes_shipping  = Util.DEFAULT_STRING;

        //From image table
        images       = Util.DEFAULT_IMAGES;
        
        // Extra
        totalSales      = SaleTab.getTotalSales(this);
    }

    public Item(List<string> item, List<string> colNames)
    {
        this.rawList = item;

        for(int i = 0; i < colNames.Count; i++) 
        {
            string itemAttribute = item[i];
            // Missing info, skip
            if (item[i].CompareTo("None") == 0)
            {
                itemAttribute = null!;
            }
            switch(colNames[i])
            {
                // From item table
                case "ITEM_ID":
                    set_ITEM_ID(itemAttribute);
                    break;
                case "Name":
                    set_Name(itemAttribute);
                    break;
                case "PurchaseID":
                    set_PurchaseID(itemAttribute);
                    break;
                case "SaleID":
                    set_SaleID(itemAttribute);
                    break;
                case "ShippingID":
                    set_ShippingID(itemAttribute);
                    break;
                case "InitialQuantity":
                    set_InitialQuantity(itemAttribute);
                    break;
                case "CurrentQuantity":
                    set_CurrentQuantity(itemAttribute);
                    break;
                case "Notes_item":
                    set_Notes_item(itemAttribute);
                    break;
                case "thumbnail":
                    set_Thumbnail(itemAttribute);
                    break;
                // From purchase table
                case "Date_Purchased":
                    set_Date_Purchased(itemAttribute);
                    break;
                case "Amount_purchase":
                    set_Amount_purchase(itemAttribute);
                    break;
                case "Tax":
                    set_Tax(itemAttribute);
                    break;
                case "Fees_purchase":
                    set_Fees_purchase(itemAttribute);
                    break;
                case "Seller":
                    set_Seller(itemAttribute);
                    break;
                case "Notes_purchase":
                    set_Notes_purchase(itemAttribute);
                    break;

                // From sale table
                case "Date_Sold":
                    set_Date_Sold(itemAttribute);
                    break;
                case "Amount_sale":
                    set_Amount_sale(itemAttribute);
                    break;
                case "Fees_sale":
                    set_Fees_sale(itemAttribute);
                    break;
                case "Buyer":
                    set_Buyer(itemAttribute);
                    break;

                // From shipping table
                case "Length":
                    set_Length(itemAttribute);
                    break;
                case "Width":
                    set_Width(itemAttribute);
                    break;
                case "Height":
                    set_Height(itemAttribute);
                    break;
                case "Weight":
                    set_Weight(itemAttribute);
                    break;
                case "Notes_shipping":
                    set_Notes_shipping(itemAttribute);
                    break;
            }
        }
    }
    public void set_Thumbnail(MyImage image)
    {
        if (image == null)
        {
            this.thumbnail = Util.DEFAULT_IMAGE;
            return;
        } 
        this.thumbnail = image;
    }
    public void set_Thumbnail(string itemAttribute)
    {
        Image i = Util.rawImageStrToImage(itemAttribute);
        this.thumbnail = new MyImage(i,-1);
    }

    public void getAttrib(string attrib, ref string ret)
    {
        switch (attrib)
        {
            // From item table
            case "item.Name":
                ret = get_Name();
                break;
            case "item.Notes_item":
                ret = get_Notes_item();
                break;

            // From purchase table
            case "purchase.Seller":
                ret = get_Seller();
                break;
            case "purchase.Notes_purchase":
                ret = get_Notes_purchase();
                break;

            // From sale table
            case "sale.Buyer":
                ret = get_Buyer();
                break;

            // From shipping table
            case "shipping.Notes_shipping":
                ret = get_Notes_shipping();
                break;
        }
    }
    
    public void getAttrib(string attrib, ref int ret)
    {
        switch (attrib)
        {
            // From item table
            case "item.ITEM_ID":
                ret = get_ITEM_ID();
                break;
            case "item.PurchaseID":
                ret = get_PurchaseID();
                break;
            case "item.SaleID":
                ret = get_SaleID();
                break;
            case "item.ShippingID":
                ret = get_ShippingID();
                break;
            case "item.InitialQuantity":
                ret = get_InitialQuantity();
                break;
            case "item.CurrentQuantity":
                ret = get_CurrentQuantity();
                break;

            // From shipping table
            case "shipping.Length":
                ret = get_Length();
                break;
            case "shipping.Width":
                ret = get_Width();
                break;
            case "shipping.Height":
                ret = get_Height();
                break;
            case "shipping.Weight":
                ret = get_Weight();
                break;
            case "shipping.WeightLbs":
                ret = get_WeightLbs();
                break;
            case "shipping.WeightOz":
                ret = get_WeightOz();
                break;
        }
    }
    
    public void getAttrib(string attrib, ref double ret)
    {
        switch (attrib)
        {
            // From purchase table
            case "purchase.Amount_purchase":
                ret = get_Amount_purchase();
                break;
            case "purchase.Tax":
                ret = get_Tax();
                break;
            case "purchase.Fees_purchase":
                ret = get_Fees_purchase();
                break;

            // From sale table
            case "sale.Amount_sale":
                ret = get_Amount_sale();
                break;
            case "sale.Fees_sale":
                ret = get_Fees_sale();
                break;

            // From shipping table
            case "shipping.Length":
                ret = get_Length();
                break;
            case "shipping.Width":
                ret = get_Width();
                break;
            case "shipping.Height":
                ret = get_Height();
                break;
            case "shipping.Weight":
                ret = get_Weight();
                break;
        }
    }
    
    public void getAttrib(string attrib, ref Date ret)
    {
        switch (attrib)
        {
            // From sale table
            case "sale.Date_Sold":
                ret = get_Date_Sold();
                break;

            // From purchase table
            case "purhcase.Date_Purchased":
                ret = get_Date_Purchased();
                break;
        }
    }

    public void getAttribAsStr(string attrib, out string ret)
    {
        ret = "";
        switch (attrib)
        {
            // From item table
            case "item.Name":
                ret = get_Name();
                break;
            case "item.Notes_item":
                ret = get_Notes_item();
                break;

            // From purchase table
            case "purchase.Seller":
                ret = get_Seller();
                break;
            case "purchase.Notes_purchase":
                ret = get_Notes_purchase();
                break;

            // From sale table
            case "sale.Buyer":
                ret = get_Buyer();
                break;

            // From shipping table
            case "shipping.Notes_shipping":
                ret = get_Notes_shipping();
                break;

            // From item table
            case "item.ITEM_ID":
                ret = get_ITEM_ID().ToString();
                break;
            case "item.PurchaseID":
                ret = get_PurchaseID().ToString();
                break;
            case "item.SaleID":
                ret = get_SaleID().ToString();
                break;
            case "item.ShippingID":
                ret = get_ShippingID().ToString();
                break;
            case "item.InitialQuantity":
                ret = get_InitialQuantity().ToString();
                break;
            case "item.CurrentQuantity":
                ret = get_CurrentQuantity().ToString();
                break;

            // From shipping table
            case "shipping.Length":
                ret = get_Length().ToString();
                break;
            case "shipping.Width":
                ret = get_Width().ToString();
                break;
            case "shipping.Height":
                ret = get_Height().ToString();
                break;
            case "shipping.Weight":
                ret = get_Weight().ToString();
                break;
            case "shipping.WeightLbs":
                ret = get_WeightLbs().ToString();
                break;
            case "shipping.WeightOz":
                ret = get_WeightOz().ToString();
                break;

            // From purchase table
            case "purchase.Amount_purchase":
                ret = get_Amount_purchase().ToString();
                break;
            case "purchase.Tax":
                ret = get_Tax().ToString();
                break;
            case "purchase.Fees_purchase":
                ret = get_Fees_purchase().ToString();
                break;

            // From sale table
            case "sale.Amount_sale":
                ret = get_Amount_sale().ToString();
                break;
            case "sale.Fees_sale":
                ret = get_Fees_sale().ToString();
                break;
        }

        if (ret.CompareTo(Util.DEFAULT_DATE.toDateString()) == 0 || 
            ret.CompareTo(Util.DEFAULT_DOUBLE.ToString()) == 0 ||
            ret.CompareTo(Util.DEFAULT_INT.ToString())  == 0 ||
            ret.CompareTo(Util.DEFAULT_STRING) == 0)
        {
            ret = "";
        }
    }

    public string getAttribVal(string attrib)
    {
        string ret = "";
        getAttribAsStr(attrib, out ret);
        return ret;
    }

    public bool hasItemEntry() => ITEM_ID != Util.DEFAULT_INT;

    public bool hasPurchaseEntry() => PurchaseID != Util.DEFAULT_INT;

    public bool hasSaleEntry() => SaleID != Util.DEFAULT_INT;

    public bool hasShippingEntry() => ShippingID != Util.DEFAULT_INT;


    public bool hasImageEntry() => images != Util.DEFAULT_IMAGES;


    // From item table
    public int get_ITEM_ID() => ITEM_ID;
    public string get_Name() => Name;
    public int get_PurchaseID() => PurchaseID;
    public int get_SaleID() => SaleID;
    public int get_ShippingID() => ShippingID;
    public int get_InitialQuantity() => InitialQuantity;
    public int get_CurrentQuantity() => CurrentQuantity;
    public string get_Notes_item() => Notes_item;
    public MyImage get_Thumbnail()
    {
        if (this.thumbnail == null) { set_Thumbnail(Util.DEFAULT_IMAGE); } 
        return this.thumbnail;
    }


    // From purchase table
    public Date get_Date_Purchased() => Date_Purchased;
    public string get_Date_Purchased_str() => Date_Purchased.toDateString();
    public double get_Amount_purchase() => Amount_purchase;
    public double get_Tax() => Tax;
    public double get_Fees_purchase() => Fees_purchase;
    public string get_Seller() => Seller;
    public string get_Notes_purchase() => Notes_purchase;

    // From sale table
    public Date get_Date_Sold() => Date_Sold;

    public string get_Date_Sold_str() => this.Date_Sold.toDateString();

    public double get_Amount_sale() => Amount_sale;
    public double get_Fees_sale() => Fees_sale;
    public string get_Buyer() => Buyer;

    // From shipping table
    public int get_Length() => Length;
    public int get_Width() => Width;
    public int get_Height() => Height;
    public int get_Weight() => Weight;
    public int get_WeightOz() => Weight % 16;
    public int get_WeightLbs() => Weight / 16;
    public string get_Notes_shipping() => Notes_shipping;

    // From image table
    public List<MyImage> get_Images() {
        if (images == Util.DEFAULT_IMAGES || images == null)
        {
            return Util.DEFAULT_IMAGES;
        }
        else
        {
            return images;
        }
    }

    // Extra
    public double getTotalSales() => totalSales;
    

    // From item table
    public void set_ITEM_ID(int ITEM_ID) => this.ITEM_ID = ITEM_ID;
    public void set_ITEM_ID(string ITEM_ID) => this.ITEM_ID = Int32.Parse(ITEM_ID);
    public void set_Name(string Name)
    {
        // Trim off any quotations around the name should there be any
        if (Name[0] == Name[Name.Length - 1] && Name[0] == '\'')
        {
            Name = Name.Trim('\'');
        }
        else if (Name[0] == Name[Name.Length - 1] && Name[0] == '"')
        {
            Name = Name.Trim('"');
        }
        this.Name = Name;

    }

    public void set_PurchaseID(int PurchaseID) => this.PurchaseID = PurchaseID;
    public void set_PurchaseID(string PurchaseID)
    {
        if (PurchaseID == null)
        {
            this.PurchaseID = -1;
        }
        else
        {
            this.PurchaseID = Int32.Parse(PurchaseID);
        }
    }
    public void set_SaleID(int SaleID) => this.SaleID = SaleID;
    public void set_SaleID(string SaleID)
    {
        if (SaleID == null)
        {
            this.SaleID = -1;
        }
        else
        {
            this.SaleID = Int32.Parse(SaleID);
        }
    }
    public void set_ShippingID(int ShippingID) => this.ShippingID = ShippingID;
    public void set_ShippingID(string ShippingID)
    {
        if (ShippingID == null)
        {
            this.ShippingID = -1;
        }
        else
        {
            this.ShippingID = Int32.Parse(ShippingID);
        }
    }
    public void set_InitialQuantity(int InitialQuantity) => this.InitialQuantity = InitialQuantity;
    public void set_InitialQuantity(string InitialQuantity)
    {
        if (InitialQuantity == null)
        {
            this.InitialQuantity = -1;
        }
        else
        {
            this.InitialQuantity = Int32.Parse(InitialQuantity);
        }
    }
    public void set_CurrentQuantity(int CurrentQuantity) => this.CurrentQuantity = CurrentQuantity;
    public void set_CurrentQuantity(string CurrentQuantity)
    {
        if (CurrentQuantity == null)
        {
            this.CurrentQuantity = -1;
        }
        else
        {
            this.CurrentQuantity = Int32.Parse(CurrentQuantity);
        }
    }
    public void set_Notes_item(string Notes_item) => this.Notes_item = Notes_item;


    // From purchase table
    public void set_Date_Purchased(int year, int month, int day)
    {
        this.Date_Purchased = new Date(year, month, day);
    }
    public void set_Date_Purchased(Date date) => this.Date_Purchased = date;
    public void set_Date_Purchased(string date) => this.Date_Purchased = parseDate(date);
    public void set_Amount_purchase(double Amount_purchase) => this.Amount_purchase = Amount_purchase;
    public void set_Amount_purchase(string Amount_purchase)
    {
        if (Amount_purchase == null)
        {
            this.Amount_purchase = -1.0;
        }
        else
        {
            this.Amount_purchase = Double.Parse(Amount_purchase);
        }
    }
    public void set_Tax(double Tax) => this.Tax = Tax;
    public void set_Tax(string Tax)
    {
        if (Tax == null)
        {
            this.Tax = -1.0;
        }
        else
        {
            this.Tax = Double.Parse(Tax);
        }
    }
    public void set_Fees_purchase(double Fees_purchase) => this.Fees_purchase = Fees_purchase;
    public void set_Fees_purchase(string Fees_purchase) => this.Fees_purchase = double.Parse(Fees_purchase);
    public void set_Seller(string Seller) => this.Seller = Seller;
    public void set_Notes_purchase(string Notes_purchase) => this.Notes_purchase = Notes_purchase;
    

    // From sale table
    public void set_Date_Sold(int year, int month, int day) => this.Date_Sold = new Date(year, month, day);
    public void set_Date_Sold(Date date) => this.Date_Sold = date;
    public void set_Date_Sold(string date) => this.Date_Sold = parseDate(date);
    public void set_Amount_sale(double Amount_sale) => this.Amount_sale = Amount_sale;
    public void set_Amount_sale(string Amount_sale)
    {
        if (Amount_sale == null)
        {
            this.Amount_sale = -1.0;
        }
        else
        {
            this.Amount_sale = Double.Parse(Amount_sale);
        }
    }
    public void set_Fees_sale(double Fees_sale) => this.Fees_sale = Fees_sale;
    public void set_Fees_sale(string Fees_sale)
    {
        if (Fees_sale == null)
        {
            this.Fees_sale = -1.0;
        }
        else
        {
            this.Fees_sale = Double.Parse(Fees_sale);
        }
    }
    public void set_Buyer(string Buyer) => this.Buyer = Buyer;
    


    // From shipping table
    public void set_Length(int Length) => this.Length = Length;
    public void set_Length(string Length)
    {
        if (Length == null)
        {
            this.Length = -1;
        }
        else
        {
            this.Length = Int32.Parse(Length);
        }
    }
    public void set_Width(int Width) => this.Width = Width;
    public void set_Width(string Width)
    {
        if (Width == null)
        {
            this.Width = -1;
        }
        else
        {
            this.Width = Int32.Parse(Width);
        }
    }
    public void set_Height(int Height) => this.Height = Height;
    public void set_Height(string Height)
    {
        if (Height == null)
        {
            this.Height = -1;
        }
        else
        {
            this.Height = Int32.Parse(Height);
        }
    }
    public void set_Weight(int Weight) => this.Weight = Weight;
    public void set_Weight(string Weight)
    {
        if (Weight == null)
        {
            this.Weight = -1;
        }
        else
        {
            this.Weight = Int32.Parse(Weight);
        }
    }
    public void set_Notes_shipping(string Notes_shipping) => this.Notes_shipping = Notes_shipping;

    // From image table
    public void clear_images() => this.images = Util.DEFAULT_IMAGES;
    public void add_image(MyImage image) 
    {
        if (images == null)
        {
            set_images(new List<MyImage>() { image });
        } else
        {
            this.images.Append(image);
        }
    }
    public void add_images(List<MyImage> images)
    {
        if (images.Count > 0)
        {
            this.images.AddRange(images);
        }
    }

    public void set_images()
    {
        set_images(Database.getAllImages(this));
    }

    public void set_images(List<MyImage> images)
    {
        if (images.Count > 0)
        {
            this.images = images;
        }
    }

    // Extra
    public void set_totalSales(double totalSales)
    {
        if (totalSales == null)
        {
            this.totalSales = -1;
        }
        else
        {
            this.totalSales = totalSales;
        }
    }

    #pragma warning disable CS8767 // 'other' and 'this' are checked for null
    public bool Equals(Item other)
    {
        if (this  == null && other == null)  return true;
        if (this  == null)  return false;
        if (other == null)  return false;
        return this.ITEM_ID == other!.ITEM_ID;
    }

    private Date parseDate(string date)
    {
        if (date == null)
        {
            return new Date(0, 0, 0);
        }
        // date is format (y,m,d): "datetime.date(2020, 1, 1)" 
        date = date.Remove(0, "datetime.date".Length);
        // "(2020, 1, 1)"
        date = date.Trim(new char[] { '(', ')' });
        // "2020, 1, 1"
        List<string> ymd = new List<string>(date.Split(new string[] { ", " }, StringSplitOptions.None));
        // ["2020","1","1"] (string[])
        return new Date(Int32.Parse(ymd[0]), Int32.Parse(ymd[1]), Int32.Parse(ymd[2]));
    }

    // Name for an item in the database
    // Escape characters are taken care of,
    // So it should at least contain 1 alpha numeric char
    // And be >=1 length
    public static bool isValidName(string name)
    {
        if (name.Length == 0) return false;
        return Util.containsAlphaNumeric(name);
    }
}
