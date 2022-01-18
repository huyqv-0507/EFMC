using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Data.Entities;
using Mapster;

namespace EFMC.Service.Models
{
    public class ConsignmentModel
    {
        public int ConsignmentId { get; set; }
        public DateTime DateImported { get; set; }
        public string From { get; set; }
        public decimal TotalCost { get; set; }
        public List<DrugLocal> Drugs { get; set; }
        public class DrugLocal
        {
            public int DrugId { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public string Package { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; } // Sell
            public int Quantity { get; set; }
#nullable enable
            public string? BrandName { get; set; }
            public string? MainIngredient { get; set; }
            public string? Ingredient { get; set; }
            public string? Description { get; set; }
        }
        public static DrugLocal ToDrugLocal(Drug drug)
        {
            return drug.Adapt<DrugLocal>();
        }
    }
    public class ConsignmentImported
    {
        public DateTime DateImported { get; set; }
#nullable disable
        public string From { get; set; }
        public decimal TotalCost { get; set; }
#nullable enable
        public List<DrugExisted>? DrugExisteds { get; set; }
        public List<DrugNew>? DrugNews { get; set; }
        public class DrugExisted
        {
            public int DrugId { get; set; }
            public int Quantity { get; set; }
            public decimal Cost { get; set; }
        }
        public class DrugNew
        {
#nullable disable
            public string Name { get; set; }
            public string Content { get; set; }
            public string Package { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal Cost { get; set; }
#nullable enable
            public int? DrugId { get; set; }
            public string? BrandName { get; set; }
            public string? MainIngredient { get; set; }
            public string? Ingredient { get; set; }
            public string? Description { get; set; }
        }
    }

}
