using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003951_DBSD_CW2.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public int Remaining { get; set; }
        public int FlowerCategoryId { get; set; }
        public FlowerCategory FlowerCategory { get; set; }
    }
}