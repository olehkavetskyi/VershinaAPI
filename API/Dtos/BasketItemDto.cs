﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class BasketItemDto
{
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 1 cent")]
    public decimal Price { get; set; }
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
    [Required]
    public string PictureUrl { get; set; } = null!;
    [Required]
    public string Brand { get; set; } = null!;
    [Required]
    public string Type { get; set; } = null!;
}
