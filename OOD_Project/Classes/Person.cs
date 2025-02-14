﻿using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Person : DataType, IGetFields
    {
        [JsonInclude]
        public string name { get; set; }
        [JsonInclude]
        public UInt64 age { get; set; }
        [JsonInclude]
        public string phone { get; set; }
        [JsonInclude]
        public string email { get; set; }

        public Person(string type, UInt64 iD, string name, UInt64 age, string phone, string email) : base(iD, type)
        {
            this.name = name;
            this.age = age;
            this.phone = phone;
            this.email = email;
        }

        public Person():base()
        {

        }
        public override void Update(ContactInfoUpdateArgs args)
        {
            phone = args.PhoneNumber;
            email = args.EmailAddress;
        }

        public static new string[] GetFields()
        {
            string[] ret = ["ID", "type", "name", "age", "phone", "email"];
            return ret;
        }

        public new string[] GetValues()
        {
            string[] ret = [ID.ToString(), type, name, age.ToString(), phone, email];
            return ret;
        }
       
    }
}
