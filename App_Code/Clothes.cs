﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Clothes
/// </summary>
public class Clothes {
    private string _id;
    private string _name;
    private int _quantity;
    private decimal _price;
    private string _overview;
    private char _gender;
    private int _category_id;
    private string _link;

    // GET / SET
    public string id { get { return _id; } }
    public string name { get { return _name; } set { _name = value; } }
    public int quantity { get { return _quantity; } set { _quantity = value; } }
    public decimal price { get { return _price; } set { _price = value; } }
    public string overview { get { return _overview; } set { _overview = value; } }
    public char gender { get { return _gender; } set { _gender = value; } }
    public int category_id { get { return _category_id; } set { _category_id = value; } }
    public string link { get { return _link; } }
    public Clothes(string id, string name, int quantity, decimal price, string overview, char gender, int category_id, string link) {
        this._id = id;
        this._name = name;
        this._quantity = quantity;
        this._price = price;
        this._overview = overview;
        this._gender = gender;
        this._category_id = category_id;
        this._link = link;
    }
    public static Clothes getClothesID(string clothesID) {
        Clothes prodDetail_IDK = null;

        int quantity, category_id;
        string id, name, overview, link;
        decimal price;
        char gender;

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from Clothes WHERE ID = @cid", conn);
        cmd.Parameters.AddWithValue("@cid", clothesID);

        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read()) {
            id = dr["Id"].ToString();
            name = dr["name"].ToString();
            quantity = int.Parse(dr["quantity"].ToString());
            price = decimal.Parse(dr["price"].ToString());
            overview = dr["overview"].ToString();
            gender = char.Parse(dr["gender"].ToString());
            category_id = int.Parse(dr["category_id"].ToString());
            link = dr["link"].ToString();

            prodDetail_IDK = new Clothes(id, name, quantity, price, overview, gender, category_id, link);
        } else {
            prodDetail_IDK = null;
        }

        conn.Close();
        dr.Close();
        dr.Dispose();

        return prodDetail_IDK;
    }
}

/// <summary>
/// Shopping Cart Object
/// </summary>
public class Cart {
    public string item_name;
    public int item_quantity;
    public double item_price;
    public string item_color;
    public string item_size;
    public string clothes_id;

    public Cart(string item_name, int item_quantity, double item_price, string item_color, string item_size, string clothes_id) {
        this.item_name = item_name;
        this.item_quantity = item_quantity;
        this.item_price = item_price;
        this.item_color = item_color;
        this.item_size = item_size;
        this.clothes_id = clothes_id;
    }
}

/// <summary>
/// Account class for accounts table
/// </summary>
public class Account {
    private string _id;
    private string _firstname;
    private string _lastname;
    private string _email;
    private bool _isAdmin;
    private string _password;
    private string _mobilenumber;
    private bool _mfaEnabled;
    private string _secret_key;
    private string _adress1;
    private string _adress2;
    private string _zipcode;

    // GET / SET
    public string id { get { return _id; } }
    public string firstname { get { return _firstname; } set { _firstname = value; } }
    public string lastname { get { return _lastname; } set { _lastname = value; } }
    public string email { get { return _email; } set { _email = value; } }
    public bool isAdmin { get { return _isAdmin; } }
    public string password { get { return _password; } }
    public string mobilenumber { get { return _mobilenumber; } set { _mobilenumber = value; } }
    public bool mfaEnabled { get { return _mfaEnabled; } set { _mfaEnabled = value; } }
    public string secret_key { get { return _secret_key; } set { _secret_key = value; } }
    public string adress1 { get { return _adress1; } set { _adress1 = value; } }
    public string adress2 { get { return _adress2; } set { _adress2 = value; } }
    public string zipcode { get { return _zipcode; } set { _zipcode = value; } }

    // Methods
    public Account(string id, string firstname, string lastname, string email, bool isAdmin, string password, string mobilenumber, bool mfaEnabled, string secret_key, string adress1, string adress2, string zipcode) {
        this._id = id;
        this._firstname = firstname;
        this._lastname = lastname;
        this._email = email;
        this._isAdmin = isAdmin;
        this._password = password;
        this._mobilenumber = mobilenumber;
        this._mfaEnabled = mfaEnabled;
        this._secret_key = secret_key;
        this._adress1 = adress1;
        this._adress2 = adress2;
        this._zipcode = zipcode;
    }
    public static Account GetAccount(string emailParam) {
        Account account = null;

        //string id, firstname, lastname, email, password, mobilenumber, secret_key, adress1, adress2, zipcode;
        //bool isAdmin, mfaEnabled;

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from Accounts WHERE email = @email", conn);
        cmd.Parameters.AddWithValue("@email", emailParam);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read()) {
            account = new Account(
                dr["Id"].ToString(),
                dr["first_name"].ToString(),
                dr["last_name"].ToString(),
                dr["email"].ToString(),
                bool.Parse(dr["isAdmin"].ToString()),
                dr["password"].ToString(),
                dr["mobile_number"].ToString(),
                bool.Parse(dr["multi_factor_enabled"].ToString()),
                dr["secret_key"].ToString(),
                dr["address1"].ToString(),
                dr["address2"].ToString(),
                dr["zipcode"].ToString()
                );
        } else {
            account = null;
        }
        conn.Close();
        dr.Close();
        dr.Dispose();
        return account;
    }
    public class SecretKeys {
        public string stripe_api_key { get; set; }
    }
}

public class SecretKeys {
    public string stripe_api_key { get; set; }
    public string google_public { get; set; }
    public string google_secret { get; set; }
}