﻿using System.Threading.Tasks;
using CarvedRock.Web.Clients;
using CarvedRock.Web.HttpClients;
using CarvedRock.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Web.Controllers
{
    public class HomeController: Controller
    {
        private readonly ProductHttpClient _httpClient;
        private readonly ProductHttpClient2 _httpClient2;
        private readonly ProductGraphClient _productGraphClient;

        public HomeController(
            ProductHttpClient httpClient, 
            ProductHttpClient2 httpClient2,     // with httpclient factory
            ProductGraphClient productGraphClient)
        {
            _httpClient = httpClient;
            _httpClient2 = httpClient2;
            _productGraphClient = productGraphClient;
        }


        public async Task<IActionResult> Index()
        {
            var responseModel = await _httpClient2.GetProducts();
            responseModel.ThrowErrors();
            return View(responseModel.Data.Products);
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            await _productGraphClient.SubscribeToUpdates();
            var product = await _productGraphClient.GetProduct(productId);
            return View(product);
        }

        public IActionResult AddReview(int productId)
        {
            return View(new ProductReviewModel {ProductId = productId});
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ProductReviewInputModel reviewModel)
        {
            await _productGraphClient.AddReview(reviewModel);
            return RedirectToAction("ProductDetail", new {productId = reviewModel.ProductId});
        }
    }
}
