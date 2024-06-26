﻿using HelixLaserWorks.Core.Models.Material;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Part
{
    public class PartFormModel
    {
        [Required]
        [StringLength(PartNameMaxLength,
            MinimumLength = PartNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(PartDescriptionMaxLength,
            MinimumLength = PartDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int MaterialId { get; set; }

        public ICollection<MaterialDropdownViewModel> Materials { get; set; } = new List<MaterialDropdownViewModel>();

        [Required]
        [Display(Name = "Part Thickness")]
        public double PartThickness { get; set; }

        [Required]
        [Range(PartQuantityMinValue, PartQuantityMaxValue)]
        public int Quantity { get; set; }

        public string? SchemeUrl { get; set; }

        [Display(Name = "Scheme File")]
        public IFormFile? SchemeFile { get; set; }

        [FileExtensions(Extensions = ".dxf,.cad,.pdf,.dwg,.ai,.eps,.step,.stp")]
        public string? FileName
        {
            get
            {
                if (SchemeFile != null)
                {
                    return SchemeFile.FileName;
                }

                return null;
            }
        }


        [Range(PartSchemFileMinLength, PartSchemFileMaxLength, ErrorMessage = "Maximum file size allowed is 5MB")]
        public long? FileLength
        {
            get
            {
                if (SchemeFile != null)
                {
                    return SchemeFile.Length;
                }

                return null;
            }
        }
    }
}
