﻿using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace cheeseIt.Models
{
    public class LoadFileViewModel
    {
        [Required(ErrorMessage = "Please choose a Date.")]
        [DataType(DataType.Date, ErrorMessage = "test error")]
        public DateTime RecievedDate { get; set; }

        public bool DateError { get; set; }
        public bool FileError { get; set; }
    }
}
