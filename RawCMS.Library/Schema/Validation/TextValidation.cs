﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RawCMS.Library.Schema.Validation
{
    public class TextValidation : FieldTypeValidator
    {
        public override string Type => "text";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            string value = (input[field.Name] ?? "").ToString();
            if (field.Options != null)
            {
                if (field.Options["maxlength"] != null)
                {
                    if (int.TryParse(field.Options["maxlength"].ToString(), out int maxlenght))
                    {
                        if (input[field.Name] != null && maxlenght < value.ToString().Length)
                        {
                            errors.Add(new Error()
                            {
                                Code = "REQUIRED",
                                Title = "Field " + field.Name + " too long"
                            });
                        }
                    }
                }

                if (field.Options["regexp"] != null)
                {
                    if (!Regex.IsMatch(value, field.Options["regexp"].ToString()))
                    {
                        errors.Add(new Error()
                        {
                            Code = "INVALID",
                            Title = "Field " + field.Name + " doesn't match" + field.Options["regexp"]
                        });
                    }
                }
            }
            return errors;
        }
    }
}