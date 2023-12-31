﻿using System.Collections.Generic;
using System.Diagnostics;
using Lesson18.Data;
using Lesson18.Models;
using Lesson18.Data.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lesson18.Controllers;

[Authorize]
public class HomeController : Controller
{
    public ShopDbContext _dataContext;


    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger, ShopDbContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    public async Task<IActionResult> Index()
    {
        var dbProducts =  await _dataContext.SelectProducts();
        var products = dbProducts.Select(dbProduct =>
        {
            ProductModel product = dbProduct.ProductType switch
            {
                Data.Models.ProductType.Accessories => new AccessoriesModel(),
                Data.Models.ProductType.Book => new BookModel(),
                Data.Models.ProductType.Food => new FoodModel()
            };

            product.Id = dbProduct.Id;
            product.Name = dbProduct.Name;
            product.Description = dbProduct.Description;
            product.Price = dbProduct.Price;

            return product;
        }).ToArray();

        var model = new IndexModel {
            Products = products
        };

        return View(model);
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromForm]ProductModel newProduct)
    {
        await _dataContext.InsertProduct(new Data.Models.Product() {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Author = "user",
            ProductType = newProduct.ProductType
        });
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize(Policy = "OnlyForAdults")]
    public IActionResult Adult()
    {
        return Redirect("https://www.nalog.gov.ru/rn77/taxation/taxes/ndfl/nalog_vichet/primer_3ndfl/");
    }
}