﻿using System.ComponentModel.DataAnnotations;
namespace RESTfulAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
